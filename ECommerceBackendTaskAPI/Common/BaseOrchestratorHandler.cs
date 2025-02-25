namespace ECommerceBackendTaskAPI.Common
{
    public abstract class BaseOrchestratorHandler< TRequest, TResponse> : IRequestHandler<TRequest, TResponse> where TRequest : IRequest<TResponse> 
    {
        protected readonly IMediator _mediator;
        public BaseOrchestratorHandler(OrchestratorParameters orchestratorParameters)
        {
            _mediator = orchestratorParameters.Mediator;
        }

        public abstract Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken);
    }
}
