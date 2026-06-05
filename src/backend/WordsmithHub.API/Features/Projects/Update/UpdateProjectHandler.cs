using FastEndpoints;
using JetBrains.Annotations;
using WordsmithHub.API.Features.Common.Results;
using WordsmithHub.API.Services.ResourceAccessService;
using WordsmithHub.Domain.DirectCustomerAggregate;
using WordsmithHub.Domain.EndCustomerAggregate;
using WordsmithHub.Domain.FreelanceAggregate;
using WordsmithHub.Domain.ProjectAggregate;

namespace WordsmithHub.API.Features.Projects.Update;

public record UpdateProjectCommand(
    Guid ProjectId,
    string Name,
    string Domain,
    string? Description,
    Guid[] DirectCustomerIds,
    string EndCustomerName,
    Guid AppUserId) : ICommand<OperationResult<Guid>>;

[UsedImplicitly]
public class UpdateProjectHandler(
    IFreelanceRepository freelanceRepository,
    IProjectRepository projectRepository,
    IEndCustomerRepository endCustomerRepository,
    IDirectCustomerRepository directCustomerRepository,
    IEndCustomerFactory endCustomerFactory,
    IResourceAuthorizationService resourceAuthorizationService)
    : ICommandHandler<UpdateProjectCommand, OperationResult<Guid>>
{
    public async Task<OperationResult<Guid>> ExecuteAsync(UpdateProjectCommand command,
        CancellationToken cancellationToken)
    {
        var freelance = await freelanceRepository.GetByAppUserIdAsync(command.AppUserId, cancellationToken);

        if (freelance == null ||
            !await resourceAuthorizationService.CanAccessAsync<Project>(command.AppUserId, command.ProjectId,
                cancellationToken))
            return OperationResult.Forbidden<Guid>();

        List<DirectCustomer> directCustomers = [];

        foreach (var directCustomerId in command.DirectCustomerIds)
        {
            var directCustomer = await directCustomerRepository.GetByIdAsync(directCustomerId, cancellationToken);
            if (directCustomer != null) directCustomers.Add(directCustomer);
        }

        if (directCustomers.Count == 0)
            return OperationResult.Error<Guid>();

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

        var project = await projectRepository.GetByIdAsync(command.ProjectId, cancellationToken);

        if (project is null)
            return OperationResult.NotFound<Guid>();

        project.Name = command.Name;
        project.Domain = command.Domain;
        project.DirectCustomers = directCustomers;
        project.Description = command.Description;
        project.EndCustomerId = endCustomer?.Id;

        await projectRepository.UpdateInformationAsync(project, cancellationToken);

        return OperationResult.Success(project.Id);
    }
}
