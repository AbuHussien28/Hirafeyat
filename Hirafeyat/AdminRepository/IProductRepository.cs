using Hirafeyat.Models;
using Microsoft.EntityFrameworkCore;

namespace Hirafeyat.AdminRepository
{
    public interface IProductRepository : IRepository
    {
        Task<IPagedList<Product>> GetProductsAsync(int pageNumber, int pageSize, string? sellerId = null);
        Task<int> GetTotalProductsCountAsync(string? sellerId = null);
        Task<Product> GetProductByIdAsync(int id);
        Task UpdateProductAsync(Product product);
        public Task DeleteProductAsync(int id);
        Task SaveAsync();
    }
}
