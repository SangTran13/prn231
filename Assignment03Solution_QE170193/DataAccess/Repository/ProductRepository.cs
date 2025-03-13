using BusinessObject.Models;
using DataAccess.Dto.ProductDto;
using DataAccess.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly EstoreDbContext _context;

        public ProductRepository(EstoreDbContext context)
        {
            _context = context;
        }

        public async Task DeleteProduct(Product p)
        {
            _context.Products.Remove(p);
            await _context.SaveChangesAsync();
        }

        public async Task<Product?> GetProductById(int id)
        {
            return await _context.Products.FindAsync(id);
        }

        public async Task<List<Product>> GetProducts(string? keyword, decimal? unitP)
        {
            var query = _context.Products.AsQueryable();
            if (!string.IsNullOrEmpty(keyword))
            {
                query = query.Where(p => p.ProductName.Contains(keyword));
            }
            if (unitP != null)
            {
                query = query.Where(p => p.UnitPrice == unitP);
            }
            return await query.Include(p => p.Category).ToListAsync();
        }

        public async Task SaveProduct(ProductRequestDto p)
        {
            var product = new Product
            {
                CategoryId = p.CategoryId,
                ProductName = p.ProductName,
                Weight = p.Weight,
                UnitPrice = p.UnitPrice,
                UnitsInStock = p.UnitsInStock
            };
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateProduct(ProductUpdateRequestDto p)
        {
            var product = await _context.Products.FindAsync(p.ProductId);
            if (product == null)
            {
                throw new InvalidOperationException("Product not found");
            }
            product.CategoryId = p.CategoryId;
            product.ProductName = p.ProductName;
            product.Weight = p.Weight;
            product.UnitPrice = p.UnitPrice;
            product.UnitsInStock = p.UnitsInStock;
            await _context.SaveChangesAsync();
        }
    }
}
