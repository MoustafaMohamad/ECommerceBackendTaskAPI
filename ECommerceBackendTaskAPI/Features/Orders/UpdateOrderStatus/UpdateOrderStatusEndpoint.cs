using ECommerceBackendTaskAPI.Features.Orders.UpdateOrderStatus.Commands;

namespace ECommerceBackendTaskAPI.Features.Orders.UpdateOrderStatus
{
    [ApiController]
    [Route("Orders/[action]")]
    public class UpdateOrderStatusEndpoint : ControllerBase
    {
        private readonly IMediator _mediator;

        public UpdateOrderStatusEndpoint(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Updates the status of an order.
        /// </summary>
        /// <param name="OrderId">The ID of the order to update.</param>
        /// <param name="Status">The new status of the order.</param>
        /// <returns>A result view model indicating the success or failure of the operation.</returns>
        [HttpPut]
        [SwaggerOperation(
            Summary = "Update order status",
            Description = "Updates the status of an order.",
            OperationId = "UpdateOrderStatus",
            Tags = new[] { "Orders" }
        )]
        [SwaggerResponse(200, "Order status updated successfully", typeof(ResultViewModel))]
        [SwaggerResponse(400, "Invalid request parameters", typeof(ResultViewModel))]
        [SwaggerResponse(500, "Internal server error", typeof(ResultViewModel))]

        public async Task<ResultViewModel> UpdateOrderStatusAsync(int OrderId, OrderStatus Status)
        {
            var result = await _mediator.Send(new UpdateOrderStatusCommand(OrderId, Status));
            if (result.IsSuccess is false)
            {
                return ResultViewModel.Faliure(result.ErrorCode, result.Message);
            }
            return ResultViewModel.Sucess("Order status updated successfully");
        }
    }
}
