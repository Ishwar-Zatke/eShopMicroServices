using Discount.Grpc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;

namespace Catalog.Repositories
{
    public class ProductsRepo : IProducts
    {
        private readonly productDbContext dbContext;
        private readonly DiscountProtoService.DiscountProtoServiceClient discountProto;
        private readonly IConfiguration configuration;

        public ProductsRepo(productDbContext dbContext, DiscountProtoService.DiscountProtoServiceClient discountProto, IConfiguration configuration)
        {
            this.dbContext = dbContext;
            this.discountProto = discountProto;
            this.configuration = configuration;
        }
        private void SendMessage(object message)
        {
            var factory = new ConnectionFactory() { HostName = configuration["RabbitMQSettings:HostName"] };
            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();
            channel.QueueDeclare(queue: configuration["RabbitMQSettings:QueueName"], durable: false, exclusive: false, autoDelete: false, arguments: null);

            var messageBody = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));
            channel.BasicPublish(exchange: "", routingKey: configuration["RabbitMQSettings:QueueName"], basicProperties: null, body: messageBody);
        }

        public async Task<PRODUCT?> CreateProductAsync(PRODUCT product)
        {
            try
            {
                if (product == null)
                {
                    return null;
                }
                //Grpc use
                var coupon = await discountProto.GetDiscountAsync(new GetDiscountRequest { ProductName = product.Name });
                if (coupon != null)
                {
                    product.Price = product.Price - coupon.Amount;
                }
                await dbContext.PRODUCTS.AddAsync(product);
                await dbContext.SaveChangesAsync();
                SendMessage(new { Action = "Create", Product = "GET API call" });
                return product;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<PRODUCT?> UpdateProductAsync(Guid id, PRODUCT product)
        {
            try
            {
                var existingProduct = await dbContext.PRODUCTS.FirstOrDefaultAsync(x => x.Id == id);
                if (existingProduct == null)
                {
                    return null;
                }
                var coupon = await discountProto.GetDiscountAsync(new GetDiscountRequest { ProductName = product.Name });
                if (coupon != null)
                {
                    product.Price = product.Price - coupon.Amount;
                }
                existingProduct.Name = product.Name;
                existingProduct.Category = product.Category;
                existingProduct.Description = product.Description;
                existingProduct.ImageFile = product.ImageFile;
                existingProduct.Price = product.Price;
                await dbContext.SaveChangesAsync();
                SendMessage(new { Action = "Update", Product = existingProduct });
                return existingProduct;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<PRODUCT?> DeleteProductAsync(Guid id)
        {
            try
            {
                var existingProduct = await dbContext.PRODUCTS.FirstOrDefaultAsync(x => x.Id == id);
                if (existingProduct == null)
                {
                    return null;
                }
                dbContext.PRODUCTS.Remove(existingProduct);
                await dbContext.SaveChangesAsync();
                SendMessage(new { Action = "Update", Product = existingProduct });
                return existingProduct;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<PRODUCT?> GetProductbyCategory(string category)
        {
            try
            {
                var products = dbContext.PRODUCTS.ToList();
                List<PRODUCT?> newProducts = [];
                foreach (var product in products)
                {
                    if (product.Category.Contains(category))
                    {
                        newProducts.Add(product);
                    }
                }
                return newProducts;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
