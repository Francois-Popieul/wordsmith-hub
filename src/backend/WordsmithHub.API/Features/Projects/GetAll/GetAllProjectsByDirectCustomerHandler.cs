using FastEndpoints;
using JetBrains.Annotations;
using WordsmithHub.API.Features.Common.Results;
using WordsmithHub.API.Features.Projects.Models;
using WordsmithHub.API.Features.Projects.Services;
using WordsmithHub.API.Services.ResourceAccessService;
using WordsmithHub.Domain.DirectCustomerAggregate;
using WordsmithHub.Domain.FreelanceAggregate;
using WordsmithHub.Domain.ProjectAggregate;

namespace WordsmithHub.API.Features.Projects.GetAll;

public record GetAllProjectsByDirectCustomerCommand(Guid AppUserId, Guid DirectCustomerId)
    : ICommand<OperationResult<IReadOnlyList<ProjectDto>>>;

[UsedImplicitly]
public class GetAllProjectsByDirectCustomerHandler(
    IResourceAuthorizationService resourceAuthorizationService,
    IFreelanceRepository freelanceRepository,
    IProjectRepository projectRepository)
    : ICommandHandler<GetAllProjectsByDirectCustomerCommand, OperationResult<IReadOnlyList<ProjectDto>>>
{
    public async Task<OperationResult<IReadOnlyList<ProjectDto>>> ExecuteAsync(
        GetAllProjectsByDirectCustomerCommand command,
        CancellationToken cancellationToken)
    {
        var freelance = await freelanceRepository.GetByAppUserIdAsync(command.AppUserId, cancellationToken);

        if (freelance == null || !await resourceAuthorizationService
                .CanAccessAsync<DirectCustomer>(command.AppUserId, command.DirectCustomerId, cancellationToken))
            return new OperationResult<IReadOnlyList<ProjectDto>>(OperationStatus.Forbidden);

        var projects = await projectRepository.GetByDirectCustomerIdAsync(command.DirectCustomerId, cancellationToken);

        if (projects.Count == 0)
            return new OperationResult<IReadOnlyList<ProjectDto>>(OperationStatus.Success, []);

        var projectDtoList = projects.Select(project => project.ToDto()).ToList();

        return new OperationResult<IReadOnlyList<ProjectDto>>(OperationStatus.Success, projectDtoList);
    }
}
