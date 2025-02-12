using Application.Exceptions.Suppliers;
using Application.Suppliers.Commands;
using DataAccess.Interface;
using MediatR;

namespace Application.Suppliers.Handlers
{
    public class UpdateSupplierCommandHandler : IRequestHandler<UpdateSupplierCommand, int>
    {
        private readonly ISupplierRepository _supplierRepository;

        public UpdateSupplierCommandHandler(ISupplierRepository supplierRepository)
        {
            _supplierRepository = supplierRepository;
        }

        public async Task<int> Handle(UpdateSupplierCommand request, CancellationToken cancellationToken)
        {
            var supplier = await _supplierRepository.GetByIdAsync(request.SupplierId) ?? throw new SupplierNotFoundException();

            supplier.SupplierName = request.SupplierName;
            supplier.SupplierAddress = request.SupplierAddress;
            supplier.Telephone = request.Telephone;

            await _supplierRepository.UpdateAsync(supplier);
            return supplier.SupplierId;
        }
    }
}
