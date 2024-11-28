using Domain.Interfaces;
using Entities;

namespace Domain.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<Category> GetCategoryByIdAsync(int id)
        {
            return await _categoryRepository.GetEntityById(id);
        }

        public async Task<IEnumerable<Category>> GetAllCategoriesAsync()
        {
            return await _categoryRepository.GetList();
        }

        public async Task AddCategoryAsync(Category category)
        {
            await _categoryRepository.Add(category);
        }

        public async Task UpdateCategoryAsync(Category category)
        {
            await _categoryRepository.Update(category);
        }

        public async Task DeleteCategoryAsync(Category category)
        {
            await _categoryRepository.Delete(category);
        }
    }
}
