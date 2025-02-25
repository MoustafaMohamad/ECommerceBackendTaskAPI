namespace ECommerceBackendTaskAPI.Features.Orders.CancelOrder.Validators
{
    public class CancelOrderOrchestratorValidator : AbstractValidator<CancelOrderOrchestrator>
    {
        public CancelOrderOrchestratorValidator()
        {
            RuleFor(x => x.OrderId)
                .GreaterThan(0)
                .WithMessage("OrderId must be greater than 0.");
        }
    }
}
