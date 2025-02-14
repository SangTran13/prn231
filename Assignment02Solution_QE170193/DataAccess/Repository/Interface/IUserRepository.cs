using BusinessObject.Models;

namespace DataAccess.Repository.Interface
{
    public interface IUserRepository : IAsyncRepository<User, int>
    {
    }
}
