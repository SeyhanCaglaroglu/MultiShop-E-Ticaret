using Microsoft.AspNetCore.Mvc;
using MultiShop.DtoLayer.CatalogDtos.FeatureSliderDtos;
using MultiShop.WebUI.Services.CatalogServices.FeatureSliderServices;
using Newtonsoft.Json;
using System.Text;

namespace MultiShop.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class FeatureSliderController : Controller
    {
        private readonly IFeatureSliderService _featureSliderService;

        public FeatureSliderController(IFeatureSliderService featureSliderService)
        {
            _featureSliderService = featureSliderService;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.v1 = "Anasayfa";
            ViewBag.v2 = "Öne Çıkan Görseller";
            ViewBag.v3 = "Slayt Öne Çıkan Görsel Listesi";
            ViewBag.baslik = "Öne Çıkan Slayt Görsel İşlemleri";

            var values = await _featureSliderService.GetAllFeatureSliderAsync();

            return View(values);
        }
        [HttpGet]
        public IActionResult CreateFeatureSlider()
        {
            ViewBag.v1 = "Anasayfa";
            ViewBag.v2 = "Öne Çıkan Görseller";
            ViewBag.v3 = "Slayt Öne Çıkan Görsel Listesi";
            ViewBag.baslik = "Öne Çıkan Slayt Görsel İşlemleri";
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateFeatureSlider(CreateFeatureSliderDto createFeatureSliderDto)
        {
            createFeatureSliderDto.Status = false;

            await _featureSliderService.CreateFeatureSliderAsync(createFeatureSliderDto);

            return Redirect("/Admin/FeatureSlider/Index");

        }
        public async Task<IActionResult> DeleteFeatureSlider(string id)
        {
            await _featureSliderService.DeleteFeatureSliderAsync(id);
            return Redirect("/Admin/FeatureSlider/Index");

        }
        [HttpGet]
        public async Task<IActionResult> UpdateFeatureSlider(string id)
        {
            ViewBag.v1 = "Anasayfa";
            ViewBag.v2 = "Öne Çıkan Görseller";
            ViewBag.v3 = "Slayt Öne Çıkan Görsel Listesi";
            ViewBag.baslik = "Öne Çıkan Slayt Görsel İşlemleri";

            var value = await _featureSliderService.GetByIdFeatureSliderAsync(id);

            return View(value);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateFeatureSlider(UpdateFeatureSliderDto updateFeatureSliderDto)
        {
            await _featureSliderService.UpdateFeatureSliderAsync(updateFeatureSliderDto);
            return Redirect("/Admin/FeatureSlider/Index");

        }
    }
}
