using Basket.API.Data;
using Basket.API.Interfaces;
using Basket.API.Models;
using Basket.API.Models.Domain;
using Basket.API.Models.Dtos;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Basket.API.Controllers
{
    [ApiController]
    public class ShoppingCartController : ControllerBase
    {
        private readonly ShoppingCartDbContext dbContext;
        private readonly IShoppingCart shoppingCartRepo;

        public ShoppingCartController(ShoppingCartDbContext dbContext, IShoppingCart shoppingCartRepo)
        {
            this.dbContext = dbContext;
            this.shoppingCartRepo = shoppingCartRepo;
        }

        [HttpGet]
        [Route("shoppingCart/getallshoppingCart")]
        public async Task<IActionResult> GetAllShoppingCart()
        {

            try
            {
                var shoppingCart = await dbContext.ShoppingCart.ToListAsync();
                APIResponseDTO Response = new APIResponseDTO()
                {
                    displayMessage = "All shoppingCart fetched:",
                    responseBody = shoppingCart,
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
        [Route("shoppingCart/getshoppingCartById")]
        public async Task<IActionResult> GetShoppingCartById(Guid id)
        {
            try
            {
                var shoppingCart = await dbContext.ShoppingCart.FindAsync(id);
                APIResponseDTO Response = new APIResponseDTO()
                {
                    displayMessage = "Product fetched:",
                    responseBody = shoppingCart,
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
        [Route("shoppingCart/CreateShoppingCart")]
        public async Task<IActionResult> CreateShoppingCart(ShoppingCartDto addShoppingCart)
        {
            try
            {

                var shoppingCart = addShoppingCart.Adapt<ShoppingCart>();
                shoppingCart = await shoppingCartRepo.CreateShoppingCartAsync(shoppingCart);

                if (shoppingCart == null)
                {
                    return BadRequest(new APIResponseDTO()
                    {
                        displayMessage = "No Cart added",
                        statusMessage = "FAIL"
                    });
                    throw new Exception("Error: No cart passed");
                }
                var shoppingCartCreated = shoppingCart.Adapt<ShoppingCartDto>();
                return Ok(new APIResponseDTO()
                {
                    displayMessage = "Cart created successfully",
                    responseBody = shoppingCartCreated,

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
        [Route("shoppingCart/UpdateShoppingCart")]
        public async Task<IActionResult> UpdateShoppingCart(Guid id, ShoppingCartDto updateshoppingcart)
        {
            try
            {
                var shoppingCart = updateshoppingcart.Adapt<ShoppingCart>();
                shoppingCart = await shoppingCartRepo.UpdateShoppingCartAsync(id, shoppingCart);
                if (shoppingCart == null)
                {
                    return NotFound("product not found");
                }
                var shoppingCartUpdated = shoppingCart.Adapt<ShoppingCartDto>();
                return Ok(new APIResponseDTO()
                {
                    displayMessage = "shoppingCart updated successfully",
                    responseBody = shoppingCartUpdated,
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
        [Route("shoppingCart/deleteShoppingCart")]
        public async Task<IActionResult> deleteShoppingCart(Guid id)
        {
            try
            {
                var shoppingCart = await shoppingCartRepo.DeleteShoppingCartAsync(id);
                if (shoppingCart == null)
                {
                    return NotFound("product not found");
                }
                var shoppingCartDeleted = shoppingCart.Adapt<ShoppingCartDto>();
                return Ok(new APIResponseDTO()
                {
                    displayMessage = "shoppingCart deleted successfully",
                    responseBody = shoppingCartDeleted,
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
