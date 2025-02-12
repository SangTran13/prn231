using BusinessObject;
using BusinessObjects;
using DataAccess.Interface;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public class OrderDetailRepository : RepositoryBase<OrderDetail, int>, IOrderDetailRepository
    {
        public OrderDetailRepository(EstoreDbContext context) : base(context)
        {

        }

        public async Task<IEnumerable<OrderDetail>> GetOrderDetailsByOrderIdAsync(int orderId)
        {
            var listOrderDetails = await _context.OrderDetail
                                                  .Where(o => o.OrderId == orderId)
                                                  .ToListAsync();

            foreach (var order in listOrderDetails)
            {
                order.Product = await _context.Product
                                              .SingleOrDefaultAsync(p => p.ProductId == order.ProductId);
            }

            return listOrderDetails;
        }


        public async Task<OrderDetail?> GetOrderDetailByOrderIdAndProductIdAsync(int orderId, int productId)
        {
            return await _context.OrderDetail.FirstOrDefaultAsync(x => x.OrderId == orderId && x.ProductId == productId);
        }
    }
}
