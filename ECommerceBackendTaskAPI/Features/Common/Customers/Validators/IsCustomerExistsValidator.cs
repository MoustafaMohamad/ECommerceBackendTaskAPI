namespace ECommerceBackendTaskAPI.Features.Common.Customers.Validators
{
    public class IsCustomerExistsValidator : AbstractValidator<IsCustomerExists>
    {
        public IsCustomerExistsValidator()
        {
            RuleFor(x => x.CustomerId)
                .GreaterThan(0)
                .WithMessage("CustomerId must be greater than 0.");
        }
    }
}
