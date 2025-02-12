using Application.Exceptions.Orders;
using Application.Orders.Commands;
using DataAccess.Interface;
using MediatR;

namespace Application.Orders.Handlers
{
    public class UpdateOrderStatusCommandHandler : IRequestHandler<UpdateOrderStatusCommand, Unit>
    {
        private readonly IOrderRepository _orderRepository;

        public UpdateOrderStatusCommandHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<Unit> Handle(UpdateOrderStatusCommand request, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetByIdAsync(request.OrderId) ?? throw new OrderNotFoundException();

            order.OrderStatus = request.NewStatus;

            if (order.OrderStatus == 1)
            {
                order.ShippedDate = DateTime.UtcNow;
            }
            await _orderRepository.UpdateAsync(order);
            return Unit.Value;
        }

    }
}
