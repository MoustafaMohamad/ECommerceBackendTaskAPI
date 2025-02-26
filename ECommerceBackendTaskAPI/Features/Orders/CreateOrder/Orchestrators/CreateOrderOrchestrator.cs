namespace ECommerceBackendTaskAPI.Features.Orders.CreateOrder.Orchestrators
{
    public record CreateOrderOrchestrator(int CustomerId, IEnumerable<OrderLineItemDto> OrderLineItems) : IRequest<ResultDto<bool>>;

    public class CreateOrderOrchestratorHandeler : BaseOrchestratorHandler<CreateOrderOrchestrator, ResultDto<bool>>
    {
        public CreateOrderOrchestratorHandeler(OrchestratorParameters orchestratorParameters) : base(orchestratorParameters)
        {
        }

        public override async Task<ResultDto<bool>> Handle(CreateOrderOrchestrator request, CancellationToken cancellationToken)
        {
            var customer = await _mediator.Send(new IsCustomerExists(request.CustomerId));
            if (customer.IsSuccess == false)
            {
                return ResultDto<bool>.Faliure(customer.ErrorCode, customer.Message);
            }

            decimal totalAmount = 0;

            var orderLineItems = new List<OrderLineItem>();

            foreach (var item in request.OrderLineItems)
            {
                var productResult = await _mediator.Send(new GetProductByIdQuery(item.ProductId));

                if (productResult.IsSuccess is false)
                {
                    // ////////////////////////
                    throw new BusinessException(ErrorCode.NotFound, $"Product with ID {item.ProductId} not found");
                }
                var product = productResult.Data;
                if (product.StockQuantity < item.Quantity)
                {
                    return ResultDto<bool>.Faliure(ErrorCode.BadRequest, $"Not enough stock for product {product.Name}");
                }

                totalAmount += product.Price * item.Quantity;

                var decreasingResult = await _mediator.Send(new DecreaseProductQuntityCommand(product.ID, item.Quantity));

                if (decreasingResult.IsSuccess is false)
                {
                    return ResultDto<bool>.Faliure(decreasingResult.ErrorCode, decreasingResult.Message);
                }

            }

            var createOrderResult =
                await _mediator.Send(new CreateOrderCommand(request.CustomerId, totalAmount, request.OrderLineItems));



            return ResultDto<bool>.Sucess(true);
        }
    }
}
