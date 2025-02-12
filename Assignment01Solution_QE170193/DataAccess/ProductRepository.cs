using BusinessObject;
using BusinessObjects;
using DataAccess.Interface;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public class ProductRepository : RepositoryBase<Product, int>, IProductRepository
    {
        public ProductRepository(EstoreDbContext context) : base(context)
        {

        }

        public async Task<IEnumerable<Product>> GetAllProducts()
        {
            var listProducts = new List<Product>();
            try
            {
                listProducts = await _context.Product.Include(c => c.Category).Include(s => s.Supplier).ToListAsync();

                foreach (var product in listProducts)
                {
                    product.Category = await _context.Category.FindAsync(product.CategoryId);
                    product.Supplier = await _context.Supplier.FindAsync(product.SupplierId);
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return listProducts;
        }

        public async Task<IEnumerable<Product>> GetProductsByNameAsync(string productName = null!, decimal unitPrice = 0)
        {
            IQueryable<Product> query = _context.Product.Include(c => c.Category).Include(s => s.Supplier);

            if (!string.IsNullOrEmpty(productName))
            {
                query = query.Where(p => p.ProductName.Contains(productName));
            }

            if (unitPrice > 0)
            {
                query = query.Where(p => p.UnitPrice == unitPrice);
            }

            return await query.ToListAsync();
        }


    }
}
