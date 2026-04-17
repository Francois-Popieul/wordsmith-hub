using Microsoft.Extensions.DependencyInjection;

namespace WordsmithHub.Domain.BankAccountAggregate;

public static class Bootstrap
{
    extension(IServiceCollection services)
    {
        public IServiceCollection AddBankAccountAggregate()
        {
            services.AddScoped<IBankAccountFactory, BankAccountFactory>();
            return services;
        }
    }
}
