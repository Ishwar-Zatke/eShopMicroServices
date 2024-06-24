using Basket.API.Models.Domain;

namespace Basket.API.Interfaces
{
    public interface IShoppingCart
    {
        Task<ShoppingCart?> CreateShoppingCartAsync(ShoppingCart shoppingCart);
        Task<ShoppingCart?> UpdateShoppingCartAsync(Guid id, ShoppingCart shoppingCart);
        Task<ShoppingCart?> DeleteShoppingCartAsync(Guid id);
    }
}
