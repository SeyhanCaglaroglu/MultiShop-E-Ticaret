using Microsoft.AspNetCore.Mvc;
using MultiShop.DtoLayer.CatalogDtos.ProductDtos;
using MultiShop.WebUI.Services.CatalogServices.OfferDiscountServices;
using MultiShop.WebUI.Services.CatalogServices.ProductServices;
using MultiShop.WebUI.Services.CommentServices;
using Newtonsoft.Json;
using System.Text.RegularExpressions;

namespace MultiShop.WebUI.ViewComponents.DefaultViewComponents
{
    public class _FeatureProductsComponentPartial : ViewComponent
    {
        private readonly IProductService _productService;
        private readonly ICommentService _commentService;
        private readonly IOfferDiscountService _offerDiscountService;
        public _FeatureProductsComponentPartial(IProductService productService, ICommentService commentService, IOfferDiscountService offerDiscountService)
        {
            _productService = productService;
            _commentService = commentService;
            _offerDiscountService = offerDiscountService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var values = await _productService.GetAllProductAsync();

            var commentCounts = new List<int>();
            var productAverageRaitings = new List<int>();
            var discountRates = new List<int>();
            var discountRate = 0;
            foreach (var product in values)
            {
                var commentCount = await _commentService.GetCommentCountByProductId(product.ProductId);

                //yorum ve puan
                var productComments = await _commentService.GetByProductIdWithCommentAsync(product.ProductId);
                var totalRaiting = productComments.Sum(x=>x.Raiting);

                decimal productAverageRaiting = commentCount > 0 ? totalRaiting / commentCount : 0; //urun p ort
                int roundedRaiting = (int)Math.Round(productAverageRaiting);
                productAverageRaitings.Add(roundedRaiting);

                //indirim
                var offerDiscount = await _offerDiscountService.GetOfferDiscountByCategoryId(product.CategoryId);
                
                if (offerDiscount != null)
                {
                    var match = Regex.Match(offerDiscount.Title, @"\d+");
                    discountRate = int.Parse(match.Value);
                }
                else
                {
                    discountRate = 0;
                }
                discountRates.Add(discountRate);

                commentCounts.Add(commentCount);
            }

            ViewData["discountRates"] = discountRates;

            ViewData["commentCounts"] = commentCounts;

            ViewData["productAverageRaitings"] = productAverageRaitings;

            if (values != null && values.Any())
            {
                return View(values);
            }
            return View();
        }
    }
}
