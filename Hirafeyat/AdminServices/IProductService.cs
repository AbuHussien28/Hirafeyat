using Hirafeyat.Models;

namespace Hirafeyat.AdminServices
{
    public interface IProductService 
    {
        Task<Product> GetProductByIdAsync(int id);
        Task<IPagedList<Product>> GetProductsAsync(int pageNumber, int pageSize, string? sellerId = null);
        Task<int> GetTotalProductsCountAsync(string? sellerId = null);
        Task UpdateProductAsync(Product product);
        Task DeleteProductAsync(int id);
    }

}
