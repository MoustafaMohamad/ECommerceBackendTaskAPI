using ECommerceBackendTaskAPI.Common.Exceptions;
using ECommerceBackendTaskAPI.Common.ResultDtos;
using ECommerceBackendTaskAPI.Entities;
using ECommerceBackendTaskAPI.Features.Orders.GetCustomerOrders.Dtos;
using ECommerceBackendTaskAPI.Features.Orders.GetCustomerOrders;
using MediatR;
using Moq;
using ECommerceBackendTaskAPI.Features.Orders.GetCustomerOrders.Queries;

namespace ECommerceBackendTaskAPITest.EndpointsTests
{
    public class GetCustomerOrdersEndpointTests_XUnit
    {
        private readonly Mock<IMediator> _mediatorMock;
        private readonly GetCustomerOrdersEndpoint _endpoint;

        // Test setup: runs before each [Fact] (new instance per test in xUnit)
        public GetCustomerOrdersEndpointTests_XUnit()
        {
            _mediatorMock = new Mock<IMediator>();
            _endpoint = new GetCustomerOrdersEndpoint(_mediatorMock.Object);
        }

        [Fact]
        public async Task GetCustomerOrdersAsync_ReturnsOrders_WhenCustomerExists()
        {
            // Arrange: simulate customer existence and orders retrieval
            var customerId = 1;
            var customerOrders = new List<GetCustomerOrderDto>
        {
            new GetCustomerOrderDto(1, DateTime.Now, 100.0m, OrderStatus.Pending),
            new GetCustomerOrderDto(2, DateTime.Now, 200.0m, OrderStatus.Delivered)
        };

            var mediatorResult = ResultDto<IEnumerable<GetCustomerOrderDto>>.Sucess(customerOrders);
            _mediatorMock.Setup(m => m.Send(It.IsAny<GetCustomerOrdersQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(mediatorResult);

            // Act
            var result = await _endpoint.GetCustomerOrdersAsync(customerId);

            // Assert: verify that the result is successful and contains the correct data
            Assert.True(result.IsSuccess);
            Assert.Equal(customerOrders, result.Data);
            Assert.Equal(ErrorCode.None, result.ErrorCode);
        }

        [Fact]
        public async Task GetCustomerOrdersAsync_ReturnsNotFound_WhenCustomerDoesNotExist()
        {
            // Arrange: simulate customer not found scenario
            var customerId = 999;
            var mediatorResult = ResultDto<IEnumerable<GetCustomerOrderDto>>.Faliure(ErrorCode.NotFound, "Customer not found");
            _mediatorMock.Setup(m => m.Send(It.IsAny<GetCustomerOrdersQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(mediatorResult);

            // Act
            var result = await _endpoint.GetCustomerOrdersAsync(customerId);

            // Assert: verify failure and not found error
            Assert.False(result.IsSuccess);
            Assert.Null(result.Data);
            Assert.Equal("Customer not found", result.Message);
            Assert.Equal(ErrorCode.NotFound, result.ErrorCode);
        }

        [Fact]
        public async Task GetCustomerOrdersAsync_ReturnsEmpty_WhenCustomerHasNoOrders()
        {
            // Arrange: simulate customer with no orders
            var customerId = 1;
            var mediatorResult = ResultDto<IEnumerable<GetCustomerOrderDto>>.Sucess(new List<GetCustomerOrderDto>());
            _mediatorMock.Setup(m => m.Send(It.IsAny<GetCustomerOrdersQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(mediatorResult);

            // Act
            var result = await _endpoint.GetCustomerOrdersAsync(customerId);

            // Assert: verify success with empty list
            Assert.True(result.IsSuccess);
            Assert.Empty(result.Data);
            Assert.Equal(ErrorCode.None, result.ErrorCode);
        }


    }
}
