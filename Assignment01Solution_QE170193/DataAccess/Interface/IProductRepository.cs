using BusinessObject;
using System;

namespace DataAccess.Interface
{
    public interface IProductRepository : IAsyncRepository<Product, int>
    {
        Task<IEnumerable<Product>> GetAllProducts();
        Task<IEnumerable<Product>> GetProductsByNameAsync(string productName = null!, decimal unitPrice = 0);
    }
}
