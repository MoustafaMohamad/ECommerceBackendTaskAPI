using ECommerceBackendTaskAPI.Features.Orders.UpdateOrderStatus.Commands;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceBackendTaskAPI.Features.Orders.UpdateOrderStatus
{
    [ApiController]
    [Route("Orders/[action]")]
    public class UpdateOrderStatusEndpoint : ControllerBase
    {
        private readonly IMediator _mediator;

        public UpdateOrderStatusEndpoint(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ResultViewModel> UpdateOrderStatusAsync(int OrderId, OrderStatus Status)
        {
            var result = await _mediator.Send(new UpdateOrderStatusCommand(OrderId , Status));
            if (result.IsSuccess is false) 
            { 
                return ResultViewModel.Faliure(result.ErrorCode , result.Message);
            }
            return ResultViewModel.Sucess("Order status updated successfully");
        }
    }
}
