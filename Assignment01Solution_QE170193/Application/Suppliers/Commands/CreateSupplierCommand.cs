using MediatR;

namespace Application.Suppliers.Commands
{
    public class CreateSupplierCommand : IRequest<int>
    {
        public string SupplierName { get; set; } = string.Empty;

        public string SupplierAddress { get; set; } = string.Empty;

        public string Telephone { get; set; } = string.Empty;
    }
}
