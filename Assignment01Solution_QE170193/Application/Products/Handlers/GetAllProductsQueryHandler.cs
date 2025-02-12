using Application.Exceptions.Products;
using Application.Mappings;
using Application.Products.Queries;
using Application.Products.Responses;
using DataAccess.Interface;
using MediatR;

namespace Application.Products.Handlers
{
    public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, List<ProductResponse>>
    {
        private readonly IProductRepository _productRepository;

        public GetAllProductsQueryHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<List<ProductResponse>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            var products = await _productRepository.GetAllProducts() ?? throw new ProductNotFoundException();

            return AppMapper<CoreMappingProfile>.Mapper.Map<List<ProductResponse>>(products);
        }


    }
}
