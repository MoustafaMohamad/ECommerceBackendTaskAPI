using static ECommerceBackendTaskAPI.Common.PagedListQueryableExtensions;

namespace ECommerceBackendTaskAPI.Features.Orders.GetOrders.Queries
{
    public record GetAllOrdersQuery(int Page, int PageSize) : IRequest<ResultDto<PagedList<OrderDto>>>;

    public class GetAllOrdersQueryHandler : BaseRequestHandler<Order, GetAllOrdersQuery, ResultDto<PagedList<OrderDto>>>
    {
        public GetAllOrdersQueryHandler(RequestParameters<Order> requestParameters) : base(requestParameters)
        {
        }

        public override async Task<ResultDto<PagedList<OrderDto>>> Handle(GetAllOrdersQuery request, CancellationToken cancellationToken)
        {
            var ordersQuery = _repository.GetAll().Map<OrderDto>();
            var pagedOrders = await ordersQuery.ToPagedListAsync(request.Page, request.PageSize, cancellationToken);

            return ResultDto<PagedList<OrderDto>>.Sucess(pagedOrders);
        }
    }

}
