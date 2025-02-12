using BusinessObject;

namespace DataAccess.Interface
{
    public interface IOrderDetailRepository : IAsyncRepository<OrderDetail, int>
    {
        Task<IEnumerable<OrderDetail>> GetOrderDetailsByOrderIdAsync(int orderId);

        Task<OrderDetail?> GetOrderDetailByOrderIdAndProductIdAsync(int orderId, int productId);
    }
}
