using MediatR;

namespace Application.OrderDetails.Commands
{
    public class UpdateOrderDetailCommand : IRequest<Unit>
    {
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public decimal Discount { get; set; }
    }
}
