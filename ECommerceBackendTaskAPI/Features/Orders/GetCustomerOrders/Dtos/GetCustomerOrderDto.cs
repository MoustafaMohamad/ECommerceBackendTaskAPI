namespace ECommerceBackendTaskAPI.Features.Orders.GetCustomerOrders.Dtos
{
    public record GetCustomerOrderDto(int ID , DateTime OrderDate , decimal TotalAmount , OrderStatus Status);
}
