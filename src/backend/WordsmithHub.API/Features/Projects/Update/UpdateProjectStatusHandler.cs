using FastEndpoints;
using WordsmithHub.API.Features.Common.Results;
using WordsmithHub.Domain.FreelanceAggregate;
using WordsmithHub.Domain.ProjectAggregate;

namespace WordsmithHub.API.Features.Projects.Update;

public record UpdateProjectStatusCommand(Guid AppUserId, Guid ProjectId, int StatusId)
    : ICommand<OperationResult<Guid>>;

public class UpdateProjectStatusHandler(
    IFreelanceRepository freelanceRepository,
    IProjectRepository projectRepository) : ICommandHandler<UpdateProjectStatusCommand, OperationResult<Guid>>
{
    public async Task<OperationResult<Guid>> ExecuteAsync(UpdateProjectStatusCommand command,
        CancellationToken cancellationToken)
    {
        var freelance = await freelanceRepository.GetByAppUserIdAsync(command.AppUserId, cancellationToken);

        if (freelance == null)
            return OperationResult.Forbidden<Guid>();

        var project = await projectRepository.GetByIdAsync(command.ProjectId, cancellationToken);

        if (project == null)
            return OperationResult.NotFound<Guid>();

        project.StatusId = command.StatusId;
        project.UpdatedAt = DateTime.UtcNow;

        await projectRepository.UpdateStatusAsync(project, cancellationToken);

        return OperationResult.Success(project.Id);
    }
}
