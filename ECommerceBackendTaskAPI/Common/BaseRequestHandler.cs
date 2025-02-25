namespace ECommerceBackendTaskAPI.Common
{
    public abstract class BaseRequestHandler<TEntity, TRequest, TResponse> : IRequestHandler<TRequest, TResponse> where TRequest : IRequest<TResponse> where TEntity : BaseModel
    {
        protected readonly IMediator _mediator;
        protected readonly IRepository<TEntity> _repository;
        public BaseRequestHandler(RequestParameters<TEntity> requestParameters)
        {
            _mediator = requestParameters.Mediator;
            _repository = requestParameters.Repository;
        }

        public abstract Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken);
    }
}
