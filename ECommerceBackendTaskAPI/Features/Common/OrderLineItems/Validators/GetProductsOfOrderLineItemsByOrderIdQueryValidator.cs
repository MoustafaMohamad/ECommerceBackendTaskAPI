namespace ECommerceBackendTaskAPI.Features.Common.OrderLineItems.Validators
{
    public class GetProductsOfOrderLineItemsByOrderIdQueryValidator : AbstractValidator<GetProductsOfOrderLineItemsByOrderIdQuery>
    {
        public GetProductsOfOrderLineItemsByOrderIdQueryValidator()
        {
            RuleFor(x => x.OrderId)
                .GreaterThan(0)
                .WithMessage("OrderId must be greater than 0.");
        }
    }
}
