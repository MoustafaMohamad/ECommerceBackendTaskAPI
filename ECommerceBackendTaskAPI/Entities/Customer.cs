namespace ECommerceBackendTaskAPI.Entities
{
    public class Customer :BaseModel 
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
    }
}
