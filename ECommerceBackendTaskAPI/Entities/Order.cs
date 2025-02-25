namespace ECommerceBackendTaskAPI.Entities
{
    public class Order : BaseModel
    {
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public OrderStatus Status { get; set; }
        public IEnumerable<OrderLineItem> OrderLineItems { get; set; }
    }
}
