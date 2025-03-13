using BusinessObject.Models;
using DataAccess.Dto.ProductDto;

namespace DataAccess.Repository.Interface
{
    public interface IProductRepository
    {
        Task SaveProduct(ProductRequestDto p);
        Task<Product?> GetProductById(int id);
        Task DeleteProduct(Product p);
        Task UpdateProduct(ProductUpdateRequestDto p);
        Task<List<Product>> GetProducts(string? keyword, decimal? unitP);
    }
}
