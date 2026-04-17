using Microsoft.Extensions.DependencyInjection;

namespace WordsmithHub.Domain.InvoiceAggregate;

public static class Bootstrap
{
    extension(IServiceCollection services)
    {
        public IServiceCollection AddInvoiceAggregate()
        {
            services.AddScoped<IInvoiceFactory, InvoiceFactory>();
            return services;
        }
    }
}
