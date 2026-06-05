using FastEndpoints;
using JetBrains.Annotations;
using WordsmithHub.API.Features.Common.Results;
using WordsmithHub.API.Features.Projects.Models;
using WordsmithHub.API.Features.Projects.Services;
using WordsmithHub.Domain.FreelanceAggregate;
using WordsmithHub.Domain.ProjectAggregate;

namespace WordsmithHub.API.Features.Projects.GetAll;

public record GetAllProjectsCommand(Guid AppUserId) : ICommand<OperationResult<IReadOnlyList<ProjectDto>>>;

[UsedImplicitly]
public class GetAllProjectsHandler(
    IFreelanceRepository freelanceRepository,
    IProjectRepository projectRepository)
    : ICommandHandler<GetAllProjectsCommand, OperationResult<IReadOnlyList<ProjectDto>>>
{
    public async Task<OperationResult<IReadOnlyList<ProjectDto>>> ExecuteAsync(
        GetAllProjectsCommand command,
        CancellationToken cancellationToken)
    {
        var freelance = await freelanceRepository.GetByAppUserIdAsync(command.AppUserId, cancellationToken);

        if (freelance == null)
            return new OperationResult<IReadOnlyList<ProjectDto>>(OperationStatus.Forbidden);

        var projects = await projectRepository.GetByFreelanceIdAsync(freelance.Id, cancellationToken);

        if (projects.Count == 0)
            return new OperationResult<IReadOnlyList<ProjectDto>>(OperationStatus.Success, []);

        var projectDtoList = projects.Select(project => project.ToDto()).ToList();

        return new OperationResult<IReadOnlyList<ProjectDto>>(OperationStatus.Success, projectDtoList);
    }
}
