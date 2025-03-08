namespace ECommerceBackendTaskAPI.Features.Common.AppLogs.EventHandlers
{
    public class CancelOrderEventHandler : BaseNotificationHandler<CancelOrderEvent>
    {
        public CancelOrderEventHandler(NotificationParameters notificationParameters) : base(notificationParameters)
        {
        }
        public override async Task Handle(CancelOrderEvent request, CancellationToken cancellationToken)
        {
            var message = $"Order canceled with ID: {request.OrderId}";
            await _mediator.Send(new AddLogCommand(LogLevels.Information, message));
        }
    }


}
