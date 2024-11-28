using Domain.Interfaces;
using Entities;
using Infraestructure.Configuration;
using Microsoft.Extensions.Configuration;

namespace Infraestructure.Repository
{
    public class ProductRepository : RepositoryGenerics<Product>, IProductRepository
    {
        private readonly ContextBase _context;
        private readonly IConfiguration _configuration;

        public ProductRepository(ContextBase context, IConfiguration configuration) : base()
        {
            _context = context;
        }
    }
}
