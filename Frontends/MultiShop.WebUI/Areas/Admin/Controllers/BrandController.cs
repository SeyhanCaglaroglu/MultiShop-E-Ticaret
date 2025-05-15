using Microsoft.AspNetCore.Mvc;
using MultiShop.DtoLayer.CatalogDtos.BrandDtos;
using MultiShop.WebUI.Services.CatalogServices.BrandServices;
using Newtonsoft.Json;
using System.Text;

namespace MultiShop.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BrandController : Controller
    {
        private readonly IBrandService _brandService;

        public BrandController(IBrandService BrandService)
        {
            _brandService = BrandService;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.v1 = "Anasayfa";
            ViewBag.v2 = "Markalar";
            ViewBag.v3 = "Marka Listesi";
            ViewBag.baslik = "Marka İşlemleri";

            var values = await _brandService.GetAllBrandsAsync();
            return View(values);
        }
        [HttpGet]
        public IActionResult CreateBrand()
        {
            ViewBag.v1 = "Anasayfa";
            ViewBag.v2 = "Markalar";
            ViewBag.v3 = "Marka Ekle";
            ViewBag.baslik = "Marka İşlemleri";
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateBrand(CreateBrandDto createBrandDto)
        {
            await _brandService.CreateBrandAsync(createBrandDto);
            return Redirect("/Admin/Brand/Index");

        }
        public async Task<IActionResult> DeleteBrand(string id)
        {
            await _brandService.DeleteBrandAsync(id);
            return Redirect("/Admin/Brand/Index");

        }
        [HttpGet]
        public async Task<IActionResult> UpdateBrand(string id)
        {
            ViewBag.v1 = "Anasayfa";
            ViewBag.v2 = "Markalar";
            ViewBag.v3 = "Marka Güncelle";
            ViewBag.baslik = "Marka İşlemleri";

            var values = await _brandService.GetByIdBrandAsync(id);

            return View(values);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateBrand(UpdateBrandDto updateBrandDto)
        {
            await _brandService.UpdateBrandAsync(updateBrandDto);
            return Redirect("/Admin/Brand/Index");

        }
    }
}
