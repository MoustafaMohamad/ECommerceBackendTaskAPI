//using ECommerceBackendTaskAPI.Data.Contexts;
//using ECommerceBackendTaskAPI.Data.Repositories;
//using ECommerceBackendTaskAPI.Entities;
//using Microsoft.EntityFrameworkCore;

//namespace ECommerceBackendTaskAPITest
//{
//    public class UnitTest1
//    {
//        private DbContextOptions<Context> GetInMemoryOptions()
//        {
//            return new DbContextOptionsBuilder<Context>()
//                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
//                .Options;
//        }
//        [Fact]
//        public async Task GetOrdersAsync_ReturnsOrder_WhenOrderExists()
//        {
//            var options = GetInMemoryOptions();


//            using (var context = new Context())
//            {
//                var orderRepository = new Repository<Order>(context);


//                var Orders = await orderRepository.GetAll().ToListAsync();


//                Assert.NotNull(Orders);
//                Assert.Equal(7, Orders.Count);
//            }

//        }



//    }
//}