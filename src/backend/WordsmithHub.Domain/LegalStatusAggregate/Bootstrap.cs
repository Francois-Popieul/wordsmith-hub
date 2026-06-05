using JetBrains.Annotations;
using Microsoft.Extensions.DependencyInjection;

namespace WordsmithHub.Domain.LegalStatusAggregate;

public static class Bootstrap
{
    extension(IServiceCollection services)
    {
        [UsedImplicitly]
        public IServiceCollection AddLegalStatusAggregate()
        {
            services.AddScoped<ILegalStatusFactory, LegalStatusFactory>();
            return services;
        }
    }
}
