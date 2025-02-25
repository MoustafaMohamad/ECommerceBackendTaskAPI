using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace ECommerceBackendTaskAPI.Features.Orders.GetOrder
{
    [ApiController]
    [Route("Orders/[action]")]
    public class GetOrderEndpoint : ControllerBase
    {
        private readonly IMediator _mediator;

        public GetOrderEndpoint(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{orderId:int}")]
        public async Task<ResultViewModel> GetOrderAsync(int OrderId)
        {
            var orderResult = await _mediator.Send(new GetOrderQuery(OrderId));

            if (orderResult.IsSuccess is false) {
                return ResultViewModel.Faliure(orderResult.ErrorCode , orderResult.Message);
            }

            return ResultViewModel.Sucess(orderResult.Data);
        }
    }
}
