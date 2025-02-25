using ECommerceBackendTaskAPI.Features.Orders.GetOrders.Queries;

namespace ECommerceBackendTaskAPI.Features.Orders.GetOrders
{
    [ApiController]
    [Route("Orders/[action]")]
    public class GetAllOrdersEndpoint
    {
        private readonly IMediator _mediator;

        public GetAllOrdersEndpoint(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ResultViewModel> GetAllOrdersAsync([FromQuery] int page, [FromQuery] int pageSize)
        {
            var result = await _mediator.Send(new GetAllOrdersQuery(page, pageSize));
            return ResultViewModel.Sucess(result.Data.items);
        }



    }
}
