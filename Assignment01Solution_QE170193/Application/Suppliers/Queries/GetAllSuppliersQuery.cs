using Application.Suppliers.Responses;
using MediatR;

namespace Application.Suppliers.Queries
{
    public class GetAllSuppliersQuery : IRequest<List<SupplierResponse>>
    {

    }
}
