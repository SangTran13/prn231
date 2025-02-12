using MediatR;

namespace Application.Orders.Commands
{
    public class CreateOrderCommand : IRequest<int>
    {
        public decimal Total { get; set; }

        public string Freight { get; set; } = string.Empty;

        public int MemberId { get; set; }
    }
}
