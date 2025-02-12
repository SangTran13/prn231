using Application.Suppliers.Commands;
using BusinessObject;
using DataAccess.Interface;
using MediatR;

namespace Application.Suppliers.Handlers
{
    public class CreateSupplierCommandHandler : IRequestHandler<CreateSupplierCommand, int>
    {
        private readonly ISupplierRepository _supplierRepository;

        public CreateSupplierCommandHandler(ISupplierRepository supplierRepository)
        {
            _supplierRepository = supplierRepository;
        }

        public async Task<int> Handle(CreateSupplierCommand request, CancellationToken cancellationToken)
        {

            var supplier = new Supplier
            {
                SupplierName = request.SupplierName,
                SupplierAddress = request.SupplierAddress,
                Telephone = request.Telephone
            };

            await _supplierRepository.AddAsync(supplier);
            return supplier.SupplierId;
        }
    }
}
