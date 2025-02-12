using Application.Mappings;
using Application.Suppliers.Queries;
using Application.Suppliers.Responses;
using DataAccess.Interface;
using MediatR;

namespace Application.Suppliers.Handlers
{
    public class GetAllSuppliersQueryHandler : IRequestHandler<GetAllSuppliersQuery, List<SupplierResponse>>
    {
        private readonly ISupplierRepository _supplierRepository;

        public GetAllSuppliersQueryHandler(ISupplierRepository supplierRepository)
        {
            _supplierRepository = supplierRepository;
        }

        public async Task<List<SupplierResponse>> Handle(GetAllSuppliersQuery request, CancellationToken cancellationToken)
        {
            var suppliers = await _supplierRepository.GetAllAsync();

            var supplierResponses = AppMapper<CoreMappingProfile>.Mapper.Map<List<SupplierResponse>>(suppliers);

            return supplierResponses;
        }
    }
}
