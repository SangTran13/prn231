using BusinessObject.Models;
using DataAccess.Dto.OrderDto;

namespace DataAccess.Repository.Interface
{
    public interface IOrderDetailRepository
    {
        Task<OrderDetail?> FindOrderDetailById(int orderId);
        Task UpdateOrderDetail(OrderDetailUpdateRequestDto p);
        Task<List<Order>> ReportOrder(DateTime? fromDate, DateTime? toDate);
        Task<List<OrderDetail>> GetOrderDetailByOrderId(int orderId);
    }
}
