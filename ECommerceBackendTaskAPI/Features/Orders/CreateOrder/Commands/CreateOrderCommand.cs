using ECommerceBackendTaskAPI.Features.Orders.Events;

namespace ECommerceBackendTaskAPI.Features.Orders.CreateOrder.Commands
{
    public record CreateOrderCommand(int CustomerId, decimal TotalAmount, IEnumerable<OrderLineItemDto> OrderLineItems) : IRequest<ResultDto<bool>>;

    public class CreateOrderCommandHandeler : BaseRequestHandler<Order, CreateOrderCommand, ResultDto<bool>>
    {
        public CreateOrderCommandHandeler(RequestParameters<Order> requestParameters) : base(requestParameters)
        {
        }

        public override async Task<ResultDto<bool>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {

            var newOrder = new Order
            {
                CustomerId = request.CustomerId,
                TotalAmount = request.TotalAmount,
                OrderDate = DateTime.Now,
                OrderLineItems = request.OrderLineItems.AsQueryable().Map<OrderLineItem>().ToList(),
                Status = OrderStatus.Pending

            };
            var result = await _repository.AddAsync(newOrder);

            await _mediator.Publish(new OrderCreatedEvent(result, request.TotalAmount));

            return ResultDto<bool>.Sucess(true);
        }
    }
}
