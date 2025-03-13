using BusinessObject.Models;
using DataAccess.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly EstoreDbContext _context;

        public CategoryRepository(EstoreDbContext context)
        {
            _context = context;
        }

        public async Task DeleteCategory(Category c)
        {
            _context.Categories.Remove(c);
            await _context.SaveChangesAsync();
        }

        public async Task<Category?> GetCategoryById(int id)
        {
            return await _context.Categories.FindAsync(id);
        }

        public async Task SaveCategory(Category c)
        {
            await _context.Categories.AddAsync(c);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateCategory(Category c)
        {
            _context.Categories.Update(c);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Category>> GetCategories()
        {
            return await _context.Categories.ToListAsync();
        }

    }
}
