using WordsmithHub.API.Features.DirectCustomers.Services;
using WordsmithHub.API.Features.Projects.Services;
using WordsmithHub.API.Features.WorkOrders.Models;
using WordsmithHub.Domain.WorkOrderAggregate;

namespace WordsmithHub.API.Features.WorkOrders.Services;

public static class WorkOrderExtensions
{
    public static WorkOrderDto ToDto(this WorkOrder workOrder)
    {
        ArgumentNullException.ThrowIfNull(workOrder);

        return new WorkOrderDto
        {
            Id = workOrder.Id,
            Reference = workOrder.Reference,
            DirectCustomer = workOrder.DirectCustomer!.ToDto(),
            Project = workOrder.Project!.ToDto(),
            StartDate = workOrder.StartDate,
            DeliveryDate = workOrder.DeliveryDate,
            StatusId = workOrder.StatusId,
        };
    }
}
