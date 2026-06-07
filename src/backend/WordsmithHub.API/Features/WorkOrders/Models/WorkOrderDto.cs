using WordsmithHub.API.Features.DirectCustomers.Models;
using WordsmithHub.API.Features.Projects.Models;
using WordsmithHub.Domain.DirectCustomerAggregate;
using WordsmithHub.Domain.ProjectAggregate;

namespace WordsmithHub.API.Features.WorkOrders.Models;

public record WorkOrderDto
{
    public Guid Id { get; set; }
    public required string Reference { get; set; }
    public required DirectCustomerDto DirectCustomer { get; set; }
    public required ProjectDto Project { get; set; }
    public required DateTime StartDate { get; set; }
    public required DateTime DeliveryDate { get; set; }
    public required int StatusId { get; set; }
};
