using BusinessObject.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace eBookStore.Controllers
{
    public class PublishersController : Controller
    {
        private readonly HttpClient _client;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly string _publisherApiUri = "https://localhost:7158/api/Publisher";

        private readonly JsonSerializerOptions _jsonOptions = new() { PropertyNameCaseInsensitive = true };

        public PublishersController(IHttpClientFactory httpClientFactory, IHttpContextAccessor httpContextAccessor)
        {
            _client = httpClientFactory.CreateClient();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _httpContextAccessor = httpContextAccessor;
        }

        private string? GetUserRole() => _httpContextAccessor.HttpContext?.Session.GetString("Role");

        [HttpGet]
        public async Task<IActionResult> ViewPublisher()
        {
            if (GetUserRole() != "Admin") return RedirectToAction("Login", "Users");
            ViewData["Role"] = "Admin";

            var response = await _client.GetAsync(_publisherApiUri + "/GetAllPublisher");
            var publishers = response.IsSuccessStatusCode
                ? JsonSerializer.Deserialize<List<Publisher>>(await response.Content.ReadAsStringAsync(), _jsonOptions) ?? new()
                : new();

            return View(publishers);
        }

        [HttpGet]
        public IActionResult CreatePublisher()
        {
            if (GetUserRole() != "Admin") return RedirectToAction("Login", "Users");
            ViewData["Role"] = "Admin";
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreatePublisher(Publisher publisher)
        {
            if (GetUserRole() != "Admin") return RedirectToAction("Login", "Users");
            ViewData["Role"] = "Admin";

            var publisherRequest = new
            {
                publisherName = publisher.publisher_name,
                city = publisher.city,
                state = publisher.state,
                country = publisher.country
            };

            var jsonContent = new StringContent(JsonSerializer.Serialize(publisherRequest), Encoding.UTF8, "application/json");
            var response = await _client.PostAsync(_publisherApiUri, jsonContent);

            if (!response.IsSuccessStatusCode)
            {
                string errorMsg = await response.Content.ReadAsStringAsync();
                ViewData["Error"] = $"Failed to create publisher: {errorMsg}";
                return View(publisher);
            }

            return RedirectToAction(nameof(ViewPublisher));
        }

        [HttpGet]
        public async Task<IActionResult> EditPublisher(int id)
        {
            if (GetUserRole() != "Admin") return RedirectToAction("Login", "Users");
            ViewData["Role"] = "Admin";

            var response = await _client.GetAsync($"{_publisherApiUri}/{id}");
            if (!response.IsSuccessStatusCode) return NotFound();

            var publisher = JsonSerializer.Deserialize<Publisher>(await response.Content.ReadAsStringAsync(), _jsonOptions);
            return View(publisher);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPublisher(int id, Publisher publisher)
        {
            if (GetUserRole() != "Admin") return RedirectToAction("Login", "Users");
            ViewData["Role"] = "Admin";

            var publisherRequest = new
            {
                publisherName = publisher.publisher_name,
                city = publisher.city,
                state = publisher.state,
                country = publisher.country
            };

            var jsonContent = new StringContent(JsonSerializer.Serialize(publisherRequest), Encoding.UTF8, "application/json");
            var response = await _client.PutAsync($"{_publisherApiUri}/{id}", jsonContent);

            if (!response.IsSuccessStatusCode)
            {
                string errorMsg = await response.Content.ReadAsStringAsync();
                ViewData["Error"] = $"Failed to update publisher: {errorMsg}";
                return View(publisher);
            }

            return RedirectToAction(nameof(ViewPublisher));
        }

        [HttpGet]
        public async Task<IActionResult> DeletePublisher(int id)
        {
            if (GetUserRole() != "Admin") return RedirectToAction("Login", "Users");
            ViewData["Role"] = "Admin";

            var response = await _client.GetAsync($"{_publisherApiUri}/{id}");
            if (!response.IsSuccessStatusCode) return NotFound();

            var publisher = JsonSerializer.Deserialize<Publisher>(await response.Content.ReadAsStringAsync(), _jsonOptions);
            return View(publisher);
        }

        [HttpPost, ActionName("DeletePublisher")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirm(int id)
        {
            var response = await _client.DeleteAsync($"{_publisherApiUri}/{id}");

            if (!response.IsSuccessStatusCode)
            {
                ViewData["Error"] = $"Failed to delete publisher: {await response.Content.ReadAsStringAsync()}";
                return View();
            }

            return RedirectToAction(nameof(ViewPublisher));
        }
    }
}
