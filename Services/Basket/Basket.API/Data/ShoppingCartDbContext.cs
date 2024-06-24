using Basket.API.Models.Domain;
using Catalog.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Basket.API.Data
{
    public class ShoppingCartDbContext : DbContext
    {
        public ShoppingCartDbContext(DbContextOptions<ShoppingCartDbContext> options) : base(options)
        {

        }

        public virtual DbSet<ShoppingCart> ShoppingCart { get; set; }
        public virtual DbSet<ShoppingCartItem> ShoppingCartItem { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ShoppingCartItem>()
                .HasOne(u => u.Product)
                .WithMany()
                .HasForeignKey(u => u.ProductId);
        }
    }
}

