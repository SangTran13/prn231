using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shared;
using System.Net;
using Application.Suppliers.Responses;
using Application.Suppliers.Queries;
using Application.Mappings;
using eStoreAPI.Mapping;
using eStoreAPI.Models;
using Shared.Constants;
using Application.Suppliers.Commands;

namespace eStoreAPI.Controllers
{
    [Route("api/suppliers")]
    [ApiController]
    public class SupplierController : BaseController<SupplierController>
    {
        public SupplierController(
            IMediator mediator,
            ILogger<SupplierController> logger
        ) : base(mediator, logger) { }


        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<List<SupplierResponse>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAllSuppliers()
        {
            var query = new GetAllSuppliersQuery();

            return await ExecuteAsync<GetAllSuppliersQuery, List<SupplierResponse>>(query);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ApiResponse<SupplierResponse>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetSupplierById(int id)
        {
            var query = new GetSupplierByIdQuery(id);
            return await ExecuteAsync<GetSupplierByIdQuery, SupplierResponse>(query);
        }

        [HttpPost]
        [ProducesResponseType(typeof(ApiResponse<int>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> CreateSupplier([FromBody] CreateSupplierRequest request)
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
            var command = AppMapper<ModelMapping>.Mapper.Map<CreateSupplierCommand>(request);
            return await ExecuteAsync<CreateSupplierCommand, int>(command);
        }

        [HttpPut("{supplierId}")]
        [ProducesResponseType(typeof(ApiResponse<int>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateSupplier(int supplierId, [FromBody] UpdateSupplierRequest request)
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
            var command = AppMapper<ModelMapping>.Mapper.Map<UpdateSupplierCommand>(request);
            command.SupplierId = supplierId;
            return await ExecuteAsync<UpdateSupplierCommand, int>(command);
        }

        [HttpDelete("{supplierId}")]
        [ProducesResponseType(typeof(ApiResponse<Unit>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteSupplier(int supplierId)
        {
            var command = new DeleteSupplierCommand(supplierId);
            return await ExecuteAsync<DeleteSupplierCommand, Unit>(command);
        }

    }
}
