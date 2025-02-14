using BusinessObject.Models;

namespace DataAccess.Services.Interface
{
    public interface IBookService
    {
        Task<IReadOnlyList<Book>> GetAllAsync();
        Task<Book> GetByIdAsync(int id);
        Task<Book> AddAsync(Book book);
        Task<bool> UpdateAsync(int id, Book book);
        Task<bool> DeleteAsync(int id);
        Task<IReadOnlyList<Book>> SearchAsync(string search);
    }
}
