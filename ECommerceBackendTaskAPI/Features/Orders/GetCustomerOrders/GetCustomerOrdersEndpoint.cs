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

        /// <summary>
        /// Retrieves orders for a specific customer by their ID.
        /// </summary>
        /// <param name="customerId">The ID of the customer whose orders are to be retrieved.</param>
        /// <returns>A result view model containing the list of customer orders or an error message.</returns>
        [HttpGet]
        [SwaggerOperation(
            Summary = "Get Customer Orders by ID",
            Description = "Retrieves orders for a specific customer by their ID.",
            OperationId = "GetCustomerOrdersById",
            Tags = new[] { "Orders" }
        )]
        [SwaggerResponse(200, "Customer orders retrieved successfully", typeof(ResultViewModel))]
        [SwaggerResponse(404, "Customer not found", typeof(ResultViewModel))]
        [SwaggerResponse(500, "Internal server error", typeof(ResultViewModel))]

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
