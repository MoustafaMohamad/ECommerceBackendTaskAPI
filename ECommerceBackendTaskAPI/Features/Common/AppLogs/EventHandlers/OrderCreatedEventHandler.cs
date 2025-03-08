using ECommerceBackendTaskAPI.Features.Orders.CreateOrder.Events;

namespace ECommerceBackendTaskAPI.Features.Common.AppLogs.EventHandlers
{
    public class OrderCreatedEventHandler : BaseNotificationHandler<OrderCreatedEvent>
    {
        public OrderCreatedEventHandler(NotificationParameters notificationParameters) : base(notificationParameters)
        {
        }

        public override async Task Handle(OrderCreatedEvent request, CancellationToken cancellationToken)
        {
            var message = $"Order created with ID: {request.OrderId} with TotalAmount :{request.TotalAmount}";
            await _mediator.Send(new AddLogCommand(LogLevels.Information, message));
        }
    }
}
