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

        /// <summary>
        /// Creates a new order.
        /// </summary>
        /// <param name="createOrderEndpointRequest">The request model containing order details.</param>
        /// <returns>A result view model containing the created order details or an error message.</returns>
        [HttpPost]
        [SwaggerOperation(
            Summary = "Create a new order",
            Description = "Creates a new order with the specified details.",
            OperationId = "CreateOrder",
            Tags = new[] { "Orders" }
        )]
        [SwaggerResponse(200, "Order created successfully", typeof(ResultViewModel))]
        [SwaggerResponse(400, "Invalid request parameters", typeof(ResultViewModel))]
        [SwaggerResponse(500, "Internal server error", typeof(ResultViewModel))]
        public async Task<ResultViewModel> CreateOrderAsync([FromBody] CreateOrderEndpointRequest createOrderEndpointRequest)
        {

            var result = await _mediator.Send(new CreateOrderOrchestrator(createOrderEndpointRequest.CustomerId, createOrderEndpointRequest.OrderLineItems));

            if (result.IsSuccess is false)
            {
                return ResultViewModel.Faliure(result.ErrorCode, result.Message);
            }

            return ResultViewModel.Sucess(result.Data);
        }
    }
}
