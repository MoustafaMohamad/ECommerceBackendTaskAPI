namespace ECommerceBackendTaskAPI.Features.Orders.GetOrder.Validators
{
    public class GetOrderQueryValidator : AbstractValidator<GetOrderQuery>
    {
        public GetOrderQueryValidator()
        {
            RuleFor(x => x.OrderId)
                .GreaterThan(0)
                .WithMessage("OrderId must be greater than 0.");
        }
    }
}
