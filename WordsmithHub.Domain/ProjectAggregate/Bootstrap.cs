using Microsoft.Extensions.DependencyInjection;

namespace WordsmithHub.Domain.ProjectAggregate;

public static class Bootstrap
{
    extension(IServiceCollection services)
    {
        public IServiceCollection AddProjectAggregate()
        {
            services.AddScoped<IProjectFactory, ProjectFactory>();
            return services;
        }
    }
}
