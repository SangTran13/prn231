using Application.Exceptions.OrderDetails;
using Application.Exceptions.Orders;
using Application.Exceptions.Products;
using Application.Mappings;
using Application.OrderDetails.Queries;
using Application.OrderDetails.Responses;
using DataAccess.Interface;
using MediatR;

namespace Application.OrderDetails.Handlers
{
    public class GetOrderDetailByOrderIdAndProductIdQueryHandler : IRequestHandler<GetOrderDetailByOrderIdAndProductIdQuery, OrderDetailResponse>
    {
        private readonly IOrderDetailRepository _orderDetailRepository;
        private readonly IProductRepository _productRepository;
        private readonly IOrderRepository _orderRepository;

        public GetOrderDetailByOrderIdAndProductIdQueryHandler(IOrderDetailRepository orderDetailRepository, IProductRepository productRepository, IOrderRepository orderRepository)
        {
            _orderDetailRepository = orderDetailRepository;
            _productRepository = productRepository;
            _orderRepository = orderRepository;
        }

        public async Task<OrderDetailResponse> Handle(GetOrderDetailByOrderIdAndProductIdQuery request, CancellationToken cancellationToken)
        {
            _ = await _orderRepository.GetByIdAsync(request.OrderId) ?? throw new OrderNotFoundException();
            _ = await _productRepository.GetByIdAsync(request.ProductId) ?? throw new ProductNotFoundException();

            var orderDetail = await _orderDetailRepository.GetOrderDetailByOrderIdAndProductIdAsync(request.OrderId, request.ProductId) ?? throw new OrderDetailNotFoundException();

            return AppMapper<CoreMappingProfile>.Mapper.Map<OrderDetailResponse>(orderDetail);
        }
    }
}
