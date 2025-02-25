namespace ECommerceBackendTaskAPI.Features.Common.Products.Validators
{
    public class DecreaseProductQuntityCommandValidator : AbstractValidator<DecreaseProductQuntityCommand>
    {
        public DecreaseProductQuntityCommandValidator()
        {
            RuleFor(x => x.ProductId)
                .GreaterThan(0)
                .WithMessage("ProductId must be greater than 0.");

            RuleFor(x => x.DecreasingQuantity)
                .GreaterThan(0)
                .WithMessage("DecreasingQuantity must be greater than 0.");
        }
    }
}
