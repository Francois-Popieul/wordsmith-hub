using Microsoft.Extensions.DependencyInjection;
using WordsmithHub.Domain;
using WordsmithHub.Domain.BankAccountAggregate;
using WordsmithHub.Domain.DirectCustomerAggregate;
using WordsmithHub.Domain.EndCustomerAggregate;
using WordsmithHub.Domain.FreelanceAggregate;
using WordsmithHub.Domain.InvoiceAggregate;
using WordsmithHub.Domain.LegalStatusAggregate;
using WordsmithHub.Domain.OrderLineAggregate;
using WordsmithHub.Domain.ProjectAggregate;
using WordsmithHub.Domain.RateAggregate;
using WordsmithHub.Domain.WorkOrderAggregate;

namespace WordsmithHub.Infrastructure.MainDatabase.Repositories;

public static class Bootstrap
{
    extension(IServiceCollection services)
    {
        public IServiceCollection AddRepositories()
        {
            services.AddScoped<IBankAccountRepository, BankAccountRepository>();
            services.AddScoped<ICountryRepository, CountryRepository>();
            services.AddScoped<ICurrencyRepository, CurrencyRepository>();
            services.AddScoped<IDirectCustomerRepository, DirectCustomerRepository>();
            services.AddScoped<IEndCustomerRepository, EndCustomerRepository>();
            services.AddScoped<IFreelanceRepository, FreelanceRepository>();
            services.AddScoped<IInvoiceRepository, InvoiceRepository>();
            services.AddScoped<ILanguageRepository, LanguageRepository>();
            services.AddScoped<ILegalStatusRepository, LegalStatusRepository>();
            services.AddScoped<IOrderLineRepository, OrderLineRepository>();
            services.AddScoped<IProjectRepository, ProjectRepository>();
            services.AddScoped<IRateRepository, RateRepository>();
            services.AddScoped<IServiceRepository, ServiceRepository>();
            services.AddScoped<IWorkOrderRepository, WorkOrderRepository>();
            return services;
        }
    }
}
