namespace ECommerceBackendTaskAPI.Features.Orders.CancelOrder.Orchestrators
{
    public record CancelOrderOrchestrator(int OrderId):IRequest<ResultDto<bool>>;
    public class CancelOrderOrchestratorHandler : BaseOrchestratorHandler<CancelOrderOrchestrator, ResultDto<bool>>
    {
        public CancelOrderOrchestratorHandler(OrchestratorParameters orchestratorParameters) : base(orchestratorParameters)
        {
        }

        public override async Task<ResultDto<bool>> Handle(CancelOrderOrchestrator request, CancellationToken cancellationToken)
        {
            var cancelOrderResult = await _mediator.Send(new CancelOrderCommand(request.OrderId));
            if (cancelOrderResult.IsSuccess is false) { 
                return ResultDto<bool>.Faliure(cancelOrderResult.ErrorCode , cancelOrderResult.Message);
            }

            var porductsOrderResult = await _mediator.Send(new GetProductsOfOrderLineItemsByOrderIdQuery(request.OrderId));

            foreach (var item in porductsOrderResult.Data)
            {
               var updateProductStokResult = await _mediator.Send(new UpdateProductStockQuantityCommand(item.ProductId ,item.Quantity));
                if (updateProductStokResult.IsSuccess is false)
                {
                    throw new BusinessException(ErrorCode.InternalServerError,$"Failed to update stock for product {item.ProductId}");

                }
            }
            return ResultDto<bool>.Sucess(true,"Order canceled and stock updated successfully");
         }
    }
}
