using Microsoft.AspNetCore.Mvc;
using MultiShop.DtoLayer.CatalogDtos.SpecialOfferDtos;
using MultiShop.WebUI.Areas.Admin.Models;
using MultiShop.WebUI.Services.CatalogServices.ProductServices;
using MultiShop.WebUI.Services.CatalogServices.SpecialOfferServices;
using Newtonsoft.Json;
using System.Text;

namespace MultiShop.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SpecialOfferController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ISpecialOfferService _specialOfferService;
        private readonly IProductService _productService;
        public SpecialOfferController(IHttpClientFactory httpClientFactory, ISpecialOfferService specialOfferService, IProductService productService)
        {
            _httpClientFactory = httpClientFactory;
            _specialOfferService = specialOfferService;
            _productService = productService;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.v1 = "Anasayfa";
            ViewBag.v2 = "Özel Teklifler";
            ViewBag.v3 = "Özel Teklif ve Günün İndirimi Listesi";
            ViewBag.baslik = "İndirim İşlemleri";

            var values = await _specialOfferService.GetAllSpecialOffersAsync();

            return View(values);
        }
        [HttpGet]
        public async Task<IActionResult> CreateSpecialOffer()
        {
            ViewBag.v1 = "Anasayfa";
            ViewBag.v2 = "Özel Teklifler";
            ViewBag.v3 = "Özel Teklif ve Günün İndirimi Ekle";
            ViewBag.baslik = "İndirim İşlemleri";

            var values = await _productService.GetAllProductAsync();

            var specialOfferViewModel = new SpecialOfferViewModel
            {
                Products = values
            };

            return View(specialOfferViewModel);
        }
        [HttpPost]
        public async Task<IActionResult> CreateSpecialOffer(SpecialOfferViewModel model)
        {
            await _specialOfferService.CreateSpecialOfferAsync(model.CreateSpecialOfferDto);
            return Redirect("/Admin/SpecialOffer/Index");

        }
        public async Task<IActionResult> UpdateSpecialOffer(string id)
        {
            ViewBag.v1 = "Anasayfa";
            ViewBag.v2 = "Özel Teklifler";
            ViewBag.v3 = "Özel Teklif ve Günün İndirimi Güncelle";
            ViewBag.baslik = "İndirim İşlemleri";

            var value = await _specialOfferService.GetByIdSpecialOfferAsync(id);

            var products = await _productService.GetAllProductAsync();

            var specialOfferViewModel = new SpecialOfferViewModel
            {
                Products = products,
                updateSpecialOfferDto = value
            };

            return View(specialOfferViewModel);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateSpecialOffer(SpecialOfferViewModel model)
        {
            await _specialOfferService.UpdateSpecialOfferAsync(model.updateSpecialOfferDto);
            return Redirect("/Admin/SpecialOffer/Index");

        }
        public async Task<IActionResult> DeleteSpecialOffer(string id)
        {
            await _specialOfferService.DeleteSpecialOfferAsync(id);
            return Redirect("/Admin/SpecialOffer/Index");

        }
    }
}
