

using Basket.API.Models.Domain;
using Basket.API.Models.Dto;

namespace Basket.API.Models.Dtos
{
    public class ShoppingCartDto
    {
        public required string UserName { get; set; }
        public List<ShoppingCartItemDto> Items { get; set; } = new();
        public decimal TotalPrice => Items.Sum(x => x.Price = x.Quantity);

        
    }
}
