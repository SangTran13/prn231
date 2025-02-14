using BusinessObject.Models;

namespace DataAccess.Repository.Interface
{
    public interface IBookAuthorRepository : IAsyncRepository<BookAuthor, int>
    {
        Task<IReadOnlyList<BookAuthor>> GetAllByBookIdAsync(int bookId);
        Task<BookAuthor?> GetByBookAndAuthorAsync(int bookId, int authorId);
    }
}
