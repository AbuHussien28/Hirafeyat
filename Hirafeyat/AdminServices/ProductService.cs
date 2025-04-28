using Hirafeyat.AdminRepository;
using Hirafeyat.Models;

namespace Hirafeyat.AdminServices
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public async Task DeleteProductAsync(int id)
        {
             await _productRepository.DeleteProductAsync(id);
             await _productRepository.SaveAsync();
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            try
            {
                return await _productRepository.GetProductByIdAsync(id);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving product with ID {id}: {ex.Message}", ex);
            }
        }

        public async Task<IPagedList<Product>> GetProductsAsync(int pageNumber, int pageSize, string? sellerId = null)
        {
            if (pageNumber < 1) throw new ArgumentException("Page number must be greater than 0");
            if (pageSize < 1) throw new ArgumentException("Page size must be greater than 0");

            return await _productRepository.GetProductsAsync(pageNumber, pageSize, sellerId);
        }

        public async Task<int> GetTotalProductsCountAsync(string? sellerId = null)
        {
            return await _productRepository.GetTotalProductsCountAsync(sellerId);
        }

        public async Task UpdateProductAsync(Product product)
        {
            var existingProduct = await _productRepository.GetProductByIdAsync(product.Id);
            if (existingProduct == null)
            {
                throw new KeyNotFoundException($"Product with ID {product.Id} not found");
            }

            await _productRepository.UpdateProductAsync(product);
            await _productRepository.SaveAsync();
        }
    }
}
