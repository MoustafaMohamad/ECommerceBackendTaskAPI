
namespace ECommerceBackendTaskAPI.Common
{
    public abstract class BaseNotificationHandler<TNotification> : INotificationHandler<TNotification> where TNotification : INotification
    {
        protected readonly IMediator _mediator;
        public BaseNotificationHandler(NotificationParameters notificationParameters)
        {
            _mediator = notificationParameters.Mediator;
        }
        public abstract Task Handle(TNotification notification, CancellationToken cancellationToken);
    }
}
