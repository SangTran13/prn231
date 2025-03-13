using BusinessObject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;

namespace EStoreClient.Pages.Orders
{
    public class OrderDetailModel : PageModel
    {
        private readonly HttpClient _client;
        private readonly IConfiguration _configuration;
        private readonly string _orderDetailApiUrl;
        private readonly string _orderApiUrl;
        private readonly string _memberApiUrl;

        public List<OrderDetail> ListOrderDetail { get; set; }
        public Order Order { get; set; }
        public Member Member { get; set; }

        public OrderDetailModel(IConfiguration configuration)
        {
            _configuration = configuration;
            _client = new HttpClient();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            string baseUrl = _configuration.GetValue<string>("DomainURL");
            _orderDetailApiUrl = $"{baseUrl}OrderDetail/GetOrderDetailByOrderId";
            _orderApiUrl = $"{baseUrl}Order/Detail";
            _memberApiUrl = $"{baseUrl}Member/Detail";
        }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            try
            {
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

                // Lấy danh sách OrderDetail
                HttpResponseMessage orderDetailResp = await _client.GetAsync($"{_orderDetailApiUrl}/{id}");
                if (!orderDetailResp.IsSuccessStatusCode)
                {
                    ModelState.AddModelError(string.Empty, "Không thể tải thông tin chi tiết đơn hàng.");
                    return Page();
                }

                var orderDetailData = await orderDetailResp.Content.ReadAsStringAsync();
                ListOrderDetail = JsonSerializer.Deserialize<List<OrderDetail>>(orderDetailData, options);

                if (ListOrderDetail == null || ListOrderDetail.Count == 0)
                {
                    ModelState.AddModelError(string.Empty, "Không có chi tiết đơn hàng nào.");
                    return Page();
                }

                // Lấy thông tin đơn hàng từ OrderDetail đầu tiên
                int orderId = ListOrderDetail.First().OrderId;
                HttpResponseMessage orderResp = await _client.GetAsync($"{_orderApiUrl}/{orderId}");
                if (!orderResp.IsSuccessStatusCode)
                {
                    ModelState.AddModelError(string.Empty, "Không thể tải thông tin đơn hàng.");
                    return Page();
                }

                var orderData = await orderResp.Content.ReadAsStringAsync();
                Order = JsonSerializer.Deserialize<Order>(orderData, options);

                // Lấy thông tin Member nếu có MemberId
                if (!string.IsNullOrEmpty(Order?.MemberId))
                {
                    HttpResponseMessage memberResp = await _client.GetAsync($"{_memberApiUrl}/{Order.MemberId}");
                    if (memberResp.IsSuccessStatusCode)
                    {
                        var memberData = await memberResp.Content.ReadAsStringAsync();
                        Member = JsonSerializer.Deserialize<Member>(memberData, options);
                    }
                }

                return Page();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi khi gọi API: {ex.Message}");
                ModelState.AddModelError(string.Empty, "Có lỗi xảy ra khi tải dữ liệu.");
                return Page();
            }
        }
    }
}
