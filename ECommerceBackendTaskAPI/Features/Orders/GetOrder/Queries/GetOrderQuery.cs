

using ECommerceBackendTaskAPI.Entities;

namespace ECommerceBackendTaskAPI.Features.Orders.GetOrder.Queries
{
    public record GetOrderQuery(int OrderId):IRequest<ResultDto<GatOrderDto>>;

    public class GetOrderQueryHandeler : BaseRequestHandler<Order, GetOrderQuery, ResultDto<GatOrderDto>>
    {
        public GetOrderQueryHandeler(RequestParameters<Order> requestParameters) : base(requestParameters)
        {
        }

        public override async Task<ResultDto<GatOrderDto>> Handle(GetOrderQuery request, CancellationToken cancellationToken)
        {
            var orderQuery = _repository.Get( o => o.ID==request.OrderId);
            var orderDto = await orderQuery.Map<GatOrderDto>().AsSplitQuery().FirstOrDefaultAsync();

            if (orderDto is null)
            {
                return ResultDto<GatOrderDto>.Faliure(ErrorCode.NotFound, "This order not fount ");
            }
            return ResultDto<GatOrderDto>.Sucess(orderDto);
        }
    }
}
