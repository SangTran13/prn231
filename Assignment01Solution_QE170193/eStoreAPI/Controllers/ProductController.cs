using Application.Mappings;
using Application.Products.Commands;
using Application.Products.Queries;
using Application.Products.Responses;
using eStoreAPI.Mapping;
using eStoreAPI.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shared;
using Shared.Constants;
using System.Net;

namespace eStoreAPI.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductController : BaseController<ProductController>
    {
        public ProductController(
            IMediator mediator,
            ILogger<ProductController> logger
        ) : base(mediator, logger) { }

        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<List<ProductResponse>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAllProducts()
        {
            var query = new GetAllProductsQuery();
            return await ExecuteAsync<GetAllProductsQuery, List<ProductResponse>>(query);
        }

        [HttpGet("{productId}")]
        [ProducesResponseType(typeof(ApiResponse<ProductResponse>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetProductById(int productId)
        {
            var query = new GetProductByIdQuery(productId);
            return await ExecuteAsync<GetProductByIdQuery, ProductResponse>(query);
        }

        [HttpGet("search/{keyword?}")]
        [ProducesResponseType(typeof(ApiResponse<List<ProductResponse>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> SearchProducts(string? keyword)
        {
            string productName = string.Empty;
            decimal unitPrice = 0;

            if (!string.IsNullOrEmpty(keyword))
            {
                if (decimal.TryParse(keyword, out var parsedPrice))
                {
                    unitPrice = parsedPrice;
                }
                else
                {
                    productName = keyword;
                }
            }

            var query = new GetProductsByNameQuery(productName, unitPrice);
            return await ExecuteAsync<GetProductsByNameQuery, List<ProductResponse>>(query);
        }


        [HttpPost]
        [ProducesResponseType(typeof(ApiResponse<int>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductRequest request)
        {
            if (request == null)
            {
                return BadRequest(new ApiResponse<object>
                {
                    StatusCode = (int)Shared.Constants.StatusCode.ModelInvalid,
                    Message = ResponseMessages.GetMessage(Shared.Constants.StatusCode.ModelInvalid),
                    Errors = ["The request body does not contain required fields"]
                });
            }
            var command = AppMapper<ModelMapping>.Mapper.Map<CreateProductCommand>(request);
            return await ExecuteAsync<CreateProductCommand, int>(command);
        }

        [HttpPut("{productId}")]
        [ProducesResponseType(typeof(ApiResponse<Unit>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateProduct(int productId, [FromBody] UpdateProductRequest request)
        {
            if (request == null)
            {
                return BadRequest(new ApiResponse<object>
                {
                    StatusCode = (int)Shared.Constants.StatusCode.ModelInvalid,
                    Message = ResponseMessages.GetMessage(Shared.Constants.StatusCode.ModelInvalid),
                    Errors = ["The request body does not contain required fields"]
                });
            }
            var command = AppMapper<ModelMapping>.Mapper.Map<UpdateProductCommand>(request);
            command.ProductId = productId;
            return await ExecuteAsync<UpdateProductCommand, Unit>(command);
        }

        [HttpDelete("{productId}")]
        [ProducesResponseType(typeof(ApiResponse<Unit>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteProduct(int productId)
        {
            var command = new DeleteProductCommand(productId);
            return await ExecuteAsync<DeleteProductCommand, Unit>(command);
        }
    }
}
