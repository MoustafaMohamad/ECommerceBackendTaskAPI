

namespace ECommerceBackendTaskAPI.Features.Orders.CreateOrder.Validators
{
    public class CreateOrderOrchestratorValidator : AbstractValidator<CreateOrderOrchestrator>
    {
        public CreateOrderOrchestratorValidator()
        {
            RuleFor(x => x.CustomerId)
                .GreaterThan(0)
                .WithMessage("CustomerId must be greater than 0.");

            RuleFor(x => x.OrderLineItems)
                .NotEmpty()
                .WithMessage("OrderLineItems cannot be empty.");

            RuleForEach(x => x.OrderLineItems)
                .SetValidator(new OrderLineItemDtoValidator());
        }
    }
}
