using AutoMapper;
using ECommerceBackendTaskAPI.Features.Orders.CreateOrder.Commands;
using ECommerceBackendTaskAPI.Features.Orders.CreateOrder;

namespace ECommerceBackendTaskAPI.Common.Helpers
{
    public class MappingProfile :Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateOrderEndpointRequest, CreateOrderCommand>().ReverseMap();
            CreateMap<Order, GatOrderDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.ID))
            .ForMember(dest => dest.CustomerId, opt => opt.MapFrom(src => src.CustomerId))
            .ForMember(dest => dest.TotalAmount, opt => opt.MapFrom(src => src.TotalAmount))
            .ForMember(dest => dest.OrderDate, opt => opt.MapFrom(src => src.OrderDate))
            .ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src => src.Customer.Name))
            .ForMember(dest => dest.OrderLineItems, opt => opt.MapFrom(src => src.OrderLineItems));

            // Mapping from OrderLineItem to GetOrderLineItemDto
            CreateMap<OrderLineItem, GetOrderLineItemDto>()
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.Name))
                .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Product.Description))
                .ForMember(dest => dest.ProducPrice, opt => opt.MapFrom(src => src.Product.Price));

            CreateMap<OrderLineItem, ProductOfOrderLineItemDto>();


            CreateMap<Order, GetCustomerOrderDto>()
                .ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.ID))
                .ForMember(dest => dest.OrderDate, opt => opt.MapFrom(src => src.OrderDate))
                .ForMember(dest => dest.TotalAmount, opt => opt.MapFrom(src => src.TotalAmount))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status));
        }
    }
}
