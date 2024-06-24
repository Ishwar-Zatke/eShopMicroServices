
using Customer.API.Data;
using Customer.API.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace Customer.API.Repositories
{
    public class CustomersRepo :  ICustomer
    {
        private readonly CustomerDbContext dbContext;
        private readonly IConfiguration configuration;

        public CustomersRepo(CustomerDbContext dbContext, IConfiguration configuration)
        {
            this.dbContext = dbContext;
            this.configuration = configuration;
        }
        

        public async Task<CUSTOMER?> CreateCustomerAsync(CUSTOMER customer)
        {
            try
            {
                if (customer == null)
                {
                    return null;
                }
                await dbContext.CUSTOMERS.AddAsync(customer);
                await dbContext.SaveChangesAsync();
                return customer;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<CUSTOMER?> UpdateCustomerAsync(Guid id, CUSTOMER customer)
        {
            try
            {
                var existingCustomer = await dbContext.CUSTOMERS.FirstOrDefaultAsync(x => x.Id == id);
                if (existingCustomer == null)
                {
                    return null;
                }
                existingCustomer.Name = customer.Name;
                existingCustomer.ContactNumber = customer.ContactNumber;
                existingCustomer.Email = customer.Email;
                await dbContext.SaveChangesAsync();
                return existingCustomer;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<CUSTOMER?> DeleteCustomerAsync(Guid id)
        {
            try
            {
                var existingCustomer = await dbContext.CUSTOMERS.FirstOrDefaultAsync(x => x.Id == id);
                if (existingCustomer == null)
                {
                    return null;
                }
                dbContext.CUSTOMERS.Remove(existingCustomer);
                await dbContext.SaveChangesAsync();
                return existingCustomer;
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
