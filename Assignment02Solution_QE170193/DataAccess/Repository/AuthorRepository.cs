using BusinessObject.Models;
using DataAccess.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repository
{
    public class AuthorRepository : RepositoryBase<Author, int>, IAuthorRepository
    {
        private readonly EBookStoreDBContext _context;

        public AuthorRepository(EBookStoreDBContext context) : base(context)
        {
            _context = context;
        }

        public IQueryable<Author> GetAll()
        {
            return _context.Author.AsNoTracking();
        }
    }
}
