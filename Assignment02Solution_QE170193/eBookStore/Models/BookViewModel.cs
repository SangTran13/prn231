using BusinessObject.Models;

namespace eBookStore.Models
{
    public class BookViewModel
    {
        public string? SearchTitle { get; set; }
        public decimal? SearchPrice { get; set; }

        public List<Book> Books { get; set; } = [];
    }
}
