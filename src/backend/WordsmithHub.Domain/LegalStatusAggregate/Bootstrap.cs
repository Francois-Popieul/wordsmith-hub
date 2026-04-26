using Microsoft.Extensions.DependencyInjection;

namespace WordsmithHub.Domain.LegalStatusAggregate;

public static class Bootstrap
{
    extension(IServiceCollection services)
    {
        public IServiceCollection AddLegalStatusAggregate()
        {
            services.AddScoped<ILegalStatusFactory, LegalStatusFactory>();
            return services;
        }
    }
}
