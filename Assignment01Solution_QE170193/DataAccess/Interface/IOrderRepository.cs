using BusinessObject;

namespace DataAccess.Interface
{
    public interface IOrderRepository : IAsyncRepository<Order, int>
    {
        Task<IEnumerable<Order>> GetOrdersByMemberIdAsync(int memberId);
        Task<IEnumerable<Order>> GetAllOrders();

        Task<Order> GetOrderById(int orderId);
    }
}
