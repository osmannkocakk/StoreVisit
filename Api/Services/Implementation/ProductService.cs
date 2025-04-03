using Api.Services.Interfaces;
using DbComtext;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace Api.Services.Implementation
{
    public class ProductService : IProductService
    {
        private readonly AppDbContext _context;

        public ProductService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Product>> GetProducts(int page, int pageSize)
        {
            return await _context.Products.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
        }

        public async Task<Product> AddProduct(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return product;
        }
    }

}
