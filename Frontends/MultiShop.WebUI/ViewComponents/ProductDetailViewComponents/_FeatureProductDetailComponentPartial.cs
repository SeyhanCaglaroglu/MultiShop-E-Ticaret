using Microsoft.AspNetCore.Mvc;
using MultiShop.DtoLayer.CatalogDtos.ProductDtos;
using MultiShop.WebUI.Services.CatalogServices.OfferDiscountServices;
using MultiShop.WebUI.Services.CatalogServices.ProductServices;
using Newtonsoft.Json;
using System.Text.RegularExpressions;

namespace MultiShop.WebUI.ViewComponents.ProductDetailViewComponents
{
    public class _FeatureProductDetailComponentPartial : ViewComponent
    {
        private readonly IProductService _productService;
        private readonly IOfferDiscountService _offerDiscountService;
        public _FeatureProductDetailComponentPartial(IProductService productService, IOfferDiscountService offerDiscountService)
        {
            _productService = productService;
            _offerDiscountService = offerDiscountService;
        }

        public async Task<IViewComponentResult> InvokeAsync(string id, decimal number,int commentCount, int roundedAverageRaiting)
        {
            ViewBag.commentCount = commentCount;
            ViewBag.roundedAverageRaiting = roundedAverageRaiting;

            ViewBag.discountRate = number;
            var values = await _productService.GetByIdProductAsync(id);
            var offerDiscount = await _offerDiscountService.GetOfferDiscountByCategoryId(values.CategoryId);

            int categoryDiscountRate = 0;

            if(offerDiscount != null)
            {
                var numericValue = Regex.Match(offerDiscount.Title, @"\d+");
                categoryDiscountRate = int.Parse(numericValue.Value);
            }
            else
            {
                categoryDiscountRate = 0;
            }
            ViewBag.categoryDiscountRate = categoryDiscountRate;

            if (values != null)
            {
                return View(values);
            }
            return View();
        }
    }
}
