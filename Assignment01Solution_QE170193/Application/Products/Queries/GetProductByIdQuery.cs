using Application.Products.Responses;
using MediatR;

namespace Application.Products.Queries
{
    public class GetProductByIdQuery : IRequest<ProductResponse>
    {
        public int ProductId { get; set; }
        public GetProductByIdQuery(int productId)
        {
            ProductId = productId;
        }
    }
}
