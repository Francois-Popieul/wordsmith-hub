namespace WordsmithHub.Domain.WorkOrderAggregate;

public interface IWorkOrderFactory
{
    WorkOrder CreateWorkOrder(
        string reference,
        Guid projectId,
        Guid freelanceId,
        Guid directCustomerId,
        DateTime startDate,
        DateTime deliveryDate,
        string? notes,
        int statusId);
}

public class WorkOrderFactory : IWorkOrderFactory
{
    public WorkOrder CreateWorkOrder(
        string reference,
        Guid projectId,
        Guid freelanceId,
        Guid directCustomerId,
        DateTime startDate,
        DateTime deliveryDate,
        string? notes,
        int statusId)

    {
        var workOrder = new WorkOrder
        {
            Id = Guid.NewGuid(),
            Reference = reference,
            ProjectId = projectId,
            FreelanceId = freelanceId,
            DirectCustomerId = directCustomerId,
            StartDate = startDate,
            DeliveryDate = deliveryDate,
            StatusId = statusId,
            Notes = notes ?? string.Empty,
            CreatedAt = DateTimeOffset.UtcNow,
            UpdatedAt = DateTimeOffset.UtcNow
        };

        return workOrder;
    }
}
