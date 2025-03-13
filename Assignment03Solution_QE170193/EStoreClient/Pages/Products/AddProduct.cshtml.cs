using BusinessObject.Models;
using EStoreClient.Dto.ProductDto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Text;

namespace EStoreClient.Pages.Products
{
    public class AddProductModel : PageModel
    {
        private readonly HttpClient client = null;
        private readonly IConfiguration _configuration;
        private string AddProductApiUrl = "";
        private string ProductCateApiUrl = "";

        [BindProperty]
        public ProductAddRequest ProductAddRequest { get; set; }
        public List<Category> listCategory { get; set; }
        public AddProductModel(IConfiguration configuration)
        {
            _configuration = configuration;
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            AddProductApiUrl = _configuration.GetValue<string>("DomainURL") + "Product/AddProduct";
            ProductCateApiUrl = _configuration.GetValue<string>("DomainURL") + "Category/GetAllCategory";
        }

        public async Task<IActionResult> OnGetAsync()
        {
            HttpResponseMessage resp = await client.GetAsync(ProductCateApiUrl);

            var strData = await resp.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            listCategory = System.Text.Json.JsonSerializer.Deserialize<List<Category>>(strData, options);
            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            var request = new
            {
                ProductName = ProductAddRequest.ProductName,
                CategoryId = ProductAddRequest.CategoryId,
                Weight = ProductAddRequest.Weight,
                UnitPrice = ProductAddRequest.UnitPrice,
                UnitsInStock = ProductAddRequest.UnitsInStock
            };

            string body = JsonConvert.SerializeObject(request);
            HttpContent httpContent = new StringContent(body, Encoding.UTF8, "application/json");

            // Make the POST request using HttpClient with the request body
            HttpResponseMessage response = await client.PostAsync(AddProductApiUrl, httpContent);

            // Read the response from the API
            string responseContent = await response.Content.ReadAsStringAsync();

            return RedirectToPage("/Products/Product");
        }
    }
}
