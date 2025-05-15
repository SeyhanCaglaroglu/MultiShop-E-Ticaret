using Microsoft.AspNetCore.Mvc;
using MultiShop.DtoLayer.CatalogDtos.AboutDtos;
using MultiShop.WebUI.Services.CatalogServices.AboutServices;
using Newtonsoft.Json;
using System.Text;

namespace MultiShop.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AboutController : Controller
    {
        private readonly IAboutService _aboutService;

        public AboutController(IAboutService AboutService)
        {
            _aboutService = AboutService;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.v1 = "Anasayfa";
            ViewBag.v2 = "Hakkımızda";
            ViewBag.v3 = "Hakkımızda Listesi";
            ViewBag.baslik = "Hakkımızda İşlemleri";

            var values = await _aboutService.GetAllAboutsAsync();
            return View(values);
        }
        [HttpGet]
        public IActionResult CreateAbout()
        {
            ViewBag.v1 = "Anasayfa";
            ViewBag.v2 = "Hakkımızda";
            ViewBag.v3 = "Hakkımızda Ekle";
            ViewBag.baslik = "Hakkımızda İşlemleri";
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateAbout(CreateAboutDto createAboutDto)
        {
            await _aboutService.CreateAboutAsync(createAboutDto);
            return Redirect("/Admin/About/Index");

        }
        public async Task<IActionResult> DeleteAbout(string id)
        {
            await _aboutService.DeleteAboutAsync(id);
            return Redirect("/Admin/About/Index");

        }
        [HttpGet]
        public async Task<IActionResult> UpdateAbout(string id)
        {
            ViewBag.v1 = "Anasayfa";
            ViewBag.v2 = "Hakkımızda";
            ViewBag.v3 = "Hakkımızda Güncelle";
            ViewBag.baslik = "Hakkımızda İşlemleri";

            var values = await _aboutService.GetByIdAboutAsync(id);

            return View(values);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateAbout(UpdateAboutDto updateAboutDto)
        {
            await _aboutService.UpdateAboutAsync(updateAboutDto);
            return Redirect("/Admin/About/Index");

        }
    }
}
