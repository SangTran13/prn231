using BusinessObject;
using DataTransfer;
using eStoreClient.Untils;
using Microsoft.AspNetCore.Mvc;
using Shared;
using System.Net.Http.Headers;

namespace eStoreClient.Controllers
{
    public class ProductController : Controller
    {
        private readonly HttpClient client = null;

        public ProductController()
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            string Role = HttpContext.Session.GetString("ROLE");

            if (Role == null)
            {
                TempData["ErrorMessage"] = "You must login to access this page.";
                return RedirectToAction("Index", "Home");
            }
            else if (Role != "Admin")
            {
                TempData["ErrorMessage"] = "You don't have permission to access this page.";
                return RedirectToAction("Profile", "Member");
            }

            var apiResponse = await ApiHandler.DeserializeApiResponse<List<Product>>("https://localhost:7237/api/products", HttpMethod.Get);

            var listProducts = apiResponse.Data;

            if (TempData != null)
            {
                ViewData["SuccessMessage"] = TempData["SuccessMessage"];
                ViewData["ErrorMessage"] = TempData["ErrorMessage"];
            }

            return View(listProducts);
        }

        public async Task<IActionResult> Search(string keyword)
        {
            var apiResponse = await ApiHandler.DeserializeApiResponse<List<Product>>("https://localhost:7237/api/products/search/" + keyword, HttpMethod.Get);
            var listProducts = apiResponse.Data;

            ViewData["keyword"] = keyword;

            return View("Index", listProducts);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            string Role = HttpContext.Session.GetString("ROLE");

            if (Role == null)
            {
                TempData["ErrorMessage"] = "You must login to access this page.";
                return RedirectToAction("Index", "Home");
            }
            else if (Role != "Admin")
            {
                TempData["ErrorMessage"] = "You don't have permission to access this page.";
                return RedirectToAction("Profile", "Member");
            }

            var apiResponse1 = await ApiHandler.DeserializeApiResponse<List<Category>>("https://localhost:7237/api/categories", HttpMethod.Get);
            var apiResponse2 = await ApiHandler.DeserializeApiResponse<List<Supplier>>("https://localhost:7237/api/suppliers", HttpMethod.Get);

            var listCategories = apiResponse1.Data;
            var listSuppliers = apiResponse2.Data;

            ViewData["Categories"] = listCategories;
            ViewData["Suppliers"] = listSuppliers;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductRequest productRequest)
        {
            var response = await ApiHandler.DeserializeApiResponse<int>("https://localhost:7237/api/products", HttpMethod.Post, productRequest);

            if (response.StatusCode == 1000)
            {
                TempData["SuccessMessage"] = "Creation new product successfully.";
                return RedirectToAction("Index");
            }
            else
            {
                ViewData["ErrorMessage"] = "An error occurred during creation.";
                return View("Create", productRequest);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            string Role = HttpContext.Session.GetString("ROLE");

            if (Role == null)
            {
                TempData["ErrorMessage"] = "You must login to access this page.";
                return RedirectToAction("Index", "Home");
            }
            else if (Role != "Admin")
            {
                TempData["ErrorMessage"] = "You don't have permission to access this page.";
                return RedirectToAction("Profile", "Member");
            }

            var apiResponse = await ApiHandler.DeserializeApiResponse<Product>("https://localhost:7237/api/products/" + id, HttpMethod.Get);
            var product = apiResponse.Data;
            if (product == null)
            {
                TempData["ErrorMessage"] = "Product not found";
                return RedirectToAction("Index");
            }

            var apiResponse1 = await ApiHandler.DeserializeApiResponse<List<Category>>("https://localhost:7237/api/categories", HttpMethod.Get);
            var apiResponse2 = await ApiHandler.DeserializeApiResponse<List<Supplier>>("https://localhost:7237/api/suppliers", HttpMethod.Get);

            var listCategories = apiResponse1.Data;
            var listSuppliers = apiResponse2.Data;

            ViewData["Categories"] = listCategories;
            ViewData["Suppliers"] = listSuppliers;

            if (TempData != null)
            {
                ViewData["SuccessMessage"] = TempData["SuccessMessage"];
                ViewData["ErrorMessage"] = TempData["ErrorMessage"];
            }

            return View(product);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ProductRequest productRequest)
        {
            var apiResponse = await ApiHandler.DeserializeApiResponse<ApiResponse<object>>(
                $"https://localhost:7237/api/products/{productRequest.ProductId}",
                HttpMethod.Put,
                productRequest);

            if (apiResponse.StatusCode == 1000)
            {
                TempData["SuccessMessage"] = "Product updated successfully!!";
                return RedirectToAction("Index");
            }
            else
            {
                ViewData["ErrorMessage"] = TempData["ErrorMessage"];
                return RedirectToAction("Edit", new { id = productRequest.ProductId });
            }
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            string Role = HttpContext.Session.GetString("ROLE");

            if (Role == null)
            {
                TempData["ErrorMessage"] = "You must login to access this page.";
                return RedirectToAction("Index", "Home");
            }
            else if (Role != "Admin")
            {
                TempData["ErrorMessage"] = "You don't have permission to access this page.";
                return RedirectToAction("Profile", "Member");
            }


            var apiResponse = await ApiHandler.DeserializeApiResponse<Product>("https://localhost:7237/api/products/" + id, HttpMethod.Get);
            var product = apiResponse.Data;
            if (product == null)
            {
                TempData["ErrorMessage"] = "Product not found";
                return RedirectToAction("Index");
            }

            return View(product);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Product product)
        {
            _ = await ApiHandler.DeserializeApiResponse<Product>("https://localhost:7237/api/products/" + product.ProductId, HttpMethod.Delete);
            TempData["SuccessMessage"] = "Product deleted successfully";
            return RedirectToAction("Index");
        }

    }
}
