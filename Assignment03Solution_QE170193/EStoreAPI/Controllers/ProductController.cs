using BusinessObject.Models;
using DataAccess.Dto.ProductDto;
using DataAccess.Repository.Interface;
using Microsoft.AspNetCore.Mvc;

namespace EStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private IProductRepository _productRepository;

        public ProductController(IProductRepository productRepository)
        {
            this._productRepository = productRepository;
        }

        [HttpGet("GetAllProduct")]
        public async Task<IEnumerable<Product>> GetProducts(string? keyword, decimal? unitP) => await _productRepository.GetProducts(keyword, unitP);


        [HttpPost("AddProduct")]
        public IActionResult AddProduct(ProductRequestDto p)
        {
            _productRepository.SaveProduct(p);
            return NoContent();
        }

        [HttpGet("Detail/{id}")]
        public async Task<Product?> GetProductById(int id) => await _productRepository.GetProductById(id);

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var p = await _productRepository.GetProductById(id);
            if (p == null)
            {
                return NotFound("Can not found Product to delete");
            }
            await _productRepository.DeleteProduct(p);
            return NoContent();
        }

        [HttpPut("Update")]
        public async Task<IActionResult> UpdateProduct(ProductUpdateRequestDto p)
        {
            var pTmp = await _productRepository.GetProductById(p.ProductId);
            if (pTmp == null)
            {
                return NotFound($"Can not find Product have name {p.ProductName}");
            }
            await _productRepository.UpdateProduct(p);
            return NoContent();
        }
    }
}
