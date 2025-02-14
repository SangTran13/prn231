using BusinessObject.Models;

namespace DataAccess.Services.Interface
{
    public interface IBookAuthorService
    {
        Task<IReadOnlyList<BookAuthor>> GetAllAsync();
        Task<IReadOnlyList<BookAuthor>> GetAllByBookIdAsync(int bookId);
        Task<BookAuthor> AddAsync(BookAuthor bookAuthor);
        Task<bool> DeleteAsync(int bookId, int authorId);
    }
}
