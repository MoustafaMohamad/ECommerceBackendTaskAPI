using ECommerceBackendTaskAPI.Common.Exceptions;
using ECommerceBackendTaskAPI.Common.ResultDtos;
using ECommerceBackendTaskAPI.Entities;
using ECommerceBackendTaskAPI.Features.Orders.GetOrders.Dtos;
using ECommerceBackendTaskAPI.Features.Orders.GetOrders;
using MediatR;
using Moq;
using static ECommerceBackendTaskAPI.Common.PagedListQueryableExtensions;
using ECommerceBackendTaskAPI.Features.Orders.GetOrders.Queries;

namespace ECommerceBackendTaskAPITest.EndpointsTests
{
    public class GetAllOrdersEndpointTests
    {
        private readonly Mock<IMediator> _mediatorMock;
        private readonly GetAllOrdersEndpoint _endpoint;

        public GetAllOrdersEndpointTests()
        {
            _mediatorMock = new Mock<IMediator>();
            _endpoint = new GetAllOrdersEndpoint(_mediatorMock.Object);
        }

        [Fact]
        public async Task GetAllOrdersAsync_ReturnsOrders_WhenOrdersFound()
        {
            // Arrange
            var page = 1;
            var pageSize = 10;
            var orderDtos = new List<OrderDto>
        {
            new OrderDto { ID = 1, OrderDate = DateTime.Now, TotalAmount = 100.0m, Status = OrderStatus.Pending },
            new OrderDto { ID = 2, OrderDate = DateTime.Now, TotalAmount = 200.0m, Status = OrderStatus.Delivered }
        };

            var pagedList = new PagedList<OrderDto>(orderDtos, 2, page, pageSize);
            var resultDto = ResultDto<PagedList<OrderDto>>.Sucess(pagedList);

            _mediatorMock.Setup(m => m.Send(It.IsAny<GetAllOrdersQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(resultDto);

            // Act
            var result = await _endpoint.GetAllOrdersAsync(page, pageSize);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(orderDtos.Count, result.Data.Count);
            Assert.Equal(ErrorCode.None, result.ErrorCode);
        }
        [Fact]
        public async Task GetAllOrdersAsync_ReturnsEmpty_WhenNoOrdersFound()
        {
            // Arrange
            var page = 1;
            var pageSize = 10;
            var pagedList = new PagedList<OrderDto>(Enumerable.Empty<OrderDto>(), 0, page, pageSize);
            var resultDto = ResultDto<PagedList<OrderDto>>.Sucess(pagedList);

            _mediatorMock.Setup(m => m.Send(It.IsAny<GetAllOrdersQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(resultDto);

            // Act
            var result = await _endpoint.GetAllOrdersAsync(page, pageSize);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Empty(result.Data);
            Assert.Equal(ErrorCode.None, result.ErrorCode);
        }


    }
}
