using ECommerceBackendTaskAPI.Common.Exceptions;
using ECommerceBackendTaskAPI.Common.ResultDtos;
using ECommerceBackendTaskAPI.Entities;
using ECommerceBackendTaskAPI.Features.Common.OrderLineItems.Dtos;
using ECommerceBackendTaskAPI.Features.Common.Products.Queries;
using ECommerceBackendTaskAPI.Features.Orders.CreateOrder;
using ECommerceBackendTaskAPI.Features.Orders.CreateOrder.Orchestrators;
using MediatR;
using Moq;

namespace ECommerceBackendTaskAPITest.EndpointsTests
{
    public class CreateOrderEndpointTests
    {
        private readonly Mock<IMediator> _mediatorMock;
        private readonly CreateOrderEndpoint _endpoint;

        public CreateOrderEndpointTests()
        {
            _mediatorMock = new Mock<IMediator>();
            _endpoint = new CreateOrderEndpoint(_mediatorMock.Object);
        }

        [Fact]
        public async Task CreateOrderAsync_ReturnsSuccess_WhenOrderCreatedSuccessfully()
        {
            // Arrange: simulate valid order creation
            var createOrderRequest = new CreateOrderEndpointRequest(
                1,
                new List<OrderLineItemDto>
                {
                new OrderLineItemDto(1,2),
                new OrderLineItemDto(2,1)
                }
            );

            var productResult1 = ResultDto<Product>.Sucess(new Product { ID = 1, Price = 50, StockQuantity = 10 });

            var createOrderResult = ResultDto<bool>.Sucess(true);

            // Mocking the mediator
            _mediatorMock.Setup(m => m.Send(It.IsAny<CreateOrderOrchestrator>(), It.IsAny<CancellationToken>()))
           .ReturnsAsync(createOrderResult);

            // Mocking the Send method to return a Task<ResultDto<Product>>
            _mediatorMock.Setup(m => m.Send(It.IsAny<GetProductByIdQuery>(), It.IsAny<CancellationToken>()))
             .ReturnsAsync(productResult1);



            // Act
            var result = await _endpoint.CreateOrderAsync(createOrderRequest);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(ErrorCode.None, result.ErrorCode);
        }

        [Fact]
        public async Task CreateOrderAsync_ReturnsFailure_WhenCustomerNotFound()
        {
            // Arrange: simulate customer not found
            var createOrderRequest = new CreateOrderEndpointRequest(999, new List<OrderLineItemDto>());

            var customerNotFoundResult = ResultDto<bool>.Faliure(ErrorCode.NotFound, "Customer not found");

            _mediatorMock.Setup(m => m.Send(It.IsAny<CreateOrderOrchestrator>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(customerNotFoundResult);

            // Act
            var result = await _endpoint.CreateOrderAsync(createOrderRequest);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("Customer not found", result.Message);
            Assert.Equal(ErrorCode.NotFound, result.ErrorCode);
        }

        [Fact]
        public async Task CreateOrderAsync_ReturnsFailure_WhenProductNotFound()
        {
            // Arrange: simulate product not found
            var createOrderRequest = new CreateOrderEndpointRequest(
                1,
                new List<OrderLineItemDto>
                {
                new OrderLineItemDto(999,2)
                }
            );


            var orchestratorResult = ResultDto<bool>.Faliure(ErrorCode.NotFound, "Product not found");
            _mediatorMock.Setup(m => m.Send(It.IsAny<CreateOrderOrchestrator>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(orchestratorResult);

            // Act
            var result = await _endpoint.CreateOrderAsync(createOrderRequest);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("Product not found", result.Message);
            Assert.Equal(ErrorCode.NotFound, result.ErrorCode);
        }

        [Fact]
        public async Task CreateOrderAsync_ReturnsFailure_WhenNotEnoughStock()
        {
            // Arrange
            var createOrderRequest = new CreateOrderEndpointRequest(
                1,
                new List<OrderLineItemDto>
                {
                new OrderLineItemDto(1,1000)
                }
            );

            var productResult = ResultDto<Product>.Sucess(new Product { ID = 1, Price = 50, StockQuantity = 10 });

            _mediatorMock.Setup(m => m.Send(It.IsAny<GetProductByIdQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(productResult);

            var orchestratorResult = ResultDto<bool>.Faliure(ErrorCode.BadRequest, "Not enough stock for product Product 1");
            _mediatorMock.Setup(m => m.Send(It.IsAny<CreateOrderOrchestrator>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(orchestratorResult);

            // Act
            var result = await _endpoint.CreateOrderAsync(createOrderRequest);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("Not enough stock for product Product 1", result.Message);
            Assert.Equal(ErrorCode.BadRequest, result.ErrorCode);
        }

        [Fact]
        public async Task CreateOrderAsync_ReturnsFailure_WhenProductQuantityUpdateFails()
        {
            // Arrange
            var createOrderRequest = new CreateOrderEndpointRequest(
                1,
                new List<OrderLineItemDto>
                {
                new OrderLineItemDto(1,1)
                }
            );



            var orchestratorResult = ResultDto<bool>.Faliure(ErrorCode.InternalServerError, "Failed to decrease stock");
            _mediatorMock.Setup(m => m.Send(It.IsAny<CreateOrderOrchestrator>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(orchestratorResult);


            // Act
            var result = await _endpoint.CreateOrderAsync(createOrderRequest);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("Failed to decrease stock", result.Message);
            Assert.Equal(ErrorCode.InternalServerError, result.ErrorCode);
        }


    }
}
