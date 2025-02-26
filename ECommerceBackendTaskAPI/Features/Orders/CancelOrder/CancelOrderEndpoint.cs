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
        /// <summary>
        /// Cancels an order by its ID.
        /// </summary>
        /// <param name="OrderId">The ID of the order to cancel.</param>
        /// <returns>A result view model indicating the success or failure of the operation.</returns>
        [HttpPut]
        [SwaggerOperation(
            Summary = "Cancel an order",
            Description = "Cancels an order by its ID.",
            OperationId = "CancelOrder",
            Tags = new[] { "Orders" }
        )]
        [SwaggerResponse(200, "Order cancelled successfully", typeof(ResultViewModel))]
        [SwaggerResponse(400, "Invalid request parameters", typeof(ResultViewModel))]
        [SwaggerResponse(500, "Internal server error", typeof(ResultViewModel))]

        public async Task<ResultViewModel> CancelOrderAsync([FromBody] int OrderId)
        {
            var cancelResult = await _mediator.Send(new CancelOrderOrchestrator(OrderId));

            if (cancelResult.IsSuccess is false)
            {
                return ResultViewModel.Faliure(cancelResult.ErrorCode, cancelResult.Message);
            }
            return ResultViewModel.Sucess(true);
        }

    }
}
