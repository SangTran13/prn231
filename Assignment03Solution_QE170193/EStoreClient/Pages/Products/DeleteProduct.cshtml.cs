using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http.Headers;

namespace EStoreClient.Pages.Products
{
    public class DeleteProductModel : PageModel
    {
        private readonly HttpClient client = null;
        private readonly IConfiguration _configuration;
        private string ProductDeleteApiUrl = "";
        public DeleteProductModel(IConfiguration configuration)
        {
            _configuration = configuration;
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            ProductDeleteApiUrl = _configuration.GetValue<string>("DomainURL") + "Product/Delete/{id}";
        }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            string requestUrl = ProductDeleteApiUrl.Replace("{id}", id);
            HttpResponseMessage resp = await client.DeleteAsync(requestUrl);
            return RedirectToPage("Product");
        }
    }
}
