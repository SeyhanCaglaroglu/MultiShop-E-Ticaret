using Microsoft.AspNetCore.Mvc;
using MultiShop.DtoLayer.CatalogDtos.OfferDiscountDtos;
using MultiShop.WebUI.Areas.Admin.Models;
using MultiShop.WebUI.Services.CatalogServices.CategoryServices;
using MultiShop.WebUI.Services.CatalogServices.OfferDiscountServices;
using Newtonsoft.Json;
using System.Text;

namespace MultiShop.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class OfferDiscountController : Controller
    {
        private readonly IOfferDiscountService _offerDiscountService;
        private readonly ICategoryService _categoryService;
        public OfferDiscountController(IOfferDiscountService offerDiscountService, ICategoryService categoryService)
        {
            _offerDiscountService = offerDiscountService;
            _categoryService = categoryService;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.v1 = "Anasayfa";
            ViewBag.v2 = "İndirim Teklifleri";
            ViewBag.v3 = "İndirim Teklif Listesi";
            ViewBag.baslik = "İndirim Teklif İşlemleri";

            var values = await _offerDiscountService.GetAllOfferDiscountsAsync();

            return View(values);
        }
        public async Task<IActionResult> CreateOfferDiscount()
        {
            ViewBag.v1 = "Anasayfa";
            ViewBag.v2 = "İndirim Teklifleri";
            ViewBag.v3 = "İndirim Teklifi Ekle";
            ViewBag.baslik = "İndirim Teklif İşlemleri";

            var categories = await _categoryService.GetAllCategoriesAsync();

            var offerDiscountViewModel = new OfferDiscountViewModel
            {
                ResultCategories = categories,
            };

            return View(offerDiscountViewModel);
        }
        [HttpPost]
        public async Task<IActionResult> CreateOfferDiscount(OfferDiscountViewModel model)
        {
            await _offerDiscountService.CreateOfferDiscountAsync(model.CreateOfferDiscountDto);
            return Redirect("/Admin/OfferDiscount/Index");

        }
        public async Task<IActionResult> UpdateOfferDiscount(string id)
        {
            ViewBag.v1 = "Anasayfa";
            ViewBag.v2 = "İndirim Teklifleri";
            ViewBag.v3 = "İndirim Teklifini Güncelle";
            ViewBag.baslik = "İndirim Teklif İşlemleri";

            var value = await _offerDiscountService.GetByIdOfferDiscountAsync(id);
            
            var categories = await _categoryService.GetAllCategoriesAsync();

            var offerDiscountViewModel = new OfferDiscountViewModel
            {
                UpdateOfferDiscountDto = value,
                ResultCategories = categories
            };

            return View(offerDiscountViewModel);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateOfferDiscount(OfferDiscountViewModel model)
        {
            await _offerDiscountService.UpdateOfferDiscountAsync(model.UpdateOfferDiscountDto);
            return Redirect("/Admin/OfferDiscount/Index");

        }
        public async Task<IActionResult> DeleteOfferDiscount(string id)
        {
            await _offerDiscountService.DeleteOfferDiscountAsync(id);
            return Redirect("/Admin/OfferDiscount/Index");

        }
    }
}
