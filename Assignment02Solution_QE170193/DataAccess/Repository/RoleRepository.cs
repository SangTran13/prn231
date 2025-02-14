using BusinessObject.Models;
using DataAccess.Repository.Interface;

namespace DataAccess.Repository
{
    public class RoleRepository : RepositoryBase<Role, int>, IRoleRepository
    {
        public RoleRepository(EBookStoreDBContext context) : base(context)
        {
        }
    }
}
