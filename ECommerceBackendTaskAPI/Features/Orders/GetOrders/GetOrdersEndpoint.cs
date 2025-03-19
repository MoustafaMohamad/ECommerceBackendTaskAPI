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

        /// <summary>
        /// Retrieves all orders with pagination.
        /// </summary>
        /// <param name="page">The page number to retrieve.</param>
        /// <param name="pageSize">The number of orders per page.</param>
        /// <returns>A result view model containing the list of orders or an error message.</returns>
        [HttpGet]
        [SwaggerOperation(
            Summary = "Get All Orders",
            Description = "Retrieves all orders with pagination.",
            OperationId = "GetAllOrders",
            Tags = new[] { "Orders" }
        )]
        [SwaggerResponse(200, "Orders retrieved successfully", typeof(ResultViewModel))]
        [SwaggerResponse(400, "Invalid request parameters", typeof(ResultViewModel))]
        [SwaggerResponse(500, "Internal server error", typeof(ResultViewModel))]

        public async Task<ResultViewModel> GetAllOrdersAsync([FromQuery] int page, [FromQuery] int pageSize)
        {
            var result = await _mediator.Send(new GetAllOrdersQuery(page, pageSize));
            return ResultViewModel.Sucess(result.Data);
        }



    }
}
