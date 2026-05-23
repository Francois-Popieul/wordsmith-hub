using FastEndpoints;
using JetBrains.Annotations;
using WordsmithHub.API.Features.Common.Results;
using WordsmithHub.API.Services.ResourceAccessService;
using WordsmithHub.Domain.ProjectAggregate;
using WordsmithHub.Domain.FreelanceAggregate;

namespace WordsmithHub.API.Features.Projects.Delete;

public record DeleteProjectCommand(Guid AppUserId, Guid ProjectId) : ICommand<OperationResult<NoContent>>;

[UsedImplicitly]
public class DeleteProjectHandler(
    IFreelanceRepository freelanceRepository,
    IResourceAuthorizationService resourceAuthorizationService,
    IProjectRepository repository)
    : ICommandHandler<DeleteProjectCommand, OperationResult<NoContent>>
{
    public async Task<OperationResult<NoContent>> ExecuteAsync(DeleteProjectCommand command,
        CancellationToken cancellationToken)
    {
        var freelance = await freelanceRepository.GetByAppUserIdAsync(command.AppUserId, cancellationToken);

        if (freelance == null ||
            !await resourceAuthorizationService.CanAccessAsync<Project>(command.AppUserId,
                command.ProjectId, cancellationToken))
        {
            return OperationResult.Forbidden<NoContent>();
        }

        var project = await repository.GetByIdAsync(command.ProjectId, cancellationToken);

        if (project == null)
        {
            return OperationResult.NotFound<NoContent>();
        }

        project.MarkAsDeleted();

        await repository.ArchiveAsync(project, cancellationToken);

        return OperationResult.Success(new NoContent());
    }
}
