using MediatR;

namespace Application.Suppliers.Commands
{
    public class DeleteSupplierCommand : IRequest<Unit>
    {
        public int SupplierId { get; set; }

        public DeleteSupplierCommand(int supplierId)
        {
            SupplierId = supplierId;
        }
    }
}
