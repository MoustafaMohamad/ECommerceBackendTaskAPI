namespace ECommerceBackendTaskAPI.Features.Orders.UpdateOrderStatus.Commands
{
    public record UpdateOrderStatusCommand(int OrderId, OrderStatus Status) : IRequest<ResultDto<bool>>;

    public class UpdateOrderStatusCommandHandeler : BaseRequestHandler<Order, UpdateOrderStatusCommand, ResultDto<bool>>
    {
        public UpdateOrderStatusCommandHandeler(RequestParameters<Order> requestParameters) : base(requestParameters)
        {
        }

        public override async Task<ResultDto<bool>> Handle(UpdateOrderStatusCommand request, CancellationToken cancellationToken)
        {
            if (request.Status != OrderStatus.Pending && request.Status != OrderStatus.Shipped && request.Status != OrderStatus.Delivered)
            {
                return ResultDto<bool>.Faliure(ErrorCode.BadRequest, "Invalid status update");
            }
            var order = await _repository.GetByIdAsync(request.OrderId);
            if (order == null)
            {
                return ResultDto<bool>.Faliure(ErrorCode.NotFound, "this Order not found");
            }
            if (order.Status == OrderStatus.Canceled)
            {
                return ResultDto<bool>.Faliure(ErrorCode.Conflict, "Cannot update the status of a canceled order");
            }

            if (order.Status == request.Status)
            {
                return ResultDto<bool>.Faliure(ErrorCode.Conflict, "This order already has this status");
            }

            var orderUpdated = new Order()
            {
                ID = order.ID,
                Status = request.Status,
            };

            _repository.UpdateIncluded(orderUpdated, nameof(Order.Status));

            await _mediator.Publish(new OrderStatusUpdatedEvent(order.ID, request.Status, DateTime.Now), cancellationToken);

            return ResultDto<bool>.Sucess(true, "Order status updated successfully");

        }
    }
}
