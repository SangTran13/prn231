using Application.Suppliers.Responses;
using MediatR;

namespace Application.Suppliers.Queries
{
    public class GetSupplierByIdQuery : IRequest<SupplierResponse>
    {
        public int SupplierId { get; set; }

        public GetSupplierByIdQuery(int supplierId)
        {
            SupplierId = supplierId;
        }
    }
}
