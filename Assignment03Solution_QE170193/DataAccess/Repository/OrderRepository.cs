using BusinessObject.Models;
using DataAccess.Dto.OrderDto;
using DataAccess.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly EstoreDbContext _context;

        public OrderRepository(EstoreDbContext context)
        {
            _context = context;
        }

        public async Task DeleteOrder(Order p)
        {
            var orderDetails = await _context.OrderDetails.Where(o => o.OrderId == p.OrderId).ToListAsync();
            _context.OrderDetails.RemoveRange(orderDetails);
            _context.Orders.Remove(p);
            await _context.SaveChangesAsync();
        }

        public async Task<Order?> GetOrderById(int id)
        {
            return await _context.Orders.FindAsync(id);
        }

        public async Task<List<Order>> GetOrders(string? keyword)
        {
            if (string.IsNullOrEmpty(keyword))
            {
                return await _context.Orders.Include(u => u.Member).Include(o => o.OrderDetails).ToListAsync();
            }
            return await _context.Orders.Include(u => u.Member).Include(o => o.OrderDetails).Where(p => p.Member.Email!.ToUpper().ToString().Contains(keyword.ToUpper())).AsNoTracking().ToListAsync();
        }

        public async Task<List<Order>> GetOrdersByMemberId(Guid memberId, string? keyword)
        {
            if (string.IsNullOrEmpty(keyword))
            {
                return await _context.Orders.Include(u => u.Member).Include(o => o.OrderDetails).Where(p => p.MemberId.Equals(memberId.ToString())).ToListAsync();
            }

            return await _context.Orders.Include(u => u.Member).Include(o => o.OrderDetails).Where(p => p.MemberId.Equals(memberId.ToString()) && p.OrderId.ToString().Contains(keyword)).ToListAsync();
        }

        public async Task SaveOrder(OrderRequestDto p)
        {
            var order = new Order
            {
                MemberId = p.MemberId.ToString(),
                OrderDate = p.OrderDate,
                RequiredDate = p.RequireDate,
                ShippedDate = p.ShippedDate,
                Freight = p.Freight
            };
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
            foreach (var item in p.OrderItems)
            {
                var orderItem = new OrderDetail
                {
                    OrderId = order.OrderId,
                    ProductId = item.ProductId,
                    UnitPrice = item.UnitPrice,
                    Quantity = item.Quantity,
                    Discount = item.Discount
                };
                _context.OrderDetails.Add(orderItem);
            }
            await _context.SaveChangesAsync();
        }

        public async Task UpdateOrder(OrderUpdateRequestDto p)
        {
            var order = await _context.Orders.FindAsync(p.OrderId);
            if (order != null)
            {
                order.MemberId = p.MemberId.ToString();
                order.OrderDate = p.OrderDate;
                order.RequiredDate = p.RequireDate;
                order.ShippedDate = p.ShippedDate;
                order.Freight = p.Freight;
                await _context.SaveChangesAsync();
            }
        }
    }
}
