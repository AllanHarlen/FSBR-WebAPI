using Domain.Services;
using Entities;
using Microsoft.AspNetCore.Mvc;

namespace WebApis.Controllers
{
    [ApiController]
    [Route("api/Produtos")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;

        public ProductController(IProductService productService, ICategoryService categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            if (product == null)
                return NotFound();

            var category = await _categoryService.GetCategoryByIdAsync(product.CategoryId);
            if (category != null)
                product.Category = category;

            return Ok(product);
        }

        [HttpGet("ListarProdutos")]
        public async Task<IActionResult> GetAllProducts()
        {
            var products = await _productService.GetAllProductsAsync();
            var lstCategories = await _categoryService.GetAllCategoriesAsync();

            var populatedProducts = products.Select(p =>
            {
                p.Category = lstCategories.FirstOrDefault(c => c.Id == p.CategoryId);
                return p;
            }).ToList();

            return Ok(products);
        }

        // Alterado para usar DTO ao invés do modelo Produto completo
        [HttpPost]
        public async Task<IActionResult> AddProduct([FromBody] ProductDTO productDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Verificar se a categoria existe
            var category = await _categoryService.GetCategoryByIdAsync(productDto.CategoryId);
            if (category == null)
                return BadRequest("Categoria não encontrada.");

            // Criar e adicionar o produto com base no DTO
            var product = new Product
            {
                Name = productDto.Name,
                Price = productDto.Price,
                CategoryId = productDto.CategoryId
            };

            await _productService.AddProductAsync(product);
            return CreatedAtAction(nameof(GetProductById), new { id = product.Id }, product);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] Product product)
        {
            if (id != product.Id)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var Category = await _categoryService.GetCategoryByIdAsync(product.CategoryId);
            if (Category != null)
                product.Category = Category;

            await _productService.UpdateProductAsync(product);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            if (product == null)
                return NotFound();

            await _productService.DeleteProductAsync(product);
            return NoContent();
        }
    }
}
