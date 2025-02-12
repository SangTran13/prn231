using Application.Exceptions.OrderDetails;
using Application.Mappings;
using Application.OrderDetails.Queries;
using Application.OrderDetails.Responses;
using DataAccess.Interface;
using MediatR;

namespace Application.OrderDetails.Handlers
{
    public class GetOrderDetailsByOrderIdQueryHandler : IRequestHandler<GetOrderDetailsByOrderIdQuery, List<OrderDetailResponse>>
    {
       
        private readonly IOrderDetailRepository _orderDetailRepository;

        public GetOrderDetailsByOrderIdQueryHandler(IOrderDetailRepository orderDetailRepository)
        {
            _orderDetailRepository = orderDetailRepository;
        }

        public async Task<List<OrderDetailResponse>> Handle(GetOrderDetailsByOrderIdQuery request, CancellationToken cancellationToken)
        {
            var orderDetails = await _orderDetailRepository.GetOrderDetailsByOrderIdAsync(request.OrderId) ?? throw new OrderDetailNotFoundException();

            return AppMapper<CoreMappingProfile>.Mapper.Map<List<OrderDetailResponse>>(orderDetails);
        }
    }
}
