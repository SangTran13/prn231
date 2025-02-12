using MediatR;

namespace Application.Products.Commands
{
    public class DeleteProductCommand : IRequest<Unit>
    {
        public int ProductId { get; set; }

        public DeleteProductCommand(int productId)
        {
            ProductId = productId;
        }
    }
}
