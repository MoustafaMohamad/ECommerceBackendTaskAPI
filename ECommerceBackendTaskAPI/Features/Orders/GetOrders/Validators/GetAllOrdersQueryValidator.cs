namespace ECommerceBackendTaskAPI.Features.Orders.GetOrders.Validators
{
    public class GetAllOrdersQueryValidator : AbstractValidator<GetAllOrdersQuery>
    {
        public GetAllOrdersQueryValidator()
        {
            RuleFor(x => x.Page)
                .GreaterThan(0)
                .WithMessage("Page must be greater than 0.");

            RuleFor(x => x.PageSize)
                .GreaterThan(0)
                .WithMessage("PageSize must be greater than 0.");
        }
    }
}
