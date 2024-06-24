namespace Catalog.Interfaces
{
    public interface IProducts
    {
        Task<PRODUCT?> CreateProductAsync(PRODUCT product);
        Task<PRODUCT?> UpdateProductAsync(Guid id, PRODUCT product);
        Task<PRODUCT?> DeleteProductAsync(Guid id);
        IEnumerable<PRODUCT?> GetProductbyCategory(string category);
    }
}
