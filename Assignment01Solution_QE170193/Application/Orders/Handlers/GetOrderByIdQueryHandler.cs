using Application.Exceptions.Orders;
using Application.Mappings;
using Application.Orders.Queries;
using Application.Orders.Responses;
using DataAccess.Interface;
using MediatR;

namespace Application.Orders.Handlers
{
    public class GetOrderByIdQueryHandler : IRequestHandler<GetOrderByIdQuery, OrderResponse>
    {
        private readonly IOrderRepository _orderRepository;

        public GetOrderByIdQueryHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<OrderResponse> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetOrderById(request.OrderId) ?? throw new OrderNotFoundException();

            return AppMapper<CoreMappingProfile>.Mapper.Map<OrderResponse>(order);
        }
    }
}
