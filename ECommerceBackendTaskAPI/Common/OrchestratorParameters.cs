namespace ECommerceBackendTaskAPI.Common
{
    public class OrchestratorParameters
    {
        public IMediator Mediator { get; set; }


        public OrchestratorParameters(IMediator mediator)
        {
            Mediator = mediator;

        }
    }
}
