using BusinessObject.Models;
using eBookStore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace eBookStore.Controllers
{
    public class BooksController : Controller
    {
        private readonly HttpClient _client;
        private readonly IHttpContextAccessor _httpContextAccessor;

        private readonly string _bookApiUri = "https://localhost:7158/api/Book/";
        private readonly string _odataBookApiUri = "https://localhost:7158/odata/Book";
        private readonly string _publisherApiUri = "https://localhost:7158/api/Publisher/GetAllPublisher";
        private readonly string _bookAuthorApiUri = "https://localhost:7158/api/BookAuthor";

        private readonly JsonSerializerOptions _jsonOptions = new() { PropertyNameCaseInsensitive = true };

        public BooksController(IHttpClientFactory httpClientFactory, IHttpContextAccessor httpContextAccessor)
        {
            _client = httpClientFactory.CreateClient();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _httpContextAccessor = httpContextAccessor;
        }

        private string? GetUserRole() => _httpContextAccessor.HttpContext?.Session.GetString("Role");

        [HttpGet]
        public async Task<List<Publisher>> GetAllPublisher()
        {
            var response = await _client.GetAsync(_publisherApiUri);
            response.EnsureSuccessStatusCode();
            return JsonSerializer.Deserialize<List<Publisher>>(await response.Content.ReadAsStringAsync(), _jsonOptions) ?? new();
        }

        [HttpGet]
        public async Task<IActionResult> ViewBook(string? searchTitle, decimal? searchPrice)
        {
            if (GetUserRole() != "Admin") return RedirectToAction("Login", "Users");
            ViewData["Role"] = "Admin";

            var api = _odataBookApiUri;
            List<string> filters = new();

            if (!string.IsNullOrEmpty(searchTitle))
            {
                filters.Add($"contains(title, '{searchTitle}')");
            }

            if (searchPrice.HasValue)
            {
                filters.Add($"price eq {searchPrice.Value}");
            }

            if (filters.Any())
            {
                api += $"?$filter=" + string.Join(" and ", filters);
            }

            var response = await _client.GetAsync(api);
            var books = response.IsSuccessStatusCode
                ? JsonSerializer.Deserialize<Dictionary<string, object>>(await response.Content.ReadAsStringAsync(), _jsonOptions)?["value"]?.ToString() ?? "[]"
                : "[]";

            return View(new BookViewModel { SearchTitle = searchTitle, SearchPrice = searchPrice, Books = JsonSerializer.Deserialize<List<Book>>(books, _jsonOptions) ?? new() });
        }


        [HttpGet]
        public async Task<IActionResult> DetailBook(int id)
        {
            if (GetUserRole() != "Admin") return RedirectToAction("Login", "Users");
            ViewData["Role"] = "Admin";

            var response = await _client.GetAsync($"{_bookApiUri}{id}");
            if (!response.IsSuccessStatusCode) return NotFound();

            var book = JsonSerializer.Deserialize<Book>(await response.Content.ReadAsStringAsync(), _jsonOptions);
            return View(new BookDetailViewModel { Book = book, BookAuthors = await GetAllBookAuthorById(id) });
        }

        [HttpGet]
        public async Task<List<BookAuthor>> GetAllBookAuthorById(int bookId)
        {
            var response = await _client.GetAsync($"{_bookAuthorApiUri}/GetAllByBookId/{bookId}");
            return response.IsSuccessStatusCode ? JsonSerializer.Deserialize<List<BookAuthor>>(await response.Content.ReadAsStringAsync(), _jsonOptions) ?? new() : new();
        }

        [HttpGet]
        public async Task<IActionResult> CreateBook()
        {
            if (GetUserRole() != "Admin") return RedirectToAction("Login", "Users");
            ViewData["Role"] = "Admin";

            ViewData["PublisherId"] = new SelectList(await GetAllPublisher(), "pub_id", "publisher_name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateBook(Book book)
        {
            if (GetUserRole() != "Admin") return RedirectToAction("Login", "Users");
            ViewData["Role"] = "Admin";

            if (book.price < 0 || book.royalty < 0 || book.ytd_sales < 0)
            {
                ViewData["Price"] = book.price < 0 ? "Price cannot be less than 0" : null;
                ViewData["Royalty"] = book.royalty < 0 ? "Royalty cannot be less than 0" : null;
                ViewData["YtdSales"] = book.ytd_sales < 0 ? "YtdSales cannot be less than 0" : null;
                ViewData["PublisherId"] = new SelectList(await GetAllPublisher(), "pub_id", "publisher_name");
                return View(book);
            }

            var bookRequest = new
            {
                title = book.title,
                type = book.type,
                publisherId = book.pub_id,
                price = book.price,
                advance = book.advance,
                royalty = book.royalty,
                ytdSales = book.ytd_sales,
                notes = book.notes,
                publishedDate = book.published_date
            };

            var jsonContent = new StringContent(JsonSerializer.Serialize(bookRequest), Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("https://localhost:7158/api/Book", jsonContent);

            if (!response.IsSuccessStatusCode)
            {
                string errorMsg = await response.Content.ReadAsStringAsync();
                ViewData["Error"] = $"Failed to create book: {errorMsg}";
                ViewData["PublisherId"] = new SelectList(await GetAllPublisher(), "pub_id", "publisher_name");
                return View(book);
            }

            return RedirectToAction(nameof(ViewBook));
        }

        [HttpGet]
        public async Task<IActionResult> EditBook(int id)
        {
            if (GetUserRole() != "Admin") return RedirectToAction("Login", "Users");
            ViewData["Role"] = "Admin";

            var response = await _client.GetAsync($"{_bookApiUri}{id}");
            if (!response.IsSuccessStatusCode) return NotFound();

            var book = System.Text.Json.JsonSerializer.Deserialize<Book>(await response.Content.ReadAsStringAsync(), _jsonOptions);
            ViewData["PublisherId"] = new SelectList(await GetAllPublisher(), "pub_id", "publisher_name");

            return View(book);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditBook(int id, Book book)
        {
            if (GetUserRole() != "Admin") return RedirectToAction("Login", "Users");
            ViewData["Role"] = "Admin";

            if (book.price < 0 || book.royalty < 0 || book.ytd_sales < 0)
            {
                ViewData["Price"] = book.price < 0 ? "Price cannot be less than 0" : null;
                ViewData["Royalty"] = book.royalty < 0 ? "Royalty cannot be less than 0" : null;
                ViewData["YtdSales"] = book.ytd_sales < 0 ? "YtdSales cannot be less than 0" : null;
                ViewData["PublisherId"] = new SelectList(await GetAllPublisher(), "pub_id", "publisher_name");
                return View(book);
            }

            var bookRequest = new
            {
                title = book.title,
                type = book.type,
                publisherId = book.pub_id,
                price = book.price,
                advance = book.advance,
                royalty = book.royalty,
                ytdSales = book.ytd_sales,
                notes = book.notes,
                publishedDate = book.published_date
            };

            var jsonContent = new StringContent(System.Text.Json.JsonSerializer.Serialize(bookRequest), Encoding.UTF8, "application/json");

            var response = await _client.PutAsync($"{_bookApiUri}{id}", jsonContent);

            if (!response.IsSuccessStatusCode)
            {
                ViewData["Error"] = $"Failed to update book: {await response.Content.ReadAsStringAsync()}";
                ViewData["PublisherId"] = new SelectList(await GetAllPublisher(), "pub_id", "publisher_name");
                return View(book);
            }

            return RedirectToAction(nameof(ViewBook));
        }


        [HttpGet]
        public async Task<IActionResult> DeleteBook(int id)
        {
            if (GetUserRole() != "Admin") return RedirectToAction("Login", "Users");
            ViewData["Role"] = "Admin";

            var response = await _client.GetAsync($"{_bookApiUri}{id}");
            if (!response.IsSuccessStatusCode) return NotFound();

            var book = System.Text.Json.JsonSerializer.Deserialize<Book>(await response.Content.ReadAsStringAsync(), _jsonOptions);
            return View(book);
        }

        [HttpPost, ActionName("DeleteBook")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirm(int id)
        {
            var response = await _client.DeleteAsync($"{_bookApiUri}{id}");

            if (!response.IsSuccessStatusCode)
            {
                ViewData["Error"] = $"Failed to delete book: {await response.Content.ReadAsStringAsync()}";
                return View();
            }

            return RedirectToAction(nameof(ViewBook));
        }

    }
}