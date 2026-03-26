using System.Text;
using FastEndpoints;
using FastEndpoints.Swagger;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NJsonSchema.Generation;
using WordsmithHub.API.Features.Authentication;
using WordsmithHub.API.Features.DirectClient.Get;
using WordsmithHub.API.Features.User.Get;
using WordsmithHub.API.Services;
using WordsmithHub.Infrastructure.IdentityDatabase;
using WordsmithHub.Infrastructure.MainDatabase;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

// DbContexts
builder.Services.AddDbContext<IdentityDbContext>(options =>
    options.UseNpgsql(configuration.GetConnectionString("IdentityDbConnection"),
        npgsqlOptions => npgsqlOptions.MigrationsHistoryTable("__IdentityDbHistory")));
builder.Services.AddDbContext<MainDbContext>(options =>
    options.UseNpgsql(configuration.GetConnectionString("MainDbConnection"),
        npgsqlOptions => npgsqlOptions.MigrationsHistoryTable("__MainDbHistory")));

// Identity
builder.Services.AddIdentityCore<AppUser>(options =>
    {
        options.Password.RequireDigit = true;
        options.Password.RequireLowercase = true;
        options.Password.RequireUppercase = true;
        options.Password.RequireNonAlphanumeric = true;
        options.Password.RequiredLength = 8;
        options.User.RequireUniqueEmail = true;
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
var key = Encoding.UTF8.GetBytes(jwtSection["Key"]!);

builder.Services
    .AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
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
            RoleClaimType = "http://schemas.microsoft.com/ws/2008/06/identity/claims/role",
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
builder.Services.AddScoped<RegisterUserHandler>();
builder.Services.AddScoped<LoginUserHandler>();
builder.Services.AddScoped<GetUserHandler>();
builder.Services.AddScoped<GetDirectClientHandler>();
builder.Services.AddScoped(typeof(Repository<>));
builder.Services.AddScoped<ITokenService, TokenService>();

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

if (!app.Environment.IsEnvironment("IntegrationTest"))
{
    app.UseFastEndpoints().UseSwaggerGen();
    app.UseHttpLogging();
}

app.Run();