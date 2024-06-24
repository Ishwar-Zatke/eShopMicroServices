using Customer.API.Models;

namespace Customer.API.Repositories
{
    public interface ICustomer
    {
        Task<CUSTOMER?> CreateCustomerAsync(CUSTOMER product);
        Task<CUSTOMER?> UpdateCustomerAsync(Guid id, CUSTOMER product);
        Task<CUSTOMER?> DeleteCustomerAsync(Guid id);
    }
}
