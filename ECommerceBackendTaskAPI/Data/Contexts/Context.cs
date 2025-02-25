using System.Diagnostics;

namespace ECommerceBackendTaskAPI.Data.Contexts
{
    public class Context :DbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderLineItem> OrderLineItems { get; set; }

        public Context()
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTrackingWithIdentityResolution;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=localhost;Database=ECommerceBackendTask;Trusted_Connection=True;Encrypt=True;TrustServerCertificate=true;")
                 .LogTo(log => Console.WriteLine(log), LogLevel.Information)
                 .EnableSensitiveDataLogging();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>()
           .Property(o => o.Status)
           .HasDefaultValue(OrderStatus.Pending);

            modelBuilder.Entity<Customer>()
            .HasQueryFilter(e => !e.IsDeleted);


            modelBuilder.Entity<Product>()
                .HasQueryFilter(e => !e.IsDeleted);

            modelBuilder.Entity<Order>()
                .HasQueryFilter(e => !e.IsDeleted);

            modelBuilder.Entity<OrderLineItem>()
                .HasQueryFilter(e => !e.IsDeleted);

            base.OnModelCreating(modelBuilder);
        }

    }
}
