using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http.Headers;

namespace EStoreClient.Pages.Orders
{
    public class DeleteOrderModel : PageModel
    {
        private readonly HttpClient client = null;
        private readonly IConfiguration _configuration;
        private string OrderDeleteApiUrl = "";
        public DeleteOrderModel(IConfiguration configuration)
        {
            _configuration = configuration;
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            OrderDeleteApiUrl = _configuration.GetValue<string>("DomainURL") + "Order/Delete/{id}";
        }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            string requestUrl = OrderDeleteApiUrl.Replace("{id}", id);
            HttpResponseMessage resp = await client.DeleteAsync(requestUrl);
            return RedirectToPage("Order");
        }
    }
}
