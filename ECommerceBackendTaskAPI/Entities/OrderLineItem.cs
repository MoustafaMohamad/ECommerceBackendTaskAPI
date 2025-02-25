namespace ECommerceBackendTaskAPI.Entities
{
    public class OrderLineItem :BaseModel
    {
        public int Quantity { get; set; }
        public int OrderId { get; set; }
        public Order Order { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}
