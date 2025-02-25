
namespace ECommerceBackendTaskAPI.Features.Common.OrderLineItems.Queries
{
    public record GetProductsOfOrderLineItemsByOrderIdQuery(int OrderId) : IRequest<ResultDto<IEnumerable<ProductOfOrderLineItemDto>>>;

    public class GetProductsOfOrderLineItemsByOrderIdQueryHandler : BaseRequestHandler<OrderLineItem, GetProductsOfOrderLineItemsByOrderIdQuery, ResultDto<IEnumerable<ProductOfOrderLineItemDto>>>
    {
        public GetProductsOfOrderLineItemsByOrderIdQueryHandler(RequestParameters<OrderLineItem> requestParameters) : base(requestParameters)
        {
        }

        public override async Task<ResultDto<IEnumerable<ProductOfOrderLineItemDto>>> Handle(GetProductsOfOrderLineItemsByOrderIdQuery request, CancellationToken cancellationToken)
        {
            var ProductsOfOrderLineItemsQuery = _repository.Get(oli => oli.OrderId == request.OrderId);

            var productsOfOrderLineItems =await ProductsOfOrderLineItemsQuery
                .Map<ProductOfOrderLineItemDto>().ToListAsync();
            return ResultDto<IEnumerable<ProductOfOrderLineItemDto>>.Sucess(productsOfOrderLineItems);
        }
    }
}
