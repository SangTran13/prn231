using BusinessObject.Models;

namespace DataAccess.Services.Interface
{
    public interface IPublisherService
    {
        Task<IReadOnlyList<Publisher>> GetAllAsync();
        Task<Publisher> GetByIdAsync(int id);
        Task<Publisher> AddAsync(Publisher publisher);
        Task<bool> UpdateAsync(int id, Publisher publisher);
        Task<bool> DeleteAsync(int id);
    }
}
