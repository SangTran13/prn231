using BusinessObject;
using DataTransfer;
using eStoreClient.Untils;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace eStoreClient.Controllers
{
    public class OrderController : Controller
    {
        private readonly HttpClient client = null;

        public OrderController()
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            int? userId = HttpContext.Session.GetInt32("USERID");
            string role = HttpContext.Session.GetString("ROLE");
            if (userId == null)
            {
                TempData["ErrorMessage"] = "You must login to view your order history.";
                return RedirectToAction("Index", "Home", TempData);
            }
            else if (role != "Admin")
            {
                TempData["ErrorMessage"] = "You must login as a admin to view orders.";
                return RedirectToAction("Profile", "Member", TempData);
            }

            var apiResponse = await ApiHandler.DeserializeApiResponse<List<Order>>("https://localhost:7237/api/orders", HttpMethod.Get);
            var listOrders = apiResponse.Data;
            if (TempData != null)
            {
                ViewData["SuccessMessage"] = TempData["SuccessMessage"];
                ViewData["ErrorMessage"] = TempData["ErrorMessage"];
            }

            return View(listOrders);
        }

        [HttpGet]
        public async Task<IActionResult> OrderHistory()
        {
            int? userId = HttpContext.Session.GetInt32("USERID");
            string role = HttpContext.Session.GetString("ROLE");
            if (userId == null)
            {
                TempData["ErrorMessage"] = "You must login to view your order history.";
                return RedirectToAction("Index", "Home", TempData);
            }
            else if (role != "Member")
            {
                TempData["ErrorMessage"] = "You must login as a customer to view your order history.";
                return RedirectToAction("Index", "Member", TempData);
            }

            var apiResponse = await ApiHandler.DeserializeApiResponse<List<Order>>("https://localhost:7237/api/orders/member/" + userId.Value, HttpMethod.Get);

            var listOrders = apiResponse.Data;

            if (TempData != null)
            {
                ViewData["SuccessMessage"] = TempData["SuccessMessage"];
                ViewData["ErrorMessage"] = TempData["ErrorMessage"];
            }

            return View("Index", listOrders);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            int? userId = HttpContext.Session.GetInt32("USERID");
            string role = HttpContext.Session.GetString("ROLE");
            if (userId == null)
            {
                TempData["ErrorMessage"] = "You must login to view order details.";
                return RedirectToAction("Index", "Home", TempData);
            }

            var apiResponse = await ApiHandler.DeserializeApiResponse<Order>("https://localhost:7237/api/orders/" + id, HttpMethod.Get);
            var order = apiResponse.Data;
            if (order == null)
            {
                TempData["ErrorMessage"] = "Order not found.";

                if (role == "Admin")
                    return RedirectToAction("Index", "Order", TempData);
                else
                    return RedirectToAction("OrderHistory", "Order", TempData);
            }
            if (role == "Member" && order.MemberId != userId.Value)
            {
                TempData["ErrorMessage"] = "You don't have permission to view this order.";
                return RedirectToAction("OrderHistory", "Order", TempData);
            }

            var apiResponse1 = await ApiHandler.DeserializeApiResponse<List<OrderDetail>>("https://localhost:7237/api/orderdetails/order/" + id, HttpMethod.Get);
            var listOrderDetails = apiResponse1.Data;

            if (TempData != null)
            {
                ViewData["SuccessMessage"] = TempData["SuccessMessage"];
                ViewData["ErrorMessage"] = TempData["ErrorMessage"];
            }

            ViewData["Order"] = order;
            ViewData["OrderDetails"] = listOrderDetails;

            return View("OrderDetail");
        }

        public async Task<IActionResult> Report(string startDate, string endDate)
        {
            var apiResponse = await ApiHandler.DeserializeApiResponse<List<Order>>("https://localhost:7237/api/orders", HttpMethod.Get);

            var listOrders = apiResponse.Data;

            if (startDate != null && endDate == null)
            {
                DateTime start = DateTime.Parse(startDate);
                listOrders = listOrders.Where(o => o.OrderStatus == 1 && o.OrderDate >= start).ToList();
            }
            else if (startDate == null && endDate != null)
            {
                DateTime end = DateTime.Parse(endDate);
                listOrders = listOrders.Where(o => o.OrderStatus == 1 && o.OrderDate <= end).ToList();
            }
            else if (startDate != null && endDate != null)
            {
                DateTime start = DateTime.Parse(startDate);
                DateTime end = DateTime.Parse(endDate);

                if (start > end)
                {
                    TempData["ErrorMessage"] = "Start date must be before end date.";
                    return RedirectToAction("Index", "Order", TempData);
                }

                listOrders = listOrders.Where(o => o.OrderStatus == 1 && o.OrderDate >= start && o.OrderDate <= end).OrderByDescending(o => o.Total).ToList();
            }
            else
            {
                TempData["ErrorMessage"] = "Please select a date range.";
                return RedirectToAction("Index", "Order", TempData);
            }

            if (TempData != null)
            {
                ViewData["SuccessMessage"] = TempData["SuccessMessage"];
                ViewData["ErrorMessage"] = TempData["ErrorMessage"];
            }

            ViewData["StartDate"] = startDate;
            ViewData["EndDate"] = endDate;
            ViewData["Orders"] = listOrders;

            return View();
        }

        public async Task<IActionResult> Shipped(int id)
        {
            _ = await ApiHandler.DeserializeApiResponse<object>("https://localhost:7237/api/orders/shipped/" + id, HttpMethod.Put, "");

            TempData["SuccessMessage"] = "Order shipped successfully.";
            return RedirectToAction("Index", "Order", TempData);
        }

        public async Task<IActionResult> Cancel(int id)
        {
            _ = await ApiHandler.DeserializeApiResponse<object>("https://localhost:7237/api/orders/cancel/" + id, HttpMethod.Put, "");

            TempData["SuccessMessage"] = "Order canceled successfully.";
            return RedirectToAction("Index", "Order", TempData);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            int? userId = HttpContext.Session.GetInt32("USERID");
            if (userId == null)
            {
                TempData["ErrorMessage"] = "You must login to create order.";
                return RedirectToAction("Index", "Home", TempData);
            }

            var apiResponse = await ApiHandler.DeserializeApiResponse<List<Member>>("https://localhost:7237/api/members", HttpMethod.Get);
            var listMembers = apiResponse.Data;
            var apiResponse2 = await ApiHandler.DeserializeApiResponse<List<Product>>("https://localhost:7237/api/products", HttpMethod.Get);
            var listProducts = apiResponse2.Data;
            listProducts = listProducts.Where(fb => fb.ProductStatus == 1).ToList();

            if (TempData != null)
            {
                ViewData["SuccessMessage"] = TempData["SuccessMessage"];
                ViewData["ErrorMessage"] = TempData["ErrorMessage"];
            }

            ViewData["OrderItems"] = GetOrderItems();
            ViewData["Members"] = listMembers;
            ViewData["Products"] = listProducts;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(OrderRequest orderRequest)
        {
            List<OrderItemRequest> listItemsRequest = GetOrderItems();
            if (listItemsRequest.Count == 0)
            {
                TempData["ErrorMessage"] = "Order items are empty.";
                return RedirectToAction("Create", TempData);
            }

            string role = HttpContext.Session.GetString("ROLE");
            int memberId = orderRequest.MemberId;
            if (role == "Member")
            {
                memberId = HttpContext.Session.GetInt32("USERID").Value;
            }

            decimal total = listItemsRequest.Sum(p => p.Product.UnitPrice * p.Quantity);
            decimal discount = orderRequest.Discount;

            if (orderRequest.Discount >= 0 && orderRequest.Discount <= 100)
            {
                discount = total * ((orderRequest.Discount) / 100);
            }
            else
            {
                TempData["ErrorMessage"] = "Discount must be between 0 and 100 percent.";
                return RedirectToAction("Create", TempData);
            }



            Order order = new Order()
            {
                MemberId = memberId,
                OrderDate = DateTime.Now,
                OrderStatus = 0,
                Freight = orderRequest.Freight,
                Total = total - discount
            };

            var response = await ApiHandler.DeserializeApiResponse<int>("https://localhost:7237/api/orders", HttpMethod.Post, order);
            var orderSaved = response.Data;

            foreach (OrderItemRequest itemRequest in listItemsRequest)
            {
                OrderDetail orderDetail = new OrderDetail()
                {
                    OrderId = orderSaved,
                    ProductId = itemRequest.Product.ProductId,
                    Quantity = itemRequest.Quantity,
                    UnitPrice = itemRequest.Product.UnitPrice,
                    Discount = orderRequest.Discount
                };
                await client.PostAsJsonAsync("https://localhost:7237/api/orderdetails", orderDetail);
            }

            ClearOrderItemsSession();

            if (role == "Member")
            {
                TempData["SuccessMessage"] = "Create order successfully.";
                return RedirectToAction("OrderHistory", TempData);
            }
            else
            {
                TempData["SuccessMessage"] = "Create order successfully.";
                return RedirectToAction("Index", TempData);
            }
        }

        public async Task<IActionResult> AddOrderItem(OrderRequest orderRequest)
        {
            var apiResponse2 = await ApiHandler.DeserializeApiResponse<List<Product>>("https://localhost:7237/api/products", HttpMethod.Get);
            var listProducts = apiResponse2.Data;
            Product product = listProducts.Where(p => p.ProductId == orderRequest.ProductId).FirstOrDefault();
            if (product == null)
            {
                TempData["ErrorMessage"] = "Product doesn't exist.";
                return RedirectToAction("Create", TempData);
            }

            List<OrderItemRequest> listItemsRequest = GetOrderItems();
            OrderItemRequest itemRequest = listItemsRequest.Find(p => p.Product.ProductId == orderRequest.ProductId);
            if (itemRequest != null)
            {
                if (itemRequest.Quantity + orderRequest.Quantity > product.UnitsInStock)
                {
                    TempData["ErrorMessage"] = "Quantity exceeds the number of products in stock.";
                    return RedirectToAction("Create", TempData);
                }
                itemRequest.Quantity += orderRequest.Quantity;
            }
            else
            {
                if (orderRequest.Quantity > product.UnitsInStock)
                {
                    TempData["ErrorMessage"] = "Quantity exceeds the number of products in stock.";
                    return RedirectToAction("Create", TempData);
                }
                listItemsRequest.Add(new OrderItemRequest() { Quantity = orderRequest.Quantity, Product = product });
            }
            TempData["SelectedProductId"] = orderRequest.ProductId;

            SaveOrderItemsSession(listItemsRequest);
            return RedirectToAction("Create");
        }


        public async Task<IActionResult> RemoveOrderItem(OrderRequest orderRequest)
        {
            List<OrderItemRequest> listItemsRequest = GetOrderItems();
            listItemsRequest.RemoveAll(p => p.Product.ProductId == orderRequest.ProductId);
            SaveOrderItemsSession(listItemsRequest);
            return RedirectToAction("Create");
        }

        private List<OrderItemRequest> GetOrderItems()
        {
            var session = HttpContext.Session;
            string jsonOrderItems = session.GetString("ORDER_ITEMS");
            if (jsonOrderItems != null)
            {
                return JsonConvert.DeserializeObject<List<OrderItemRequest>>(jsonOrderItems);
            }
            return new List<OrderItemRequest>();
        }

        private void SaveOrderItemsSession(List<OrderItemRequest> ls)
        {
            var session = HttpContext.Session;
            string jsoncart = JsonConvert.SerializeObject(ls);
            session.SetString("ORDER_ITEMS", jsoncart);
        }

        private void ClearOrderItemsSession()
        {
            var session = HttpContext.Session;
            session.Remove("ORDER_ITEMS");
        }

        public async Task<IActionResult> SubtractOrderItem(OrderRequest orderRequest)
        {
            List<OrderItemRequest> listItemsRequest = GetOrderItems();
            var itemRequest = listItemsRequest.FirstOrDefault(p => p.Product.ProductId == orderRequest.ProductId);

            if (itemRequest != null)
            {
                if (itemRequest.Quantity > 1)
                {
                    itemRequest.Quantity -= 1;
                }
                else
                {
                    listItemsRequest.Remove(itemRequest);
                }
            }

            TempData["SelectedProductId"] = orderRequest.ProductId;
            SaveOrderItemsSession(listItemsRequest);
            return RedirectToAction("Create");
        }




    }
}
