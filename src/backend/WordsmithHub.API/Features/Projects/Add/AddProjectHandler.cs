using FastEndpoints;
using JetBrains.Annotations;
using WordsmithHub.API.Features.Common.Results;
using WordsmithHub.Domain.DirectCustomerAggregate;
using WordsmithHub.Domain.EndCustomerAggregate;
using WordsmithHub.Domain.FreelanceAggregate;
using WordsmithHub.Domain.ProjectAggregate;

namespace WordsmithHub.API.Features.Projects.Add;

public record AddProjectCommand(
    string Name,
    string Domain,
    string? Description,
    Guid[] DirectCustomerIds,
    string EndCustomerName,
    Guid AppUserId) : ICommand<OperationResult<Guid>>;

[UsedImplicitly]
public class AddProjectHandler(
    IFreelanceRepository freelanceRepository,
    IProjectRepository projectRepository,
    IEndCustomerRepository endCustomerRepository,
    IDirectCustomerRepository directCustomerRepository,
    IProjectFactory projectFactory,
    IEndCustomerFactory endCustomerFactory) : ICommandHandler<AddProjectCommand, OperationResult<Guid>>
{
    public async Task<OperationResult<Guid>> ExecuteAsync(AddProjectCommand command,
        CancellationToken cancellationToken)
    {
        var freelance = await freelanceRepository.GetByAppUserIdAsync(command.AppUserId, cancellationToken);

        if (freelance == null)
        {
            return OperationResult.Forbidden<Guid>();
        }

        List<DirectCustomer> directCustomers = [];

        foreach (var directCustomerId in command.DirectCustomerIds)
        {
            var directCustomer = await directCustomerRepository.GetByIdAsync(directCustomerId, cancellationToken);
            if (directCustomer != null) directCustomers.Add(directCustomer);
        }

        if (directCustomers.Count == 0)
        {
            return OperationResult.NotFound<Guid>();
        }

        EndCustomer? endCustomer = null;

        var endCustomerExists =
            await endCustomerRepository.ExistsWithNameAsync(freelance.Id, command.EndCustomerName, cancellationToken);

        switch (endCustomerExists)
        {
            case false when string.IsNullOrWhiteSpace(command.EndCustomerName):
                endCustomer = null;
                break;
            case false when !string.IsNullOrWhiteSpace(command.EndCustomerName):
                endCustomer = endCustomerFactory.CreateEndCustomer(command.EndCustomerName);
                await endCustomerRepository.AddAsync(endCustomer, cancellationToken);
                break;
            case true:
                endCustomer = await endCustomerRepository.GetByNameAsync(command.EndCustomerName, cancellationToken);
                break;
        }

        var project = projectFactory.CreateProject(
            command.Name,
            command.Domain,
            command.Description,
            freelance.Id,
            endCustomer?.Id,
            directCustomers);

        await projectRepository.AddAsync(project, cancellationToken);

        return OperationResult.Success(project.Id);
    }
}
