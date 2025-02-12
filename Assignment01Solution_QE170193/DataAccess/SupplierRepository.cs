using BusinessObject;
using BusinessObjects;
using DataAccess.Interface;

namespace DataAccess
{
    public class SupplierRepository : RepositoryBase<Supplier, int>, ISupplierRepository
    {
        public SupplierRepository(EstoreDbContext context) : base(context)
        {

        }
    }
}
