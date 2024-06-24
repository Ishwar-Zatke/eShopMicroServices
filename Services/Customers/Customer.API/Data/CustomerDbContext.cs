using Customer.API.Models;
using Microsoft.EntityFrameworkCore;
namespace Customer.API.Data
{
    public class CustomerDbContext: DbContext
    {
        public CustomerDbContext(DbContextOptions<CustomerDbContext> options) : base(options) { }

        public DbSet<CUSTOMER> CUSTOMERS { get; set; }
    }
}
