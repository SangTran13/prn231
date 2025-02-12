using MediatR;

namespace Application.Products.Commands
{
    public class UpdateProductCommand : IRequest<Unit>
    {
        public int ProductId { get; set; }

        public string ProductName { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public decimal UnitPrice { get; set; }

        public int UnitsInStock { get; set; }

        public int ProductStatus { get; set; }

        public int CategoryId { get; set; }

        public int SupplierId { get; set; }
    }
}
