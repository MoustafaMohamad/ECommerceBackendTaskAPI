using ECommerceBackendTaskAPI.Features.Orders.UpdateOrderStatus.Commands;

namespace ECommerceBackendTaskAPI.Features.Orders.UpdateOrderStatus.Validators
{
    public class UpdateOrderStatusCommandValidator : AbstractValidator<UpdateOrderStatusCommand>
    {
        public UpdateOrderStatusCommandValidator()
        {
            RuleFor(x => x.OrderId)
                .GreaterThan(0)
                .WithMessage("OrderId must be greater than 0.");

            RuleFor(x => x.Status)
                .IsInEnum()
                .WithMessage("Invalid order status.");
        }
    }
}
