using BusinessObject.Models;
using DataAccess.Dto.OrderDto;
using DataAccess.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repository
{
    public class OrderDetailRepository : IOrderDetailRepository
    {
        private readonly EstoreDbContext _context;

        public OrderDetailRepository(EstoreDbContext context)
        {
            _context = context;
        }

        public async Task<OrderDetail?> FindOrderDetailById(int orderId)
        {
            return await _context.OrderDetails.FindAsync(orderId);
        }

        public async Task<List<OrderDetail>> GetOrderDetailByOrderId(int orderId)
        {
            return await _context.OrderDetails.Include(o => o.Product).Include(o => o.Order).Where(x => x.OrderId == orderId).AsNoTracking().ToListAsync();
        }

        public async Task<List<Order>> ReportOrder(DateTime? fromDate, DateTime? toDate)
        {
            List<Order>? listOrderDetail = null;
            try
            {
                if (fromDate == null && toDate == null)
                {
                    listOrderDetail = await _context.Orders.Include(u => u.Member).ToListAsync();
                }
                else if (fromDate != null && toDate == null)
                {
                    listOrderDetail = await _context.Orders.Include(u => u.Member).Where(x => x.OrderDate.Date == fromDate).ToListAsync();
                }
                else
                {
                    listOrderDetail = await _context.Orders.Include(u => u.Member).Where(x => x.OrderDate.Date >= fromDate && x.OrderDate.Date <= toDate).ToListAsync();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return listOrderDetail.OrderByDescending(o => o.Freight).ToList();
        }

        public async Task UpdateOrderDetail(OrderDetailUpdateRequestDto p)
        {
            var orderDetail = await _context.OrderDetails.FindAsync(p.OrderId, p.ProductId);
            if (orderDetail != null)
            {
                orderDetail.UnitPrice = p.UnitPrice;
                orderDetail.Quantity = p.Quantity;
                orderDetail.Discount = p.Discount;
                await _context.SaveChangesAsync();
            }
        }
    }
}
