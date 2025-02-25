namespace ECommerceBackendTaskAPI.Features.Orders.CreateOrder
{
    public record CreateOrderEndpointRequest(int CustomerId, IEnumerable<OrderLineItemDto> OrderLineItems);
}
