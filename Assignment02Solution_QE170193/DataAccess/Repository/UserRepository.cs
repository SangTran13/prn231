using BusinessObject.Models;
using DataAccess.Repository.Interface;

namespace DataAccess.Repository
{
    public class UserRepository : RepositoryBase<User, int>, IUserRepository
    {
        public UserRepository(EBookStoreDBContext context) : base(context)
        {
        }
    }
}
