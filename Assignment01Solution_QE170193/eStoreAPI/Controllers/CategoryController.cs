using Application.Categories.Commands;
using Application.Categories.Queries;
using Application.Categories.Responses;
using Application.Mappings;
using eStoreAPI.Mapping;
using eStoreAPI.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shared;
using Shared.Constants;
using System.Net;

namespace eStoreAPI.Controllers
{
    [Route("api/categories")]
    [ApiController]
    public class CategoryController : BaseController<CategoryController>
    {
        public CategoryController(
            IMediator mediator,
            ILogger<CategoryController> logger
        ) : base(mediator, logger) { }


        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<List<CategoryResponse>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAllCategories()
        {
            var query = new GetAllCategoriesQuery();
            return await ExecuteAsync<GetAllCategoriesQuery, List<CategoryResponse>>(query);
        }

        [HttpGet("{categoryId}")]
        [ProducesResponseType(typeof(ApiResponse<CategoryResponse>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetCategoryById(int categoryId)
        {
            var query = new GetCategoryByIdQuery(categoryId);
            return await ExecuteAsync<GetCategoryByIdQuery, CategoryResponse>(query);
        }

        [HttpDelete("{categoryId}")]
        [ProducesResponseType(typeof(ApiResponse<Unit>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteCategory(int categoryId)
        {
            var command = new DeleteCategoryCommand(categoryId);
            return await ExecuteAsync<DeleteCategoryCommand, Unit>(command);
        }

        [HttpPost]
        [ProducesResponseType(typeof(ApiResponse<int>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> CreateCategory([FromBody] CreateCategoryRequest request)
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
            var command = AppMapper<ModelMapping>.Mapper.Map<CreateCategoryCommand>(request);
            return await ExecuteAsync<CreateCategoryCommand, int>(command);
        }

        [HttpPut("{categoryId}")]
        [ProducesResponseType(typeof(ApiResponse<int>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateCategory(int categoryId, [FromBody] UpdateCategoryRequest request)
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
            var command = AppMapper<ModelMapping>.Mapper.Map<UpdateCategoryCommand>(request);
            command.CategoryId = categoryId;
            return await ExecuteAsync<UpdateCategoryCommand, int>(command);
        }
    }
}
