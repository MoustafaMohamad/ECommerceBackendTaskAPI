namespace ECommerceBackendTaskAPI.Features.Orders.CreateOrder.Events
{
    public record OrderCreatedEvent(int OrderId, decimal TotalAmount) : INotification;
}
