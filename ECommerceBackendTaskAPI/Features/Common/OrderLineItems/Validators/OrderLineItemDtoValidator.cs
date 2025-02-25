namespace ECommerceBackendTaskAPI.Features.Common.OrderLineItems.Validators
{
    public class OrderLineItemDtoValidator : AbstractValidator<OrderLineItemDto>
    {
        public OrderLineItemDtoValidator()
        {
            RuleFor(x => x.ProductId)
                .GreaterThan(0)
                .WithMessage("ProductId must be greater than 0.");

            RuleFor(x => x.Quantity)
                .GreaterThan(0)
                .WithMessage("Quantity must be greater than 0.");
        }

    }
}
