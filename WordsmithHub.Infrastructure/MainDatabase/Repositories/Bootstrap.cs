using Microsoft.Extensions.DependencyInjection;
using WordsmithHub.Domain.DirectCustomerAggregate;
using WordsmithHub.Domain.FreelanceAggregate;

namespace WordsmithHub.Infrastructure.MainDatabase.Repositories;

public static class Bootstrap
{
    extension(IServiceCollection services)
    {
        public IServiceCollection AddRepositories()
        {
            services.AddScoped<IDirectCustomerRepository, DirectCustomerRepository>();
            services.AddScoped<IFreelanceRepository, FreelanceRepository>();
            return services;
        }
    }
}
