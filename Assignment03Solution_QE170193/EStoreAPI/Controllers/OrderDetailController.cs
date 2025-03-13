using BusinessObject.Models;
using DataAccess.Dto.OrderDto;
using DataAccess.Repository.Interface;
using Microsoft.AspNetCore.Mvc;

namespace EStoreAPI.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class OrderDetailController : ControllerBase
    {
        private IOrderDetailRepository _orderDetailRepository;

        public OrderDetailController(IOrderDetailRepository orderDetailRepository)
        {
            this._orderDetailRepository = orderDetailRepository;
        }

        [HttpGet("Detail/{id}")]
        public async Task<OrderDetail> GetOrderDetails(int Id) => await _orderDetailRepository.FindOrderDetailById(Id);

        [HttpGet("GetOrderDetailByOrderId/{orderId}")]
        public async Task<List<OrderDetail>> GetOrderDetailByOrderId(int orderId)
        {
            return await _orderDetailRepository.GetOrderDetailByOrderId(orderId);
        }

        [HttpPut("Update")]
        public IActionResult UpdateOrderDetail(OrderDetailUpdateRequestDto p)
        {
            var pTmp = _orderDetailRepository.FindOrderDetailById(p.OrderId);
            if (pTmp == null)
            {
                return NotFound($"Can not find OrderDetail have id {p.OrderId}");
            }
            _orderDetailRepository.UpdateOrderDetail(p);
            return NoContent();
        }

        [HttpGet("Report")]
        public Task<List<Order>> ReportOrder(DateTime? fromDate, DateTime? toDate) => _orderDetailRepository.ReportOrder(fromDate, toDate);
    }
}
