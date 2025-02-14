using BusinessObject.Models;

namespace DataAccess.Repository.Interface
{
    public interface IAuthorRepository : IAsyncRepository<Author, int>
    {
        IQueryable<Author> GetAll();
    }
}
