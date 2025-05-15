using Microsoft.AspNetCore.Mvc;
using MultiShop.WebUI.Services.BasketServices;

namespace MultiShop.WebUI.ViewComponents.OrderViewComponents
{
    public class _SummaryOrderComponentPartial : ViewComponent
    {
        private readonly IBasketService _basketService;
        public _SummaryOrderComponentPartial(IBasketService basketService)
        {
            _basketService = basketService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var BasketTotal = await _basketService.GetBasket();
            var BasketItems = BasketTotal.BasketItems;
            return View(BasketItems);
        }
    }
}
