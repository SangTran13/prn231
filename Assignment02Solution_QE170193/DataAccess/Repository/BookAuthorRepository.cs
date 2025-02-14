using BusinessObject.Models;
using DataAccess.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repository
{
    public class BookAuthorRepository : RepositoryBase<BookAuthor, int>, IBookAuthorRepository
    {
        public BookAuthorRepository(EBookStoreDBContext context) : base(context)
        {
        }

        public async Task<IReadOnlyList<BookAuthor>> GetAllByBookIdAsync(int bookId)
        {
            return await _context.BookAuthor
                .Where(x => x.book_id == bookId).Include(ba => ba.Author).ToListAsync();
        }

        public async Task<BookAuthor?> GetByBookAndAuthorAsync(int bookId, int authorId)
        {
            return await _context.BookAuthor.Include(ba => ba.Author)
                .FirstOrDefaultAsync(x => x.book_id == bookId && x.author_id == authorId);
        }
    }
}
