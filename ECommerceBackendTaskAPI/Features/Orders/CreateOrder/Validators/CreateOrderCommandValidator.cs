namespace ECommerceBackendTaskAPI.Features.Orders.CreateOrder.Validators
{
    public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
    {
        public CreateOrderCommandValidator()
        {
            RuleFor(x => x.CustomerId)
                .GreaterThan(0)
                .WithMessage("CustomerId must be greater than 0.");

            RuleFor(x => x.TotalAmount)
                .GreaterThan(0)
                .WithMessage("TotalAmount must be greater than 0.");

            RuleFor(x => x.OrderLineItems)
                .NotEmpty()
                .WithMessage("OrderLineItems cannot be empty.");

            RuleForEach(x => x.OrderLineItems)
                .SetValidator(new OrderLineItemDtoValidator());
        }
    }
}
