namespace ECommerceBackendTaskAPI.Features.Orders.GetOrder.Dtos
{
    public class GatOrderDto
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime OrderDate { get; set; }
        public string CustomerName { get; set; } 
        public IEnumerable<GetOrderLineItemDto> OrderLineItems { get; set; }
    }
}
