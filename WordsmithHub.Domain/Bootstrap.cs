using Microsoft.Extensions.DependencyInjection;
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

namespace WordsmithHub.Domain;

public static class Bootstrap
{
    extension(IServiceCollection services)
    {
        public IServiceCollection AddDomainAggregates()
        {
            services.AddBankAccountAggregate();
            services.AddDirectCustomerAggregate();
            services.AddEndCustomerAggregate();
            services.AddFreelanceAggregate();
            services.AddInvoiceAggregate();
            services.AddLegalStatusAggregate();
            services.AddOrderLineAggregate();
            services.AddProjectAggregate();
            services.AddRateAggregate();
            services.AddWorkOrderAggregate();
            return services;
        }
    }
}
