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

        /// <summary>
        /// Retrieves an order by its ID.
        /// </summary>
        /// <param name="OrderId">The ID of the order to retrieve.</param>
        /// <returns>A result view model containing the order details or an error message.</returns>
        [HttpGet]
        [SwaggerOperation(
            Summary = "Get Order by ID",
            Description = "Retrieves an order by its ID.",
            OperationId = "GetOrderById",
            Tags = new[] { "Orders" }
        )]
        [SwaggerResponse(200, "Order retrieved successfully", typeof(ResultViewModel))]
        [SwaggerResponse(404, "Order not found", typeof(ResultViewModel))]
        [SwaggerResponse(500, "Internal server error", typeof(ResultViewModel))]

        public async Task<ResultViewModel> GetOrderAsync(int OrderId)
        {
            var orderResult = await _mediator.Send(new GetOrderQuery(OrderId));

            if (orderResult.IsSuccess is false)
            {
                return ResultViewModel.Faliure(orderResult.ErrorCode, orderResult.Message);
            }

            return ResultViewModel.Sucess(orderResult.Data);
        }
    }
}
