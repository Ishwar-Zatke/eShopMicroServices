using Discount.Grpc;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Controllers
{
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly productDbContext dbContext;
        private readonly IProducts productsRepo;

        public ProductController(productDbContext dbContext, IProducts productsRepo )
        {
            this.dbContext = dbContext;
            this.productsRepo = productsRepo;
        }

        [HttpGet]
        [Route("products/getallproducts")]
        public async Task<IActionResult> GetAllProducts()
        {

            try
            {
                var products = await dbContext.PRODUCTS.ToListAsync();
                APIResponseDTO Response = new APIResponseDTO()
                {
                    displayMessage = "All Products fetched:",
                    responseBody = products,
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
        [Route("products/getproductById")]
        public async Task<IActionResult> GetProductById(Guid id)
        {
            try
            {
                var products = await dbContext.PRODUCTS.FindAsync(id);
                APIResponseDTO Response = new APIResponseDTO()
                {
                    displayMessage = "Product fetched:",
                    responseBody = products,
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
        [Route("products/getProductByCategory")]
        public async Task<IActionResult> GetProductByCategory(string category)
        {
            try
            {
                var products =   productsRepo.GetProductbyCategory(category);
                return Ok(new APIResponseDTO()
                {
                    displayMessage = "Products fetched for particular category",
                    responseBody = products,
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

        [HttpPost]
        [Route("products/CreateProduct")]
        public async Task<IActionResult> CreateProduct(ProductDto addproduct)
        {
            try
            {

                var product = addproduct.Adapt<PRODUCT>();
                product = await productsRepo.CreateProductAsync(product);
                
                if (product == null)
                {
                    return BadRequest(new APIResponseDTO()
                    {
                        displayMessage = "No Products added",
                        statusMessage = "FAIL"
                    });
                    throw new Exception("Error: No products passed");
                }
                var productCreated = product.Adapt<ProductDto>();
                return Ok(new APIResponseDTO()
                {
                    displayMessage = "Products created successfully",
                    responseBody = productCreated,

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
        [Route("products/UpdateProduct")]
        public async Task<IActionResult> UpdateProduct(Guid id, ProductDto updateProduct)
        {
            try
            {
                var product = updateProduct.Adapt<PRODUCT>();
                product = await productsRepo.UpdateProductAsync(id, product);
                if (product == null)
                {
                    return NotFound("product not found");
                }
                var productUpdated = product.Adapt<ProductDto>();
                return Ok(new APIResponseDTO()
                {
                    displayMessage = "Products updated successfully",
                    responseBody = productUpdated,
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
        [Route("products/deleteProduct")]
        public async Task<IActionResult> deleteProduct(Guid id)
        {
            try
            {
                var product = await productsRepo.DeleteProductAsync(id);
                if (product == null)
                {
                    return NotFound("product not found");
                }
                var productDeleted = product.Adapt<ProductDto>();
                return Ok(new APIResponseDTO()
                {
                    displayMessage = "Products deleted successfully",
                    responseBody = productDeleted,
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
