using WordsmithHub.API.Features.Common.Results;
using WordsmithHub.API.Features.Freelances.Models;
using WordsmithHub.API.Features.Freelances.Services;
using WordsmithHub.Domain.FreelanceAggregate;

namespace WordsmithHub.API.Features.Freelances.GetAll;

public class GetAllFreelancesHandler(IFreelanceRepository freelanceRepository)
{
    public async Task<OperationResult<IReadOnlyList<FreelanceDto>>> HandleAsync(CancellationToken cancellationToken)
    {
        var freelances = await freelanceRepository.GetAllAsync(cancellationToken);

        if (freelances.Count == 0)
        {
            return OperationResult.NotFound<IReadOnlyList<FreelanceDto>>();
        }

        var freelanceDtoList = freelances.Select(freelance => freelance.ToDto()).ToList();

        return new OperationResult<IReadOnlyList<FreelanceDto>>(OperationStatus.Success, freelanceDtoList);
    }
}
