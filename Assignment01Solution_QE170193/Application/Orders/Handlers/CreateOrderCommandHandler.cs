using Application.Exceptions.Members;
using Application.Orders.Commands;
using BusinessObject;
using DataAccess.Interface;
using MediatR;

namespace Application.Orders.Handlers
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, int>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMemberRepository _memberRepository;
        public CreateOrderCommandHandler(IOrderRepository orderRepository, IMemberRepository memberRepository)
        {
            _orderRepository = orderRepository;
            _memberRepository = memberRepository;
        }
        public async Task<int> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            _ = await _memberRepository.GetByIdAsync(request.MemberId) ?? throw new MemberNotFoundException();

            var order = new Order
            {
                OrderDate = DateTime.UtcNow,
                ShippedDate = null,
                Total = request.Total,
                OrderStatus = 0,
                Freight = request.Freight,
                MemberId = request.MemberId
            };
            await _orderRepository.AddAsync(order);
            return order.OrderId;
        }
    }
}
