using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MultiShop.DtoLayer.BasketDtos;
using MultiShop.DtoLayer.CatalogDtos.OfferDiscountDtos;
using MultiShop.DtoLayer.CatalogDtos.ProductDtos;
using MultiShop.WebUI.Services.BasketServices;
using MultiShop.WebUI.Services.CatalogServices.OfferDiscountServices;
using MultiShop.WebUI.Services.CatalogServices.ProductServices;
using MultiShop.WebUI.Services.DiscountService;
using Newtonsoft.Json.Linq;
using System.Text.RegularExpressions;

namespace MultiShop.WebUI.Controllers
{
    [Authorize]
    public class ShoppingCartController : Controller
    {
        private readonly IProductService _productService;
        private readonly IBasketService _basketService;
        private readonly IDiscountService _discountService;
        private readonly IOfferDiscountService _offerDiscountService;
        public ShoppingCartController(IBasketService basketService, IProductService productService, IDiscountService discountService, IOfferDiscountService offerDiscountService)
        {
            _basketService = basketService;
            _productService = productService;
            _discountService = discountService;
            _offerDiscountService = offerDiscountService;
        }

        public async Task<IActionResult> Index(string? code, int? discountRate, decimal? DiscountPrice)
        {
            var values = await _basketService.GetBasket();
            ViewBag.Code = code;
            ViewBag.Rate = discountRate;
            ViewBag.DiscountPrice = DiscountPrice;

            var basketProductList = new List<UpdateProductDto>();
            var offerDiscounts = new List<int>();
            decimal totalPriceWithDiscount = 0;

            foreach (var item in values.BasketItems)
            {
                var product = await _productService.GetByIdProductAsync(item.ProductId);
                var offerDiscount = await _offerDiscountService.GetOfferDiscountByCategoryId(product.CategoryId);
                var discountOfferRate = 0;

                if (offerDiscount != null)
                {
                    var match = Regex.Match(offerDiscount.Title, @"\d+");
                    discountOfferRate = int.Parse(match.Value);
                }

                // İndirim uygulanmış fiyat hesaplanıyor
                var discountedPrice = item.Price * (1 - ((decimal)discountOfferRate / 100));
                totalPriceWithDiscount += discountedPrice * item.Quantity;

                offerDiscounts.Add(discountOfferRate);
                basketProductList.Add(product);
            }

            ViewData["offerDiscounts"] = offerDiscounts;

            // İndirimli toplam fiyat
            ViewBag.TotalPrice = totalPriceWithDiscount;
            ViewBag.TotalPriceWithTax = totalPriceWithDiscount + totalPriceWithDiscount / 100 * 10;
            ViewBag.Tax = totalPriceWithDiscount / 100 * 10;

            return View();
        }


        public async Task<IActionResult> AddBasketItemm(string id, decimal discountedPrice, string size)
        {
            var value = await _productService.GetByIdProductAsync(id);
            var items = new BasketItemDto
            {
                ProductId = value.ProductId,
                
                Price = value.ProductPrice,
                ProductName = value.ProductName,
                Quantity = 1,
                ProductImageUrl = value.ProductImageUrl
            };
            if (discountedPrice != 0)
            {
                items.Price = discountedPrice;
            }
            await _basketService.AddBasketItem(items);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> RemoveBasketItem(string id)
        {
            await _basketService.RemoveBasketItem(id);
            return RedirectToAction("Index");
        }
    }
}
