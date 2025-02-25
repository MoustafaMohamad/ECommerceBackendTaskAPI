namespace ECommerceBackendTaskAPI.Features.Orders.CancelOrder.Commands
{
    public record CancelOrderCommand(int OrderId): IRequest<ResultDto<bool>>;

    public class CancelOrderCommandHandler : BaseRequestHandler<Order, CancelOrderCommand, ResultDto<bool>>
    {
        public CancelOrderCommandHandler(RequestParameters<Order> requestParameters) : base(requestParameters)
        {
        }

        public override async Task<ResultDto<bool>> Handle(CancelOrderCommand request, CancellationToken cancellationToken)
        {
            var order = await _repository.GetByIdAsync(request.OrderId);
            if (order == null)
            {
                return ResultDto<bool>.Faliure(ErrorCode.NotFound, "Order not found");
            }

            if (order.Status == OrderStatus.Canceled)
            {
                return ResultDto<bool>.Faliure(ErrorCode.Conflict, "Order is already canceled");
            }
            order.Status = OrderStatus.Canceled;
            _repository.UpdateIncluded(order, nameof(Order.Status));
            return ResultDto<bool>.Sucess(true, "Order canceled successfully");

        }
    }
}
