namespace ECommerceBackendTaskAPI.Features.Common.OrderLineItems.Dtos
{
    public class GetOrderLineItemDto
    {
        public int Quantity { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public string Description { get; set; }
        public decimal ProducPrice { get; set; }


    }
}
