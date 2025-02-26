namespace ECommerceBackendTaskAPI.Common
{
    public class NotificationParameters
    {
        public IMediator Mediator { get; set; }

        public NotificationParameters(IMediator mediator)
        {
            Mediator = mediator;
        }
    }
}
