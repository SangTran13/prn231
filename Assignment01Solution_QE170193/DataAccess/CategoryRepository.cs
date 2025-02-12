using BusinessObject;
using BusinessObjects;
using DataAccess.Interface;

namespace DataAccess
{
    public class CategoryRepository : RepositoryBase<Category, int>, ICategoryRepository
    {
        public CategoryRepository(EstoreDbContext context) : base(context)
        {

        }
    }
}
