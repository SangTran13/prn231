using Application.Exceptions.Products;
using Application.Mappings;
using Application.Products.Queries;
using Application.Products.Responses;
using DataAccess.Interface;
using MediatR;

namespace Application.Products.Handlers
{
    public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, ProductResponse>
    {
        private readonly IProductRepository _productRepository;

        public GetProductByIdQueryHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<ProductResponse> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetByIdAsync(request.ProductId) ?? throw new ProductNotFoundException();
            return AppMapper<CoreMappingProfile>.Mapper.Map<ProductResponse>(product);
        }

    }
}
