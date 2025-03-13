using BusinessObject.Models;
using EStoreClient.Utility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http.Headers;
using System.Text.Json;

namespace EStoreClient.Pages.Orders
{
    public class ReportOrderModel : PageModel
    {
        private readonly HttpClient client = null;
        private readonly IConfiguration _configuration;
        private string ReportOrderApiUrl = "";
        public List<Order> ListReportOrder { get; set; }

        [BindProperty]
        public string? FromDate { get; set; }
        [BindProperty]
        public string? ToDate { get; set; }

        public ReportOrderModel(IConfiguration configuration)
        {
            _configuration = configuration;
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            ReportOrderApiUrl = _configuration.GetValue<string>("DomainURL") + "OrderDetail/Report";
        }
        public async Task<IActionResult> OnGetAsync(string? fromDate, string? toDate)
        {
            var currentRole = HttpContext.Session.GetString("Role");

            if (currentRole == null || currentRole != RoleConstant.ADMIN)
            {
                return RedirectToPage("/Index");
            }

            if (fromDate == null && toDate != null)
            {
                TempData["ErrorMessage"] = "Please select From Date";
                return RedirectToPage("/Orders/ReportOrder");
            }
            else if (fromDate != null && toDate != null)
            {
                if (DateTime.Parse(fromDate) > DateTime.Parse(toDate))
                {
                    TempData["ErrorMessage"] = "From Date must be less than To Date";
                    return RedirectToPage("/Orders/ReportOrder");
                }
            }

            FromDate = fromDate;
            ToDate = toDate;


            string url = ReportOrderApiUrl + "?fromDate=" + fromDate + "&toDate=" + toDate;
            if (fromDate == null && toDate == null)
            {
                url = ReportOrderApiUrl;
            }

            HttpResponseMessage resp = await client.GetAsync(url);

            var strData = await resp.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            List<Order> listReportOrders = JsonSerializer.Deserialize<List<Order>>(strData, options);
            ListReportOrder = listReportOrders;
            return Page();
        }

        public async Task<IActionResult> OnGetSearchAsync(string? fromDate, string? toDate)
        {
            if (string.IsNullOrEmpty(fromDate) || string.IsNullOrEmpty(toDate))
            {
                return new JsonResult(new { error = "Invalid date range" });
            }

            if (DateTime.Parse(fromDate) > DateTime.Parse(toDate))
            {
                return new JsonResult(new { error = "From Date must be less than To Date" });
            }

            string url = $"{ReportOrderApiUrl}?fromDate={fromDate}&toDate={toDate}";

            HttpResponseMessage resp = await client.GetAsync(url);
            var strData = await resp.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var listReportOrders = JsonSerializer.Deserialize<List<Order>>(strData, options);

            return new JsonResult(listReportOrders);
        }
    }
}
