using Microsoft.Extensions.DependencyInjection;

namespace WordsmithHub.Domain.WorkOrderAggregate;

public static class Bootstrap
{
    extension(IServiceCollection services)
    {
        public IServiceCollection AddWorkOrderAggregate()
        {
            services.AddScoped<IWorkOrderFactory, WorkOrderFactory>();
            return services;
        }
    }
}
