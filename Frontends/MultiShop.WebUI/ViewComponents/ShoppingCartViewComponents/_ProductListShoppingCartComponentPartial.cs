using Microsoft.AspNetCore.Mvc;
using MultiShop.WebUI.Services.BasketServices;

namespace MultiShop.WebUI.ViewComponents.ShoppingCartViewComponents
{
    public class _ProductListShoppingCartComponentPartial : ViewComponent
    {
        private readonly IBasketService _basketService;
        public _ProductListShoppingCartComponentPartial(IBasketService basketService)
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
