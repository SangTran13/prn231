using BusinessObject.Models;
using DataAccess.Repository.Interface;

namespace DataAccess.Repository
{
    public class BookRepository : RepositoryBase<Book, int>, IBookRepository
    {
        public BookRepository(EBookStoreDBContext context) : base(context)
        {
        }

        public async Task<IReadOnlyList<Book>> GetAllWithConditionAsync(Func<Book, bool> predicate)
        {
            return await Task.Run(() => _context.Book.Where(predicate).ToList());
        }
    }
}
