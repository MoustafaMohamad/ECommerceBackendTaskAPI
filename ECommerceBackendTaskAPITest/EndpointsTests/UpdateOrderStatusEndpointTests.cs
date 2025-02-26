using ECommerceBackendTaskAPI.Common.Exceptions;
using ECommerceBackendTaskAPI.Common.ResultDtos;
using ECommerceBackendTaskAPI.Data.Repositories;
using ECommerceBackendTaskAPI.Entities;
using ECommerceBackendTaskAPI.Features.Orders.UpdateOrderStatus;
using ECommerceBackendTaskAPI.Features.Orders.UpdateOrderStatus.Commands;
using MediatR;
using Moq;

namespace ECommerceBackendTaskAPITest.EndpointsTests
{
    public class UpdateOrderStatusEndpointTests_XUnit
    {
        private readonly Mock<IMediator> _mediatorMock;
        private readonly Mock<IRepository<Order>> _repositoryMock;
        private readonly UpdateOrderStatusEndpoint _endpoint;

        public UpdateOrderStatusEndpointTests_XUnit()
        {
            _mediatorMock = new Mock<IMediator>();
            _repositoryMock = new Mock<IRepository<Order>>();
            _endpoint = new UpdateOrderStatusEndpoint(_mediatorMock.Object);
        }

        [Fact]
        public async Task UpdateOrderStatusAsync_ReturnsSuccess_WhenOrderStatusUpdated()
        {
            // Arrange
            var orderId = 1;
            var newStatus = OrderStatus.Shipped;
            var order = new Order { ID = orderId, Status = OrderStatus.Pending };

            var updateResult = ResultDto<bool>.Sucess(true, "Order status updated successfully");
            _repositoryMock.Setup(r => r.GetByIdAsync(orderId)).ReturnsAsync(order);
            _mediatorMock.Setup(m => m.Send(It.IsAny<UpdateOrderStatusCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(updateResult);

            // Act
            var result = await _endpoint.UpdateOrderStatusAsync(orderId, newStatus);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(ErrorCode.None, result.ErrorCode);
        }

        [Fact]
        public async Task UpdateOrderStatusAsync_ReturnsNotFound_WhenOrderDoesNotExist()
        {
            // Arrange
            var orderId = 999;
            var newStatus = OrderStatus.Shipped;

            var updateResult = ResultDto<bool>.Faliure(ErrorCode.NotFound, "this Order not found");
            _mediatorMock.Setup(m => m.Send(It.IsAny<UpdateOrderStatusCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(updateResult);

            // Act
            var result = await _endpoint.UpdateOrderStatusAsync(orderId, newStatus);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("this Order not found", result.Message);
            Assert.Equal(ErrorCode.NotFound, result.ErrorCode);
        }

        [Fact]
        public async Task UpdateOrderStatusAsync_ReturnsBadRequest_WhenInvalidStatus()
        {
            // Arrange
            var orderId = 1;
            var invalidStatus = (OrderStatus)999;

            var updateResult = ResultDto<bool>.Faliure(ErrorCode.BadRequest, "Invalid status update");
            _mediatorMock.Setup(m => m.Send(It.IsAny<UpdateOrderStatusCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(updateResult);

            // Act
            var result = await _endpoint.UpdateOrderStatusAsync(orderId, invalidStatus);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("Invalid status update", result.Message);
            Assert.Equal(ErrorCode.BadRequest, result.ErrorCode);
        }

        [Fact]
        public async Task UpdateOrderStatusAsync_ReturnsConflict_WhenOrderIsCanceled()
        {
            // Arrange
            var orderId = 1;
            var newStatus = OrderStatus.Shipped;
            var order = new Order { ID = orderId, Status = OrderStatus.Canceled };

            var updateResult = ResultDto<bool>.Faliure(ErrorCode.Conflict, "Cannot update the status of a canceled order");
            _repositoryMock.Setup(r => r.GetByIdAsync(orderId)).ReturnsAsync(order);
            _mediatorMock.Setup(m => m.Send(It.IsAny<UpdateOrderStatusCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(updateResult);

            // Act
            var result = await _endpoint.UpdateOrderStatusAsync(orderId, newStatus);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("Cannot update the status of a canceled order", result.Message);
            Assert.Equal(ErrorCode.Conflict, result.ErrorCode);
        }

        [Fact]
        public async Task UpdateOrderStatusAsync_ReturnsConflict_WhenStatusAlreadySet()
        {
            // Arrange
            var orderId = 1;
            var currentStatus = OrderStatus.Shipped;
            var newStatus = OrderStatus.Shipped;
            var order = new Order { ID = orderId, Status = currentStatus };

            var updateResult = ResultDto<bool>.Faliure(ErrorCode.Conflict, "This order already has this status");
            _repositoryMock.Setup(r => r.GetByIdAsync(orderId)).ReturnsAsync(order);
            _mediatorMock.Setup(m => m.Send(It.IsAny<UpdateOrderStatusCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(updateResult);

            // Act
            var result = await _endpoint.UpdateOrderStatusAsync(orderId, newStatus);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("This order already has this status", result.Message);
            Assert.Equal(ErrorCode.Conflict, result.ErrorCode);
        }


    }
}
