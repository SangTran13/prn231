using BusinessObject.Models;
using EStoreClient.Utility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http.Headers;
using System.Text.Json;

namespace EStoreClient.Pages.Orders
{
    public class OrderModel : PageModel
    {
        private readonly HttpClient client = null;
        private readonly IConfiguration _configuration;
        private string OrderApiUrl = "";
        public List<Order> ListOrder { get; set; }

        [BindProperty]
        public string? Keyword { get; set; }

        public OrderModel(IConfiguration configuration)
        {
            _configuration = configuration;
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            OrderApiUrl = _configuration.GetValue<string>("DomainURL") + "Order/GetAllOrder";
        }
        public async Task<IActionResult> OnGetAsync(string? keyword)
        {
            var currentRole = HttpContext.Session.GetString("Role");
            var currentMemberId = HttpContext.Session.GetString("MemberId");

            if (currentRole == null || currentMemberId == null)
            {
                return RedirectToPage("/Index");
            }

            Keyword = keyword;
            string url = string.Empty;

            if (RoleConstant.ADMIN.Equals(currentRole))
            {
                url = OrderApiUrl;
            }
            else
            {
                url = _configuration.GetValue<string>("DomainURL") + "Order/GetOrderByMemberId/" + currentMemberId;
            }

            if (!string.IsNullOrEmpty(keyword))
            {
                url += "?keyword=" + keyword;
            }

            HttpResponseMessage resp = await client.GetAsync(url);

            var strData = await resp.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            List<Order> listOrders = JsonSerializer.Deserialize<List<Order>>(strData, options);
            ListOrder = listOrders;
            return Page();
        }
    }
}
