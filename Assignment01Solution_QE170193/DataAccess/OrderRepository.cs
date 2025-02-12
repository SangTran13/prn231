using BusinessObject;
using BusinessObjects;
using DataAccess.Interface;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public class OrderRepository : RepositoryBase<Order, int>, IOrderRepository
    {
        public OrderRepository(EstoreDbContext context) : base(context)
        {

        }

        public async Task<IEnumerable<Order>> GetOrdersByMemberIdAsync(int memberId)
        {
            return await _context.Order.Where(o => o.MemberId == memberId).ToListAsync();
        }

        public async Task<IEnumerable<Order>> GetAllOrders()
        {
            var listOrders = new List<Order>();
            try
            {
                listOrders = await _context.Order.Include(m => m.Member).ToListAsync();

                foreach (var order in listOrders)
                {
                    order.Member = await _context.Member.FindAsync(order.MemberId);
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return listOrders;
        }

        public async Task<Order> GetOrderById(int orderId)
        {
            Order order = null;
            try
            {
                order = await _context.Order.Include(m => m.Member)
                                                     .SingleOrDefaultAsync(o => o.OrderId == orderId);
                if (order != null)
                {
                    order.Member = _context.Member.Find(order.MemberId);
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return order;
        }
    }
}
