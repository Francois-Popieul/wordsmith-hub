using Microsoft.Extensions.DependencyInjection;

namespace WordsmithHub.Domain.DirectCustomerAggregate;

public static class Bootstrap
{
    extension(IServiceCollection services)
    {
        public IServiceCollection AddDirectCustomerAggregate()
        {
            services.AddScoped<IDirectCustomerFactory, DirectCustomerFactory>();
            return services;
        }
    }
}
