using ECommerceBackendTaskAPI.Common.Exceptions;
using ECommerceBackendTaskAPI.Common.ResultDtos;
using ECommerceBackendTaskAPI.Features.Orders.GetOrder;
using ECommerceBackendTaskAPI.Features.Orders.GetOrder.Dtos;
using ECommerceBackendTaskAPI.Features.Orders.GetOrder.Queries;
using MediatR;
using Moq;

namespace ECommerceBackendTaskAPITest.EndpointsTests
{

    public class GetOrderEndpointTests
    {
        private readonly Mock<IMediator> _mediatorMock;
        private readonly GetOrderEndpoint _endpoint;

        public GetOrderEndpointTests()
        {
            _mediatorMock = new Mock<IMediator>();
            _endpoint = new GetOrderEndpoint(_mediatorMock.Object);
        }

        [Fact]
        public async Task GetOrderAsync_ReturnsOrder_WhenOrderExists()
        {
            var fakeOrder = new GatOrderDto { Id = 4 };
            var mediatorResult = ResultDto<GatOrderDto>.Sucess(fakeOrder);
            _mediatorMock
                .Setup(m => m.Send(It.IsAny<GetOrderQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(mediatorResult);

            var result = await _endpoint.GetOrderAsync(4);

            Assert.True(result.IsSuccess);
            Assert.Equal(fakeOrder, result.Data);
            Assert.Equal(ErrorCode.None, result.ErrorCode);
        }

        [Fact]
        public async Task GetOrderAsync_ReturnsNotFound_WhenOrderDoesNotExist()
        {
            var mediatorResult = new ResultDto<GatOrderDto>
            {
                IsSuccess = false,
                Data = null,
                Message = "Order not found",
                ErrorCode = ErrorCode.NotFound
            };
            _mediatorMock
                .Setup(m => m.Send(It.IsAny<GetOrderQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(mediatorResult);

            // Act
            var result = await _endpoint.GetOrderAsync(42); // 42 = non-existent order ID for this test

            Assert.False(result.IsSuccess);
            Assert.Null(result.Data);
            Assert.Equal(ErrorCode.NotFound, result.ErrorCode);
        }

    }
}