using BusinessObject.Models;
using DataAccess.Dto.OrderDto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

namespace EStoreClient.Pages.Orders
{
    public class AddOrderModel : PageModel
    {
        private readonly HttpClient _client;
        private readonly IConfiguration _configuration;
        private string _productApiUrl = "";

        public AddOrderModel(IConfiguration configuration)
        {
            _configuration = configuration;
            _client = new HttpClient();
            _productApiUrl = _configuration.GetValue<string>("DomainURL") + "Product/GetAllProduct";
        }

        public List<Product> Products { get; set; } = new List<Product>();
        public string Role { get; set; }

        [BindProperty]
        public OrderRequestDto OrderRequest { get; set; } = new OrderRequestDto
        {
            OrderItems = new List<OrderItemRequest>()
        };

        private const string SessionKey = "OrderSession";

        public async Task OnGetAsync()
        {
            await LoadProductsAsync();
            LoadOrderFromSession();
            Role = HttpContext.Session.GetString("ROLE");
        }

        private async Task LoadProductsAsync()
        {
            HttpResponseMessage resp = await _client.GetAsync(_productApiUrl);
            if (resp.IsSuccessStatusCode)
            {
                var strData = await resp.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                Products = JsonSerializer.Deserialize<List<Product>>(strData, options) ?? new List<Product>();
            }
        }

        private void LoadOrderFromSession()
        {
            var sessionData = HttpContext.Session.GetString(SessionKey);
            if (!string.IsNullOrEmpty(sessionData))
            {
                OrderRequest = JsonSerializer.Deserialize<OrderRequestDto>(sessionData) ?? new OrderRequestDto { OrderItems = new List<OrderItemRequest>() };
            }
        }

        public async Task<JsonResult> OnPostAddOrderItemAsync(int productId, int quantity)
        {
            await LoadProductsAsync();
            LoadOrderFromSession();

            var product = Products.FirstOrDefault(p => p.ProductId == productId);
            if (product != null)
            {
                if (quantity <= 0)
                {
                    return new JsonResult(new { success = false, message = "Quantity must be greater than 0." });
                }

                if (quantity > product.UnitsInStock)
                {
                    return new JsonResult(new { success = false, message = "Quantity must be less than or equal to Units In Stock." });
                }

                var existingItem = OrderRequest.OrderItems.FirstOrDefault(o => o.ProductId == productId);
                if (existingItem == null)
                {
                    OrderRequest.OrderItems.Add(new OrderItemRequest
                    {
                        ProductId = productId,
                        ProductName = product.ProductName!,
                        Quantity = quantity,
                        UnitPrice = product.UnitPrice,
                        Discount = 0
                    });
                }
                else
                {
                    existingItem.Quantity += quantity;
                }
            }
            else
            {
                return new JsonResult(new { success = false, message = "Product not found." });
            }

            SaveOrderToSession();
            return new JsonResult(new { success = true, orderItems = OrderRequest.OrderItems });
        }

        private void SaveOrderToSession()
        {
            HttpContext.Session.SetString(SessionKey, JsonSerializer.Serialize(OrderRequest));
        }

        public JsonResult OnPostRemoveOrderItem(int productId)
        {
            LoadOrderFromSession();

            var item = OrderRequest.OrderItems.FirstOrDefault(o => o.ProductId == productId);
            if (item != null)
            {
                OrderRequest.OrderItems.Remove(item);
            }

            SaveOrderToSession();
            return new JsonResult(new { success = true, orderItems = OrderRequest.OrderItems });
        }

        public async Task<IActionResult> OnPostCreateOrderAsync()
        {
            var freight = OrderRequest.Freight;

            LoadOrderFromSession();

            if (OrderRequest.OrderItems.Count == 0)
            {
                TempData["ErrorMessage"] = "Please add at least one product to create order.";
                return RedirectToPage();
            }

            var memberId = Guid.Parse(HttpContext.Session.GetString("MemberId") ?? Guid.Empty.ToString());

            if (memberId == Guid.Empty)
            {
                TempData["ErrorMessage"] = "Please login to create order.";
                return RedirectToPage("/Login");
            }

            OrderRequest.MemberId = memberId;
            OrderRequest.OrderDate = DateTime.Now;
            OrderRequest.RequireDate = DateTime.Now.AddDays(7);
            OrderRequest.ShippedDate = DateTime.Now.AddDays(3);
            OrderRequest.Freight = freight;

            var content = new StringContent(JsonSerializer.Serialize(OrderRequest), System.Text.Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _client.PostAsync(_configuration.GetValue<string>("DomainURL") + "Order/AddOrder", content);

            if (response.IsSuccessStatusCode)
            {
                HttpContext.Session.Remove(SessionKey);
                return RedirectToPage("/Orders/Order");
            }
            else
            {
                TempData["ErrorMessage"] = "Failed to create order. Please try again.";
                return RedirectToPage();
            }
        }
    }
}
