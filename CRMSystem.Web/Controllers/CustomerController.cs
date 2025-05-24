using CRMSystem.Web.Helpers;
using CRMSystem.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace CRMSystem.Web.Controllers
{
    public class CustomerController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public CustomerController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index()
        {
            var token = HttpContext.Request.Cookies["jwt"];
            
            if (string.IsNullOrEmpty(token))
            {
                return RedirectToAction("Index", "Login");
            }
            
            var role  = JwtHelper.GetRoleFromToken(token);

            if(role!="Admin")
                return RedirectToAction("AccessDenied", "Login");

            //adminse devam

            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await client.GetAsync("https://localhost:7242/api/Customer");

            if (response.StatusCode==System.Net.HttpStatusCode.Unauthorized)
            {
                //token geçersizse login sayfasına yönlendir
                HttpContext.Response.Cookies.Delete("jwt");
                return RedirectToAction("Index", "Login");
            }
            if (!response.IsSuccessStatusCode)
            {
                ViewBag.Error = "Veriler alınırken hata oluştu.";
                return View();
            }

            var jsonData = await response.Content.ReadAsStringAsync();
            var customers = JsonConvert.DeserializeObject<List<CustomerDto>>(jsonData);

            return View(customers);
        }


        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CustomerDto customer)
        {
            var token = HttpContext.Request.Cookies["jwt"];
            if (string.IsNullOrEmpty(token))
            {
                return RedirectToAction("Index", "Login");
            }

            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var json = JsonConvert.SerializeObject(customer);
            var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

            var response = await client.PostAsync("https://localhost:7242/api/Customer", content);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            ViewBag.Error = "Müşteri eklenemedi.";
            return View(customer);
        }




        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var token = HttpContext.Request.Cookies["jwt"];
            if (string.IsNullOrEmpty(token))
                return RedirectToAction("Index", "Login");

            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await client.GetAsync($"https://localhost:7242/api/Customer/{id}");

            if (!response.IsSuccessStatusCode)
            {
                ViewBag.Error = "Müşteri verisi alınamadı.";
                return RedirectToAction("Index");
            }

            var jsonData = await response.Content.ReadAsStringAsync();
            var customer = JsonConvert.DeserializeObject<CustomerDto>(jsonData);

            return View(customer);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(CustomerDto customer)
        {
            var token = HttpContext.Request.Cookies["jwt"];
            if (string.IsNullOrEmpty(token))
                return RedirectToAction("Index", "Login");

            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var json = JsonConvert.SerializeObject(customer);
            var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

            var response = await client.PutAsync($"https://localhost:7242/api/Customer/{customer.Id}", content);

            if (response.IsSuccessStatusCode)
                return RedirectToAction("Index");

            ViewBag.Error = "Güncelleme başarısız.";
            return View(customer);
        }


        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var token = HttpContext.Request.Cookies["jwt"];
            if (string.IsNullOrEmpty(token))
                return RedirectToAction("Index", "Login");

            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await client.DeleteAsync($"https://localhost:7242/api/Customer/{id}");

            return RedirectToAction("Index");
        }


    }
}
