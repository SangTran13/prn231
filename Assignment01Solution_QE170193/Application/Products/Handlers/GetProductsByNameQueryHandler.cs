using Application.Exceptions.Products;
using Application.Mappings;
using Application.Products.Queries;
using Application.Products.Responses;
using DataAccess.Interface;
using MediatR;

namespace Application.Products.Handlers
{
    public class GetProductsByNameQueryHandler : IRequestHandler<GetProductsByNameQuery, List<ProductResponse>>
    {
        private readonly IProductRepository _productRepository;

        public GetProductsByNameQueryHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<List<ProductResponse>> Handle(GetProductsByNameQuery request, CancellationToken cancellationToken)
        {
            var products = await _productRepository.GetProductsByNameAsync(request.ProductName, request.UnitPrice) ?? throw new ProductNotFoundException();
            return AppMapper<CoreMappingProfile>.Mapper.Map<List<ProductResponse>>(products);
        }
    }
}
