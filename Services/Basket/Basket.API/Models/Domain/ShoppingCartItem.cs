using Catalog.Models.Domain;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Basket.API.Models.Domain
{
    public class ShoppingCartItem
    {
        [Key]
        public Guid Id { get; set; }
        public int Quantity { get; set; } = default;
        public string Color { get; set; } = default!;
        public decimal Price { get; set; } = default;

        [ForeignKey("PRODUCT")]
        public Guid ProductId { get; set; } = default;

        public PRODUCT Product {  get; set; } = default!;

    }
}
