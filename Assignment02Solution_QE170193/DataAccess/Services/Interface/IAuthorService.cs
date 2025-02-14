using BusinessObject.Models;

namespace DataAccess.Services.Interface
{
    public interface IAuthorService
    {
        Task<IReadOnlyList<Author>> GetAllAsync();

        IQueryable<Author> GetAll();
        Task<Author> GetAsync(int id);
        Task<Author> AddAsync(Author author);
        Task<bool> UpdateAsync(int id, Author author);
        Task<bool> DeleteAsync(int id);
    }
}
