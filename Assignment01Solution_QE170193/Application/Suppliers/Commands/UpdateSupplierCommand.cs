using MediatR;

namespace Application.Suppliers.Commands
{
    public class UpdateSupplierCommand : IRequest<int>
    {
        public int SupplierId { get; set; }

        public string SupplierName { get; set; } = string.Empty;

        public string SupplierAddress { get; set; } = string.Empty;

        public string Telephone { get; set; } = string.Empty;
    }
}
