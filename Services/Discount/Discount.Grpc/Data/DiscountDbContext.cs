using Discount.Grpc.Models;
using Microsoft.EntityFrameworkCore;

namespace Discount.Grpc.Data
{
    public class DiscountDbContext: DbContext
    {
        public DiscountDbContext(DbContextOptions<DiscountDbContext> options) : base(options)
        {
        }

        public virtual DbSet<Coupon> Coupons { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Coupon>().HasData(
                new Coupon { Id = 1, ProductName = "IphoneX", Description = "Iphone discount", Amount = 150 },
                new Coupon { Id = 2, ProductName = "Samsung 10", Description = "Samsung discount", Amount = 200 }
            );
        }
    }
}
