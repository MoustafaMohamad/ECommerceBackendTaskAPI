namespace ECommerceBackendTaskAPI.Features.Orders.UpdateOrderStatus.Events
{
    public record OrderStatusUpdatedEvent(int OrderId, OrderStatus NewStatus, DateTime UpdatedAt) : INotification;


}
