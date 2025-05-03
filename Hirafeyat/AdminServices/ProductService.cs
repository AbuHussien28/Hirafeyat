using AutoMapper;
using Hirafeyat.AdminRepository;
using Hirafeyat.Models;
using Hirafeyat.ViewModel.Admin;

namespace Hirafeyat.AdminServices
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        private readonly ICategoryRepository _categoryRepository;
        public ProductService(IProductRepository productRepository, ICategoryRepository categoryRepository,IMapper mapper)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _mapper = mapper;
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
        public async Task UpdateProductAsync(EditProduct vm)
        {
            var existingProduct = await _productRepository.GetProductByIdAsync(vm.Id);
            if (existingProduct == null)
                throw new KeyNotFoundException($"Product with ID {vm.Id} not found");

            // handle image upload
            if (vm.ImageFile != null && vm.ImageFile.Length > 0)
            {
                string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images");
                Directory.CreateDirectory(uploadsFolder);

                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(vm.ImageFile.FileName);
                var filePath = Path.Combine(uploadsFolder, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await vm.ImageFile.CopyToAsync(stream);
                }

                vm.ImageUrl = "/images/" + fileName;
            }
            else
            {
                vm.ImageUrl = existingProduct.ImageUrl; 
            }

            // Map updated values
            _mapper.Map(vm, existingProduct);

            await _productRepository.UpdateProductAsync(existingProduct);
            await _productRepository.SaveAsync();
        }
    }
}
