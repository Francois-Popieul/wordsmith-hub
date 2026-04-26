using Microsoft.Extensions.DependencyInjection;

namespace WordsmithHub.Domain.OrderLineAggregate;

public static class Bootstrap
{
    extension(IServiceCollection services)
    {
        public IServiceCollection AddOrderLineAggregate()
        {
            services.AddScoped<IOrderLineFactory, OrderLineFactory>();
            return services;
        }
    }
}
