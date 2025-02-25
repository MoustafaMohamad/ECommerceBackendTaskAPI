using ECommerceBackendTaskAPI.Features.Orders.GetCustomerOrders.Queries;

namespace ECommerceBackendTaskAPI.Features.Orders.GetCustomerOrders.Validators
{
    public class GetCustomerOrdersQueryValidator : AbstractValidator<GetCustomerOrdersQuery>
    {
        public GetCustomerOrdersQueryValidator()
        {
            RuleFor(x => x.CustomerId)
                .GreaterThan(0)
                .WithMessage("CustomerId must be greater than 0.");
        }
    }
}
