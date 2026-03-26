using WordsmithHub.Infrastructure.MainDatabase;

namespace WordsmithHub.API.Features.DirectClient.Get;

public record GetDirectClientCommand(Guid DirectClientId);

public class GetDirectClientHandler(Repository<Domain.DirectClient> repository)
{
    public async Task<Domain.DirectClient?> HandleAsync(GetDirectClientCommand command,
        CancellationToken cancellationToken)
    {
        return await repository.GetByIdAsync(command.DirectClientId, cancellationToken);
    }
}
