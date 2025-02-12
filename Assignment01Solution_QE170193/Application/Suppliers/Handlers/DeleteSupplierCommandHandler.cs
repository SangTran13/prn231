using Application.Exceptions.Suppliers;
using Application.Suppliers.Commands;
using DataAccess.Interface;
using MediatR;

namespace Application.Suppliers.Handlers
{
    public class DeleteSupplierCommandHandler : IRequestHandler<DeleteSupplierCommand, Unit>
    {
        private readonly ISupplierRepository _supplierRepository;

        public DeleteSupplierCommandHandler(ISupplierRepository supplierRepository)
        {
            _supplierRepository = supplierRepository;
        }

        public async Task<Unit> Handle(DeleteSupplierCommand request, CancellationToken cancellationToken)
        {
            var supplier = await _supplierRepository.GetByIdAsync(request.SupplierId) ?? throw new SupplierNotFoundException();

            await _supplierRepository.DeleteAsync(supplier);
            return Unit.Value;
        }
    }
}
