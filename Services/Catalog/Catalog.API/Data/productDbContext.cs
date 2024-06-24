using Microsoft.EntityFrameworkCore;

namespace Catalog.Data
{
    public class productDbContext : DbContext
    {
        public productDbContext(DbContextOptions<productDbContext> options) : base(options)
        {

        }

        public virtual DbSet<PRODUCT> PRODUCTS { get; set; }
    }
}
