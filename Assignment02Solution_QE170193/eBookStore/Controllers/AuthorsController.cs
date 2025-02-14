using BusinessObject.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace eBookStore.Controllers
{
    public class AuthorsController : Controller
    {
        private readonly HttpClient _client;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly string _authorApiUri = "https://localhost:7158/api/Author/";

        private readonly JsonSerializerOptions _jsonOptions = new() { PropertyNameCaseInsensitive = true };

        public AuthorsController(IHttpClientFactory httpClientFactory, IHttpContextAccessor httpContextAccessor)
        {
            _client = httpClientFactory.CreateClient();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _httpContextAccessor = httpContextAccessor;
        }

        private string? GetUserRole() => _httpContextAccessor.HttpContext?.Session.GetString("Role");

        [HttpGet]
        public async Task<IActionResult> ViewAuthor()
        {
            if (GetUserRole() != "Admin") return RedirectToAction("Login", "Users");
            ViewData["Role"] = "Admin";

            var response = await _client.GetAsync("https://localhost:7158/api/Author/Get");
            var authors = response.IsSuccessStatusCode
                ? JsonSerializer.Deserialize<List<Author>>(await response.Content.ReadAsStringAsync(), _jsonOptions) ?? new()
                : new();

            return View(authors);
        }

        [HttpGet]
        public IActionResult CreateAuthor()
        {
            if (GetUserRole() != "Admin") return RedirectToAction("Login", "Users");
            ViewData["Role"] = "Admin";
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateAuthor(Author author)
        {
            if (GetUserRole() != "Admin") return RedirectToAction("Login", "Users");
            ViewData["Role"] = "Admin";

            var authorRequest = new
            {
                lastName = author.last_name,
                firstName = author.first_name,
                phone = author.phone,
                address = author.address,
                city = author.city,
                state = author.state,
                zip = author.zip,
                emailAddress = author.email_address
            };

            var jsonContent = new StringContent(JsonSerializer.Serialize(authorRequest), Encoding.UTF8, "application/json");
            var response = await _client.PostAsync($"{_authorApiUri}AddAuthor", jsonContent);

            if (!response.IsSuccessStatusCode)
            {
                string errorMsg = await response.Content.ReadAsStringAsync();
                ViewData["Error"] = $"Failed to create author: {errorMsg}";
                return View(author);
            }

            return RedirectToAction(nameof(ViewAuthor));
        }

        [HttpGet]
        public async Task<IActionResult> EditAuthor(int id)
        {
            if (GetUserRole() != "Admin") return RedirectToAction("Login", "Users");
            ViewData["Role"] = "Admin";

            var response = await _client.GetAsync($"{_authorApiUri}GetAuthorById/{id}");
            if (!response.IsSuccessStatusCode) return NotFound();

            var author = JsonSerializer.Deserialize<Author>(await response.Content.ReadAsStringAsync(), _jsonOptions);
            return View(author);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditAuthor(int id, Author author)
        {
            if (GetUserRole() != "Admin") return RedirectToAction("Login", "Users");
            ViewData["Role"] = "Admin";

            var authorRequest = new
            {
                lastName = author.last_name,
                firstName = author.first_name,
                phone = author.phone,
                address = author.address,
                city = author.city,
                state = author.state,
                zip = author.zip,
                emailAddress = author.email_address
            };

            var jsonContent = new StringContent(JsonSerializer.Serialize(authorRequest), Encoding.UTF8, "application/json");
            var response = await _client.PutAsync($"{_authorApiUri}UpdateAuthor/{id}", jsonContent);

            if (!response.IsSuccessStatusCode)
            {
                string errorMsg = await response.Content.ReadAsStringAsync();
                ViewData["Error"] = $"Failed to update author: {errorMsg}";
                return View(author);
            }

            return RedirectToAction(nameof(ViewAuthor));
        }

        [HttpGet]
        public async Task<IActionResult> DeleteAuthor(int id)
        {
            if (GetUserRole() != "Admin") return RedirectToAction("Login", "Users");
            ViewData["Role"] = "Admin";

            var response = await _client.GetAsync($"{_authorApiUri}GetAuthorById/{id}");
            if (!response.IsSuccessStatusCode) return NotFound();

            var author = JsonSerializer.Deserialize<Author>(await response.Content.ReadAsStringAsync(), _jsonOptions);
            return View(author);
        }

        [HttpPost, ActionName("DeleteAuthor")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirm(int id)
        {
            var response = await _client.DeleteAsync($"{_authorApiUri}DeleteAuthor/{id}");

            if (!response.IsSuccessStatusCode)
            {
                ViewData["Error"] = $"Failed to delete author: {await response.Content.ReadAsStringAsync()}";
                return View();
            }

            return RedirectToAction(nameof(ViewAuthor));
        }
    }
}
