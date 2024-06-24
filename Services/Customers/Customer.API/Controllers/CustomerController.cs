using Customer.API.Data;
using Customer.API.Models;
using Customer.API.Models.DTOs;
using Customer.API.Repositories;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Customer.API.Controllers
{
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly CustomerDbContext dbContext;
        private readonly ICustomer customerRepo;

        public CustomerController(CustomerDbContext dbContext, ICustomer customerRepo )
        {
            this.dbContext = dbContext;
            this.customerRepo = customerRepo;
        }

        [HttpGet]
        [Route("customers/getAllCustomers")]
        public async Task<IActionResult> GetAllCustomers()
        {

            try
            {
                var customers = await dbContext.CUSTOMERS.ToListAsync();
                APIResponseDTO Response = new APIResponseDTO()
                {
                    displayMessage = "All customers fetched:",
                    responseBody = customers,
                    supportMessage = null,
                    statusMessage = "SUCCESS"
                };
                return Ok(Response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new APIResponseDTO()
                {
                    displayMessage = "Something went wrong",
                    statusMessage = "FAIL",
                    supportMessage = new
                    {
                        ex.Message,
                        ex.StackTrace
                    }
                });
            }
        }

        [HttpGet]
        [Route("customers/getCustomerById")]
        public async Task<IActionResult> GetCustomerById(Guid id)
        {
            try
            {
                var customers = await dbContext.CUSTOMERS.FindAsync(id);
                APIResponseDTO Response = new APIResponseDTO()
                {
                    displayMessage = "customers fetched:",
                    responseBody = customers,
                    supportMessage = null,
                    statusMessage = "SUCCESS"
                };
                return Ok(Response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new APIResponseDTO()
                {
                    displayMessage = "Something went wrong",
                    statusMessage = "FAIL",
                    supportMessage = new
                    {
                        ex.Message,
                        ex.StackTrace
                    }
                });
            }
        }


        [HttpPost]
        [Route("customers/CreateCustomer")]
        public async Task<IActionResult> CreateCustomer(CustomerDto addCustomer)
        {
            try
            {

                var customer = addCustomer.Adapt<CUSTOMER>();
                customer = await customerRepo.CreateCustomerAsync(customer);

                if (customer == null)
                {
                    return BadRequest(new APIResponseDTO()
                    {
                        displayMessage = "No Customer added",
                        statusMessage = "FAIL"
                    });
                    throw new Exception("Error: No customers passed");
                }
                var customerCreated = customer.Adapt<CustomerDto>();
                return Ok(new APIResponseDTO()
                {
                    displayMessage = "customer created successfully",
                    responseBody = customerCreated,

                    statusMessage = "SUCCESS"
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new APIResponseDTO()
                {
                    displayMessage = "Something went wrong",
                    statusMessage = "FAIL",
                    supportMessage = new
                    {
                        ex.Message,
                        ex.StackTrace
                    }
                });
            }
        }

        [HttpPut]
        [Route("customers/UpdateCustomer")]
        public async Task<IActionResult> UpdateCustomer(Guid id, CustomerDto updateProduct)
        {
            try
            {
                var customer = updateProduct.Adapt<CUSTOMER>();
                customer = await customerRepo.UpdateCustomerAsync(id, customer);
                if (customer == null)
                {
                    return NotFound("customer not found");
                }
                var customerUpdated = customer.Adapt<CustomerDto>();
                return Ok(new APIResponseDTO()
                {
                    displayMessage = "customers updated successfully",
                    responseBody = customerUpdated,
                    statusMessage = "SUCCESS"
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new APIResponseDTO()
                {
                    displayMessage = "Something went wrong",
                    statusMessage = "FAIL",
                    supportMessage = new
                    {
                        ex.Message,
                        ex.StackTrace
                    }
                });
            }
        }

        [HttpDelete]
        [Route("customers/deleteCustomer")]
        public async Task<IActionResult> deleteCustomer(Guid id)
        {
            try
            {
                var customer = await customerRepo.DeleteCustomerAsync(id);
                if (customer == null)
                {
                    return NotFound("customer not found");
                }
                var customerDeleted = customer.Adapt<CustomerDto>();
                return Ok(new APIResponseDTO()
                {
                    displayMessage = "customers deleted successfully",
                    responseBody = customerDeleted,
                    statusMessage = "SUCCESS"
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new APIResponseDTO()
                {
                    displayMessage = "Something went wrong",
                    statusMessage = "FAIL",
                    supportMessage = new
                    {
                        ex.Message,
                        ex.StackTrace
                    }
                });
            }
        }
    }
}
