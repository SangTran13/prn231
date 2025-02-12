using Application.Mappings;
using Application.Orders.Commands;
using Application.Orders.Queries;
using Application.Orders.Responses;
using eStoreAPI.Mapping;
using eStoreAPI.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shared;
using Shared.Constants;
using System.Net;

namespace eStoreAPI.Controllers
{
    [Route("api/orders")]
    [ApiController]
    public class OrderController : BaseController<OrderController>
    {
        public OrderController(
           IMediator mediator,
           ILogger<OrderController> logger
       ) : base(mediator, logger) { }

        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<List<OrderResponse>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAllOrders()
        {
            var query = new GetAllOrdersQuery();
            return await ExecuteAsync<GetAllOrdersQuery, List<OrderResponse>>(query);
        }

        [HttpGet("{orderId}")]
        [ProducesResponseType(typeof(ApiResponse<OrderResponse>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetOrderById(int orderId)
        {
            var query = new GetOrderByIdQuery(orderId);
            return await ExecuteAsync<GetOrderByIdQuery, OrderResponse>(query);
        }

        [HttpGet("member/{memberId}")]
        [ProducesResponseType(typeof(ApiResponse<List<OrderResponse>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetOrdersByMember(int memberId)
        {
            var query = new GetOrdersByMemberQuery(memberId);
            return await ExecuteAsync<GetOrdersByMemberQuery, List<OrderResponse>>(query);
        }

        [HttpPost]
        [ProducesResponseType(typeof(ApiResponse<int>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderRequest request)
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
            var command = AppMapper<ModelMapping>.Mapper.Map<CreateOrderCommand>(request);
            return await ExecuteAsync<CreateOrderCommand, int>(command);
        }

        [HttpPut("shipped/{orderId}")]
        [ProducesResponseType(typeof(ApiResponse<Unit>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> PutOrderShipped(int orderId)
        {
            var command = new UpdateOrderStatusCommand()
            {
                OrderId = orderId,
                NewStatus = 1,
                ShippedDate = DateTime.UtcNow
            };
            return await ExecuteAsync<UpdateOrderStatusCommand, Unit>(command);
        }

        [HttpPut("cancel/{orderId}")]
        [ProducesResponseType(typeof(ApiResponse<Unit>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> PutOrderCanceled(int orderId)
        {
            var command = new UpdateOrderStatusCommand()
            {
                OrderId = orderId,
                NewStatus = 2
            };
            return await ExecuteAsync<UpdateOrderStatusCommand, Unit>(command);
        }
    }

}
