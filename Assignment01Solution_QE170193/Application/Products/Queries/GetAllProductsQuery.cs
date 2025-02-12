using Application.Products.Responses;
using MediatR;

namespace Application.Products.Queries
{
    public class GetAllProductsQuery : IRequest<List<ProductResponse>>
    {
    }
}
