using BusinessObject.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repository.Interface
{
    public interface ICategoryRepository
    {
        Task SaveCategory(Category c);
        Task<Category?> GetCategoryById(int id);
        Task DeleteCategory(Category c);
        Task UpdateCategory(Category c);
        Task<List<Category>> GetCategories();
    }
}
