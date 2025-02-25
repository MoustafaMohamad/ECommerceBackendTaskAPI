using ECommerceBackendTaskAPI.Features.Orders.GetCustomerOrders.Queries;

namespace ECommerceBackendTaskAPI.Features.Orders.GetCustomerOrders
{
    [ApiController]
    [Route("Orders/[action]")]
    public class GetCustomerOrdersEndpoint : ControllerBase
    {
        private readonly IMediator _mediator;

        public GetCustomerOrdersEndpoint(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{customerId:int}")]
        public async Task<ResultViewModel> GetCustomerOrdersAsync(int customerId) 
        {
            var result = await _mediator.Send(new GetCustomerOrdersQuery(customerId));

            if (result.IsSuccess is false)
            {
                return ResultViewModel.Faliure(result.ErrorCode, result.Message);
            }

            return ResultViewModel.Sucess(result.Data);

        }


    }
}
