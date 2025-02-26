using ECommerceBackendTaskAPI.Common.Exceptions;
using ECommerceBackendTaskAPI.Common.ResultDtos;
using ECommerceBackendTaskAPI.Features.Orders.CancelOrder;
using ECommerceBackendTaskAPI.Features.Orders.CancelOrder.Orchestrators;
using MediatR;
using Moq;

namespace ECommerceBackendTaskAPITest.EndpointsTests
{
    public class CancelOrderEndpointTests
    {
        private readonly Mock<IMediator> _mediatorMock;
        private readonly CancelOrderEndpoint _endpoint;

        public CancelOrderEndpointTests()
        {
            _mediatorMock = new Mock<IMediator>();
            _endpoint = new CancelOrderEndpoint(_mediatorMock.Object);
        }

        [Fact]
        public async Task CancelOrderAsync_ReturnsSuccess_WhenOrderCancelledSuccessfully()
        {
            var cancelResult = ResultDto<bool>.Sucess(true);
            _mediatorMock.Setup(m => m.Send(It.IsAny<CancelOrderOrchestrator>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(cancelResult);

            var result = await _endpoint.CancelOrderAsync(1);

            Assert.True(result.IsSuccess);
            Assert.Equal(true, result.Data);
            Assert.Equal(ErrorCode.None, result.ErrorCode);
        }

        [Fact]
        public async Task CancelOrderAsync_ReturnsFailure_WhenOrderNotFound()
        {
            var cancelResult = ResultDto<bool>.Faliure(ErrorCode.NotFound, "Order not found");
            _mediatorMock.Setup(m => m.Send(It.IsAny<CancelOrderOrchestrator>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(cancelResult);

            var result = await _endpoint.CancelOrderAsync(1);

            Assert.False(result.IsSuccess);
            Assert.Null(result.Data);
            Assert.Equal(ErrorCode.NotFound, result.ErrorCode);
        }




    }
}
