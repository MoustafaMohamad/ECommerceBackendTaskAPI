using Swashbuckle.AspNetCore.Annotations;

namespace ECommerceBackendTaskAPI.Features.Orders.CreateOrder
{
    [ApiController]
    [Route("Orders/[action]")]
    public class CreateOrderEndpoint : ControllerBase
    {
        private readonly IMediator _mediator;
        public CreateOrderEndpoint(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("CreateOrder")]
        public async Task<ResultViewModel> CreateOrderAsync([FromBody]CreateOrderEndpointRequest createOrderEndpointRequest) {

            var result =await _mediator.Send(new CreateOrderOrchestrator(createOrderEndpointRequest.CustomerId , createOrderEndpointRequest.OrderLineItems));

            if (result.IsSuccess is false)
            {
                return ResultViewModel.Faliure(result.ErrorCode, result.Message);
            }

            return ResultViewModel.Sucess(result.Data);
        }
    }
}
