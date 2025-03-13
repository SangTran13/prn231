using BusinessObject.Models;
using DataAccess.Dto.OrderDto;

namespace DataAccess.Repository.Interface
{
    public interface IOrderRepository
    {
        Task SaveOrder(OrderRequestDto p);
        Task<Order?> GetOrderById(int id);
        Task DeleteOrder(Order p);
        Task UpdateOrder(OrderUpdateRequestDto p);
        Task<List<Order>> GetOrders(string? keyword);
        Task<List<Order>> GetOrdersByMemberId(Guid memberId, string? keyword);
    }
}
