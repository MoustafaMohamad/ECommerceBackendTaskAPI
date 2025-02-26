namespace ECommerceBackendTaskAPI.Features.Orders.Events
{
    public record OrderCreatedEvent(int OrderId, decimal TotalAmount) : INotification;
}
