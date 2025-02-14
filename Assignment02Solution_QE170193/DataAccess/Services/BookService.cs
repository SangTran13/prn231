using BusinessObject.Models;
using DataAccess.Repository.Interface;
using DataAccess.Services.Interface;

namespace DataAccess.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository repository;
        private readonly IPublisherRepository publisherRepository;

        public BookService(IBookRepository repository, IPublisherRepository publisherRepository)
        {
            this.repository = repository;
            this.publisherRepository = publisherRepository;
        }

        private void ValidateBook(Book book)
        {
            if (string.IsNullOrEmpty(book.title)) throw new Exception("Title cannot be empty!");
            if (string.IsNullOrEmpty(book.type)) throw new Exception("Type cannot be empty!");
            if (book.pub_id == null) throw new Exception("Publisher is invalid!");
            if (book.price == null || book.price < 0) throw new Exception("Price is invalid!");
            if (string.IsNullOrEmpty(book.advance)) throw new Exception("Advance cannot be empty!");
            if (book.royalty == null || book.royalty < 0) throw new Exception("Royalty is invalid!");
            if (string.IsNullOrEmpty(book.notes)) throw new Exception("Notes cannot be empty!");
            if (book.published_date == null) throw new Exception("Published date cannot be empty!");
        }

        public async Task<IReadOnlyList<Book>> GetAllAsync()
        {
            try
            {
                var booksDB = await repository.GetAllAsync();
                foreach (var book in booksDB)
                {
                    book.Publisher = await publisherRepository.GetByIdAsync(book.pub_id);
                    if (book.Publisher != null)
                    {
                        book.Publisher.Books.Clear();
                    }
                }
                return booksDB;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Book> GetByIdAsync(int id)
        {
            try
            {
                var bookDB = await repository.GetByIdAsync(id);
                if (bookDB == null)
                {
                    throw new Exception("Book not found");
                }

                bookDB.Publisher = await publisherRepository.GetByIdAsync(bookDB.pub_id);
                if (bookDB.Publisher != null)
                {
                    bookDB.Publisher.Books.Clear();
                }

                return bookDB;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Book> AddAsync(Book book)
        {
            try
            {
                ValidateBook(book);
                return await repository.AddAsync(book);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> UpdateAsync(int id, Book book)
        {
            try
            {
                var bookDB = await repository.GetByIdAsync(id);
                if (bookDB == null)
                {
                    throw new Exception("Book not found");
                }

                ValidateBook(book);

                bookDB.title = book.title;
                bookDB.type = book.type;
                bookDB.pub_id = book.pub_id;
                bookDB.price = book.price;
                bookDB.advance = book.advance;
                bookDB.royalty = book.royalty;
                bookDB.ytd_sales = book.ytd_sales;
                bookDB.notes = book.notes;
                bookDB.published_date = book.published_date;

                await repository.UpdateAsync(bookDB);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var book = await repository.GetByIdAsync(id);
                if (book == null)
                {
                    return false;
                }

                await repository.DeleteAsync(book);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IReadOnlyList<Book>> SearchAsync(string search)
        {
            try
            {
                if (double.TryParse(search, out double price))
                {
                    return await repository.GetAllWithConditionAsync(x => x.price == price);
                }
                else
                {
                    return await repository.GetAllWithConditionAsync(x => x.title.Contains(search));
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
