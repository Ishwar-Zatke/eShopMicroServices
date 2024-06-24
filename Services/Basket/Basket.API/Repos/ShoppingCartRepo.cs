
using Basket.API.Data;
using Basket.API.Interfaces;
using Basket.API.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Basket.API.Repositories
{
    public class ShoppingCartRepo : IShoppingCart
    {
        private readonly ShoppingCartDbContext dbContext;

        public ShoppingCartRepo(ShoppingCartDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<ShoppingCart?> CreateShoppingCartAsync(ShoppingCart shoppingCart)
        {
            try
            {
                if (shoppingCart == null)
                {
                    return null;
                }

                
                foreach(var item in shoppingCart.Items){
                    await dbContext.ShoppingCartItem.AddAsync(item);
                };
                await dbContext.ShoppingCart.AddAsync(shoppingCart);
                await dbContext.SaveChangesAsync();
                return shoppingCart;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ShoppingCart?> UpdateShoppingCartAsync(Guid id, ShoppingCart shoppingCart)
        {
            try
            {
                var existingShoppingCart = await dbContext.ShoppingCart.FirstOrDefaultAsync(x => x.Id == id);
                if (existingShoppingCart == null)
                {
                    return null;
                }
                existingShoppingCart.UserName = shoppingCart.UserName;
                existingShoppingCart.Items = shoppingCart.Items;

                await dbContext.SaveChangesAsync();
                return existingShoppingCart;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ShoppingCart?> DeleteShoppingCartAsync(Guid id)
        {
            try
            {
                var existingShoppingCart = await dbContext.ShoppingCart.FirstOrDefaultAsync(x => x.Id == id);
                if (existingShoppingCart == null)
                {
                    return null;
                }
                dbContext.ShoppingCart.Remove(existingShoppingCart);
                await dbContext.SaveChangesAsync();
                return existingShoppingCart;
            }
            catch (Exception)
            {
                throw;
            }
        }


    }
}
