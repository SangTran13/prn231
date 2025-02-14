using BusinessObject.Models;

namespace DataAccess.Repository.Interface
{
    public interface IBookRepository : IAsyncRepository<Book, int>
    {
        Task<IReadOnlyList<Book>> GetAllWithConditionAsync(Func<Book, bool> predicate);
    }
}
