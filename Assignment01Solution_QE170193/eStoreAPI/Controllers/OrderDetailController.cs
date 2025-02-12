using Application.Mappings;
using Application.OrderDetails.Commands;
using Application.OrderDetails.Queries;
using Application.OrderDetails.Responses;
using eStoreAPI.Mapping;
using eStoreAPI.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shared;
using Shared.Constants;
using System.Net;

namespace eStoreAPI.Controllers
{
    [Route("api/orderdetails")]
    [ApiController]
    public class OrderDetailController : BaseController<OrderDetailController>
    {
        public OrderDetailController(
           IMediator mediator,
           ILogger<OrderDetailController> logger
       ) : base(mediator, logger) { }

        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<List<OrderDetailResponse>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAllOrderDetails()
        {
            var query = new GetAllOrderDetailsQuery();
            return await ExecuteAsync<GetAllOrderDetailsQuery, List<OrderDetailResponse>>(query);
        }

        [HttpGet("order/{orderId}")]
        [ProducesResponseType(typeof(ApiResponse<List<OrderDetailResponse>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetOrderDetailsByOrderId(int orderId)
        {
            var query = new GetOrderDetailsByOrderIdQuery(orderId);
            return await ExecuteAsync<GetOrderDetailsByOrderIdQuery, List<OrderDetailResponse>>(query);
        }

        [HttpGet("order/{orderId}/product/{productId}")]
        [ProducesResponseType(typeof(ApiResponse<OrderDetailResponse>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetOrderDetailByOrderIdAndProductId(int orderId, int productId)
        {
            var query = new GetOrderDetailByOrderIdAndProductIdQuery(orderId, productId);
            return await ExecuteAsync<GetOrderDetailByOrderIdAndProductIdQuery, OrderDetailResponse>(query);
        }

        [HttpPost]
        [ProducesResponseType(typeof(ApiResponse<Unit>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> CreateOrderDetail([FromBody] CreateOrderDetailRequest request)
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
            var command = AppMapper<ModelMapping>.Mapper.Map<CreateOrderDetailCommand>(request);
            return await ExecuteAsync<CreateOrderDetailCommand, Unit>(command);
        }

        [HttpPut("order/{orderId}/product{productId}")]
        [ProducesResponseType(typeof(ApiResponse<Unit>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateOrderDetail(int orderId, int productId, [FromBody] UpdateOrderDetailRequest request)
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
            var command = AppMapper<ModelMapping>.Mapper.Map<UpdateOrderDetailCommand>(request);
            command.OrderId = orderId;
            command.ProductId = productId;
            return await ExecuteAsync<UpdateOrderDetailCommand, Unit>(command);
        }
    }


}
