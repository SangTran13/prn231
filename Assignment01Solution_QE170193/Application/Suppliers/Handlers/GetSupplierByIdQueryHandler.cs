using Application.Exceptions.Suppliers;
using Application.Mappings;
using Application.Suppliers.Queries;
using Application.Suppliers.Responses;
using DataAccess.Interface;
using MediatR;

namespace Application.Suppliers.Handlers
{
    public class GetSupplierByIdQueryHandler : IRequestHandler<GetSupplierByIdQuery, SupplierResponse>
    {
        private readonly ISupplierRepository _supplierRepository;

        public GetSupplierByIdQueryHandler(ISupplierRepository supplierRepository)
        {
            _supplierRepository = supplierRepository;
        }

        public async Task<SupplierResponse> Handle(GetSupplierByIdQuery request, CancellationToken cancellationToken)
        {
            var supplier = await _supplierRepository.GetByIdAsync(request.SupplierId) ?? throw new SupplierNotFoundException();

            var supplierResponse = AppMapper<CoreMappingProfile>.Mapper.Map<SupplierResponse>(supplier);
            return supplierResponse;
        }
    }
}
