using System.Text;
using FastEndpoints;
using FastEndpoints.Swagger;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NJsonSchema.Generation;
using WordsmithHub.API.Features.Common.AppUserIdPreprocessing;
using WordsmithHub.API.Services.FreelanceAccessService;
using WordsmithHub.API.Services.ResourceAccessService;
using WordsmithHub.API.Services.TokenService;
using WordsmithHub.Domain;
using WordsmithHub.Infrastructure.IdentityDatabase;
using WordsmithHub.Infrastructure.MainDatabase;
using WordsmithHub.Infrastructure.MainDatabase.Repositories;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

// DbContexts
if (!builder.Environment.IsEnvironment("IntegrationTest"))
{
    builder.Services.AddDbContext<IdentityDbContext>(options =>
        options.UseNpgsql(configuration.GetConnectionString("IdentityDbConnection"),
            npgsqlOptions => npgsqlOptions.MigrationsHistoryTable("__IdentityDbHistory")));
    builder.Services.AddDbContext<MainDbContext>(options =>
        options.UseNpgsql(configuration.GetConnectionString("MainDbConnection"),
            npgsqlOptions => npgsqlOptions.MigrationsHistoryTable("__MainDbHistory")));
}

// Infrastructure Repositories
builder.Services.AddRepositories();

// Domain Aggregates
builder.Services.AddDomainAggregates();

// Identity
builder.Services.AddIdentityCore<AppUser>(options =>
    {
        options.Password.RequireDigit = true;
        options.Password.RequireLowercase = true;
        options.Password.RequireUppercase = true;
        options.Password.RequireNonAlphanumeric = true;
        options.Password.RequiredLength = 12;
        options.User.RequireUniqueEmail = true;
        // TODO : Uncomment when email confirmation service is implemented
        //options.SignIn.RequireConfirmedEmail = true;
        options.Lockout.MaxFailedAccessAttempts = 5;
        options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
        options.Lockout.AllowedForNewUsers = true;
    })
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<IdentityDbContext>()
    .AddSignInManager<SignInManager<AppUser>>()
    .AddDefaultTokenProviders();

// Jwt
var jwtSection = configuration.GetSection("Jwt");
var jwtKey = jwtSection["Key"];

if (builder.Environment.IsEnvironment("IntegrationTest") && string.IsNullOrWhiteSpace(jwtKey))
{
    jwtKey = "integration-test-jwt-key-with-sufficient-length";
}

var key = Encoding.UTF8.GetBytes(jwtKey!);

builder.Services
    .AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.MapInboundClaims = false;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = !string.IsNullOrEmpty(jwtSection["Issuer"]),
            ValidateAudience = !string.IsNullOrEmpty(jwtSection["Audience"]),
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtSection["Issuer"],
            ValidAudience = jwtSection["Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ClockSkew = TimeSpan.FromMinutes(1),
            RoleClaimType = "role",
            NameClaimType = "sub"
        };
    });

builder.Services.AddAuthorization();
builder.Services.AddHttpLogging(options =>
{
    options.LoggingFields = HttpLoggingFields.RequestMethod |
                            HttpLoggingFields.RequestPath |
                            HttpLoggingFields.ResponseStatusCode |
                            HttpLoggingFields.Duration;
});

// CORS
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
        policy.AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod());
});

// API services
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IFreelanceAccessService, FreelanceAccessService>();
builder.Services.AddScoped<IResourceAuthorizationService, ResourceAuthorizationService>();

// FastEndpoints
if (!builder.Environment.IsEnvironment("IntegrationTest"))
{
    builder.Services.AddFastEndpoints(options => { options.Assemblies = [typeof(Program).Assembly]; })
        .SwaggerDocument(options =>
        {
            options.DocumentSettings = settings =>
            {
                settings.Title = "WordsmithHub API";
                settings.Version = "v1";
                settings.SchemaSettings.DefaultReferenceTypeNullHandling = ReferenceTypeNullHandling.NotNull;
                settings.SchemaSettings.GenerateXmlObjects = true;
            };

            options.ShortSchemaNames = true;
            options.AutoTagPathSegmentIndex = 0;
        });
}

var app = builder.Build();

if (!app.Environment.IsEnvironment("IntegrationTest"))
{
    using var scope = app.Services.CreateScope();
    var identityDb = scope.ServiceProvider.GetRequiredService<IdentityDbContext>();
    var mainDb = scope.ServiceProvider.GetRequiredService<MainDbContext>();

    identityDb.Database.Migrate();
    mainDb.Database.Migrate();
}

app.UseHttpsRedirection();

app.UseCors();

app.UseAuthentication();
app.UseAuthorization();

app.UseFastEndpoints(c =>
{
    c.Endpoints.Configurator = ep => { ep.PreProcessors(0, typeof(AppUserIdPreProcessor<>)); };
});

if (!app.Environment.IsEnvironment("IntegrationTest"))
{
    app.UseSwaggerGen();
    app.UseHttpLogging();
}

app.Run();
