namespace ECommerceBackendTaskAPI.Features.Orders.GetOrders.Dtos
{
    public class OrderDto
    {
        public int ID { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public OrderStatus Status { get; set; }
    }
}
