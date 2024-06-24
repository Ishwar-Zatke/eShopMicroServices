using System.ComponentModel.DataAnnotations;

namespace Basket.API.Models.Domain
{
    public class ShoppingCart
    {
        [Key]
        public Guid Id { get; set; }
        public required string UserName { get; set; } 
        public List<ShoppingCartItem> Items { get; set; } = new();
        public decimal TotalPrice => Items.Sum(x => x.Price = x.Quantity);
        public ShoppingCart(string userName)
        {
            UserName = userName;
        }
        public ShoppingCart()
        {
            
        }
    }
}
  