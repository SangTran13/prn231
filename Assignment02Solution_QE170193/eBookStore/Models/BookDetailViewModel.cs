using BusinessObject.Models;

namespace eBookStore.Models
{
    public class BookDetailViewModel
    {
        public Book Book { get; set; } = null!;

        public string AuthorId { get; set; } = string.Empty;

        public List<BookAuthor> BookAuthors { get; set; } = new List<BookAuthor>();
    }
}
