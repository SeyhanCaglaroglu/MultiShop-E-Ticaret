using Microsoft.AspNetCore.Mvc;
using MultiShop.WebUI.Services.BasketServices;
using MultiShop.WebUI.Services.DiscountService;

namespace MultiShop.WebUI.Controllers
{
    public class DiscountController : Controller
    {
        private readonly IBasketService _basketService;
        private readonly IDiscountService _discountService;

        public DiscountController(IDiscountService discountService, IBasketService basketService)
        {
            _discountService = discountService;
            _basketService = basketService;
        }

        [HttpGet]
        public PartialViewResult ConfirmDiscountCoupon(decimal TotalPrice, decimal Tax, int Rate, decimal DiscountPrice,decimal TotalPriceWithTax)
        {
            ViewBag.Rate = Rate;
            ViewBag.DiscountPrice = DiscountPrice;
            ViewBag.TotalPrice = TotalPrice;
            ViewBag.Tax = Tax;
            ViewBag.TotalPriceWithTax = TotalPriceWithTax;

            return PartialView();
        }
        [HttpPost]
        public async Task<IActionResult> ConfirmDiscountCoupon(string code)
        {
            var values = await _discountService.GetDiscountCode(code);
            var basketValues = await _basketService.GetBasket();

            var TotalPriceWithTax = basketValues.TotalPrice + basketValues.TotalPrice / 100 * 10;
            var DiscountPrice = TotalPriceWithTax - (TotalPriceWithTax / 100 * values.Rate);

            ViewBag.DiscountPrice = DiscountPrice;
            ViewBag.TotalPriceWithTax = TotalPriceWithTax;

            return RedirectToAction("Index", "ShoppingCart", new {code=code,discountRate = values.Rate, DiscountPrice = DiscountPrice});
        }
    }
}
