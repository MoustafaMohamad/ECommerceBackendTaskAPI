namespace ECommerceBackendTaskAPI.Features.Common.AppLogs.EventHandlers
{
    public class OrderStatusUpdatedEventHandler : BaseNotificationHandler<OrderStatusUpdatedEvent>
    {
        public OrderStatusUpdatedEventHandler(NotificationParameters notificationParameters) : base(notificationParameters)
        {
        }
        public override async Task Handle(OrderStatusUpdatedEvent request, CancellationToken cancellationToken)
        {
            var message = $"Order status updated with ID: {request.OrderId} to {request.NewStatus}";
            await _mediator.Send(new AddLogCommand(LogLevels.Information, message));

        }
    }
}
