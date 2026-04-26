using Microsoft.Extensions.DependencyInjection;

namespace WordsmithHub.Domain.FreelanceAggregate;

public static class Bootstrap
{
    extension(IServiceCollection services)
    {
        public IServiceCollection AddFreelanceAggregate()
        {
            services.AddScoped<IFreelanceFactory, FreelanceFactory>();
            return services;
        }
    }
}
