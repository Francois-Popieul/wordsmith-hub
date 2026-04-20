using Microsoft.Extensions.DependencyInjection;
using WordsmithHub.Domain.BankAccountAggregate;
using WordsmithHub.Domain.DirectCustomerAggregate;
using WordsmithHub.Domain.FreelanceAggregate;

namespace WordsmithHub.Infrastructure.MainDatabase.Repositories;

public static class Bootstrap
{
    extension(IServiceCollection services)
    {
        public IServiceCollection AddRepositories()
        {
            services.AddScoped<IBankAccountRepository, BankAccountRepository>();
            services.AddScoped<IDirectCustomerRepository, DirectCustomerRepository>();
            //services.AddScoped<IEndCustomerRepository, EndCustomerRepository>();
            services.AddScoped<IFreelanceRepository, FreelanceRepository>();
            //services.AddScoped<IInvoiceRepository, InvoiceRepository>();
            //services.AddScoped<IOrderLineRepository, OrderLineRepository>();
            //services.AddScoped<IProjectRepository, ProjectRepository>();
            //services.AddScoped<IRateRepository, RateRepository>();
            //services.AddScoped<IWorkOrderRepository, WorkOrderRepository>();
            return services;
        }
    }
}
