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
        ///// <summary>
        ///// Endpoint to retrieve all orders with pagination.
        ///// </summary>
        /// <param name="page">The page number.</param>
        /// <param name="pageSize">The number of records per page.</param>
        /// <returns>A paged list of orders.</returns>
        [HttpGet]
        [SwaggerOperation(Summary = "Get all orders", Description = "Retrieve all orders with pagination")]
        [SwaggerResponse(200, "Successfully retrieved orders", typeof(ResultViewModel))]
        [SwaggerResponse(400, "Invalid input parameters")]
        [SwaggerResponse(500, "Internal server error")]
        public async Task<ResultViewModel> GetAllOrdersAsync([FromQuery] int page, [FromQuery] int pageSize)
        {
            var result = await _mediator.Send(new GetAllOrdersQuery(page, pageSize));
            return ResultViewModel.Sucess(result.Data.items);
        }



    }
}
