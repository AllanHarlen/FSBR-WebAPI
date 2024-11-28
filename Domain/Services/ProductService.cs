using Domain.Interfaces;
using Entities;

namespace Domain.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            return await _productRepository.GetEntityById(id);
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            return await _productRepository.GetList();
        }

        public async Task AddProductAsync(Product product)
        {
            await _productRepository.Add(product);
        }

        public async Task UpdateProductAsync(Product product)
        {
            await _productRepository.Update(product);
        }

        public async Task DeleteProductAsync(Product product)
        {
            await _productRepository.Delete(product);
        }
    }
}
