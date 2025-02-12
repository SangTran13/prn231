using MediatR;

namespace Application.Orders.Commands
{
    public class UpdateOrderStatusCommand : IRequest<Unit>
    {
        public int OrderId { get; set; }

        public int NewStatus { get; set; }

        public DateTime? ShippedDate { get; set; }
    }
}
