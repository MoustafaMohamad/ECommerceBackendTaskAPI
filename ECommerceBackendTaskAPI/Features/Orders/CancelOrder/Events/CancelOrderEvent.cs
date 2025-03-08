namespace ECommerceBackendTaskAPI.Features.Orders.CancelOrder.Events
{
    public record CancelOrderEvent(int OrderId) : INotification;
}
