using Application.Products.Responses;
using MediatR;

namespace Application.Products.Queries
{
    public class GetProductsByNameQuery : IRequest<List<ProductResponse>>
    {
        public string ProductName { get; set; } = string.Empty;

        public decimal UnitPrice { get; set; } = 0;

        public GetProductsByNameQuery(string? productName = null, decimal unitPrice = 0)
        {
            ProductName = productName ?? string.Empty;
            UnitPrice = unitPrice;
        }
    }
}
