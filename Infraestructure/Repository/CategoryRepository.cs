using Domain.Interfaces;
using Entities;
using Infraestructure.Configuration;

namespace Infraestructure.Repository
{
    public class CategoryRepository : RepositoryGenerics<Category>, ICategoryRepository
    {
        private readonly ContextBase _context;

        public CategoryRepository(ContextBase context) : base()
        {
            _context = context;
        }
    }
}
