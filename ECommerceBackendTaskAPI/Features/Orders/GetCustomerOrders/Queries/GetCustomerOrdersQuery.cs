
namespace ECommerceBackendTaskAPI.Features.Orders.GetCustomerOrders.Queries
{
    public record  GetCustomerOrdersQuery(int CustomerId) :IRequest<ResultDto<IEnumerable<GetCustomerOrderDto>>>;
    public class GetCustomerOrdersQueryHandler : BaseRequestHandler<Order, GetCustomerOrdersQuery, ResultDto<IEnumerable<GetCustomerOrderDto>>>
    {
        public GetCustomerOrdersQueryHandler(RequestParameters<Order> requestParameters) : base(requestParameters)
        {
        }

        public override async Task<ResultDto<IEnumerable<GetCustomerOrderDto>>> Handle(GetCustomerOrdersQuery request, CancellationToken cancellationToken)
        {
            var customer = await _mediator.Send(new IsCustomerExists(request.CustomerId));
            if (customer.IsSuccess == false)
            {
                return ResultDto<IEnumerable<GetCustomerOrderDto>>.Faliure(customer.ErrorCode, customer.Message);
            }
            var orderQuery = _repository.Get(order => order.CustomerId == request.CustomerId).Map<GetCustomerOrderDto>();
            if (await orderQuery.AnyAsync() is false ) 
            { 
                return ResultDto<IEnumerable<GetCustomerOrderDto>>.Sucess(Enumerable.Empty<GetCustomerOrderDto>());
            }

            return ResultDto<IEnumerable<GetCustomerOrderDto>>.Sucess(orderQuery);
        }
    }


}
