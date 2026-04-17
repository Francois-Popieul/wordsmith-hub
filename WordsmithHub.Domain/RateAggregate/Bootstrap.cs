using Microsoft.Extensions.DependencyInjection;

namespace WordsmithHub.Domain.RateAggregate;

public static class Bootstrap
{
    extension(IServiceCollection services)
    {
        public IServiceCollection AddRateAggregate()
        {
            services.AddScoped<IRateFactory, RateFactory>();
            return services;
        }
    }
}
