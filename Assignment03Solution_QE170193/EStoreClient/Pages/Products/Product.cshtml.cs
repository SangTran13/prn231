using BusinessObject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http.Headers;
using System.Text.Json;

namespace EStoreClient.Pages.Products
{
    public class ProductModel : PageModel
    {
        private readonly HttpClient client;
        private readonly IConfiguration _configuration;
        private readonly string ProductApiUrl;

        public List<Product> ListProduct { get; set; } = new List<Product>();

        public ProductModel(IConfiguration configuration)
        {
            _configuration = configuration;
            client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            ProductApiUrl = _configuration.GetValue<string>("DomainURL") + "Product/GetAllProduct";
        }

        public async Task<IActionResult> OnGetAsync(string? keyword, string? price)
        {
            string url = ProductApiUrl;
            if (!string.IsNullOrEmpty(keyword) || !string.IsNullOrEmpty(price))
            {
                url += $"?keyword={keyword}&unitP={price}";
            }

            HttpResponseMessage response = await client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var strData = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                ListProduct = JsonSerializer.Deserialize<List<Product>>(strData, options) ?? new List<Product>();
            }

            return Page();
        }

        public async Task<JsonResult> OnGetSearchAsync(string? keyword, string? price)
        {
            string url = ProductApiUrl;
            if (!string.IsNullOrEmpty(keyword) || !string.IsNullOrEmpty(price))
            {
                url += $"?keyword={keyword}&unitP={price}";
            }

            HttpResponseMessage response = await client.GetAsync(url);
            if (!response.IsSuccessStatusCode)
            {
                return new JsonResult(new { error = "Failed to fetch data" }) { StatusCode = 500 };
            }

            var strData = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var products = JsonSerializer.Deserialize<List<Product>>(strData, options) ?? new List<Product>();

            return new JsonResult(products);
        }
    }
}
