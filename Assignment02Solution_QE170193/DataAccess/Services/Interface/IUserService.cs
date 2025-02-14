using BusinessObject.Models;

namespace DataAccess.Services.Interface
{
    public interface IUserService
    {
        Task<IReadOnlyList<User>> GetAllAsync();
        Task<User> GetByIdAsync(int id);
        Task<User> AddAsync(User user);
        Task<bool> UpdateAsync(int id, User user);
        Task<bool> DeleteAsync(int id);
        Task<User?> CheckLoginAsync(string email);
    }
}
