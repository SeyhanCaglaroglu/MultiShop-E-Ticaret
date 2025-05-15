using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using MultiShop.DtoLayer.IdentityDtos.LoginDtos;
using MultiShop.DtoLayer.IdentityDtos.RegisterDtos;
using MultiShop.WebUI.Models;
using MultiShop.WebUI.Services;
using MultiShop.WebUI.Services.CatalogServices.CategoryServices;
using MultiShop.WebUI.Services.Interfaces;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;


namespace MultiShop.WebUI.Controllers
{
    public class RegisterController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IIdentityService _identityService;
        private readonly ICategoryService _categoryService;
        public RegisterController(IHttpClientFactory httpClientFactory, IIdentityService identityService, ICategoryService categoryService)
        {
            _httpClientFactory = httpClientFactory;
            _identityService = identityService;
            _categoryService = categoryService;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(CreateRegisterDto createRegisterDto)
        {
            if(createRegisterDto.ConfirmPassword == createRegisterDto.Password)
            {
                // HttpClient oluştur
                var client = _httpClientFactory.CreateClient();

                // DTO'yu JSON'a çevir
                var jsonData = JsonConvert.SerializeObject(createRegisterDto);
                Console.WriteLine($"Gönderilecek JSON Verisi: {jsonData}");

                // JSON verisini gönderime hazır hale getir
                StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");

                // API'ye POST isteği gönder
                var responseMessage = await client.PostAsync("http://localhost:5001/api/Registers", stringContent);

                // Yanıtın başarılı olup olmadığını kontrol et
                if (responseMessage.IsSuccessStatusCode)
                {
                    Console.WriteLine("API isteği başarılı. Yanıt kodu: 200");

                    // API'den dönen veriyi logla
                    var responseContent = await responseMessage.Content.ReadAsStringAsync();
                    Console.WriteLine($"API Yanıt İçeriği: {responseContent}");

                    // Başarılıysa yönlendir
                    return Redirect("/Register/Index");
                }
                else
                {
                    // Yanıt başarılı değilse hata mesajını logla
                    Console.WriteLine($"API isteği başarısız. Yanıt kodu: {responseMessage.StatusCode}");
                    return View();
                }
                

            }
            return View();

        }
        [HttpPost]
        public async Task<IActionResult> Login(SignInDto signInDto)
        {
           bool status =  await _identityService.SignIn(signInDto);

            if (status == false)
            {
                TempData["errorMessage"] = "Kullanıcı Adı veya Şifre Hatalı !";
                return RedirectToAction("Index", "Register");
            }

            return RedirectToAction("Index", "Default");
        } 

        
    }
}
