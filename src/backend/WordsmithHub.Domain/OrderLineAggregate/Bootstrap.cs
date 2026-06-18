using JetBrains.Annotations;
using Microsoft.Extensions.DependencyInjection;

namespace WordsmithHub.Domain.OrderLineAggregate;

public static class Bootstrap
{
    extension(IServiceCollection services)
    {
        [UsedImplicitly]
        public IServiceCollection AddOrderLineAggregate()
        {
            services.AddScoped<IOrderLineFactory, OrderLineFactory>();
            return services;
        }
    }
}
