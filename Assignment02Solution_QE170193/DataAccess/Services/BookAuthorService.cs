using BusinessObject.Models;
using DataAccess.Repository.Interface;
using DataAccess.Services.Interface;

namespace DataAccess.Services
{
    public class BookAuthorService : IBookAuthorService
    {
        private readonly IBookAuthorRepository repository;
        private readonly IAuthorRepository authorRepository;

        public BookAuthorService(IBookAuthorRepository repository, IAuthorRepository authorRepository)
        {
            this.repository = repository;
            this.authorRepository = authorRepository;
        }

        public async Task<IReadOnlyList<BookAuthor>> GetAllAsync()
        {
            try
            {
                return await repository.GetAllAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IReadOnlyList<BookAuthor>> GetAllByBookIdAsync(int bookId)
        {
            try
            {
                return await repository.GetAllByBookIdAsync(bookId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<BookAuthor> AddAsync(BookAuthor bookAuthor)
        {
            try
            {
                var existingEntry = await repository.GetByBookAndAuthorAsync(bookAuthor.book_id, bookAuthor.author_id);
                if (existingEntry != null)
                {
                    throw new Exception("Author already exists for this book.");
                }

                return await repository.AddAsync(bookAuthor);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> DeleteAsync(int bookId, int authorId)
        {
            try
            {
                var bookAuthor = await repository.GetByBookAndAuthorAsync(bookId, authorId);
                if (bookAuthor != null)
                {
                    await repository.DeleteAsync(bookAuthor);
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
