namespace ECommerceBackendTaskAPI.Features.Common.Products.Validators
{
    public class UpdateProductStockQuantityCommandValidator : AbstractValidator<UpdateProductStockQuantityCommand>
    {
        public UpdateProductStockQuantityCommandValidator()
        {
            RuleFor(x => x.ProductId)
                .GreaterThan(0)
                .WithMessage("ProductId must be greater than 0.");

            RuleFor(x => x.Quantity)
                .NotEqual(0)
                .WithMessage("Quantity must not be equal to 0.");
        }
    }
}
