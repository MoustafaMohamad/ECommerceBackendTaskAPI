using Swashbuckle.AspNetCore.Annotations;

namespace ECommerceBackendTaskAPI.Features.Orders.CancelOrder
{
    [ApiController]
    [Route("Orders/[action]")]
    public class CancelOrderEndpoint : ControllerBase
    {
        private readonly IMediator _mediator;

        public CancelOrderEndpoint(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost("CancelOrder")]
        public async Task<ResultViewModel> CancelOrderAsync([FromBody] int OrderId)
        {
            var cancelResult = await _mediator.Send(new CancelOrderOrchestrator(OrderId));

            if (cancelResult.IsSuccess is false) 
            { 
                return ResultViewModel.Faliure(cancelResult.ErrorCode , cancelResult.Message);
            }
            return ResultViewModel.Sucess(true);
        }

    }
}
