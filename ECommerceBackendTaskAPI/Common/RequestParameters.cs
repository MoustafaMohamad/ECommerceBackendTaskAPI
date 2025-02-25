namespace ECommerceBackendTaskAPI.Common
{
    public class RequestParameters<T> where T : BaseModel
    {
        public IMediator Mediator { get; set; }
        public IRepository<T> Repository { get; set; }

        public RequestParameters(IMediator mediator,
            IRepository<T> repository)
        {
            Mediator = mediator;
            Repository = repository;
        }
    }
}
