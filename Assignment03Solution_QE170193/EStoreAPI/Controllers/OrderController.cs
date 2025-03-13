using BusinessObject.Models;
using DataAccess.Dto.OrderDto;
using DataAccess.Repository.Interface;
using Microsoft.AspNetCore.Mvc;

namespace EStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private IOrderRepository _orderRepository;

        public OrderController(IOrderRepository orderRepository)
        {
            this._orderRepository = orderRepository;
        }

        [HttpGet("GetAllOrder")]
        public async Task<IEnumerable<Order>> GetOrders(string? keyword) => await _orderRepository.GetOrders(keyword);

        [HttpPost("AddOrder")]
        public async Task<IActionResult> AddOrder(OrderRequestDto p)
        {
            await _orderRepository.SaveOrder(p);
            return NoContent();
        }

        [HttpGet("Detail/{id}")]
        public async Task<Order?> GetOrderById(int id) => await _orderRepository.GetOrderById(id);

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var p = await _orderRepository.GetOrderById(id);
            if (p == null)
            {
                return NotFound("Can not found order to delete");
            }
            await _orderRepository.DeleteOrder(p);
            return NoContent();
        }

        [HttpGet("GetOrderByMemberId/{memberId}")]
        public async Task<IEnumerable<Order>> GetOrderByMemberId(Guid memberId, string? keyword)
        {
            return await _orderRepository.GetOrdersByMemberId(memberId, keyword);
        }


        [HttpPut("Update")]
        public async Task<IActionResult> UpdateOrder(OrderUpdateRequestDto p)
        {
            var pTmp = await _orderRepository.GetOrderById(p.OrderId);
            if (pTmp == null)
            {
                return NotFound($"Can not find order have id {p.OrderId}");
            }
            await _orderRepository.UpdateOrder(p);
            return NoContent();
        }
    }
}
