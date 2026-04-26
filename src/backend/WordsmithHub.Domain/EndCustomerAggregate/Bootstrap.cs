using Microsoft.Extensions.DependencyInjection;

namespace WordsmithHub.Domain.EndCustomerAggregate;

public static class Bootstrap
{
    extension(IServiceCollection services)
    {
        public IServiceCollection AddEndCustomerAggregate()
        {
            services.AddScoped<IEndCustomerFactory, EndCustomerFactory>();
            return services;
        }
    }
}
