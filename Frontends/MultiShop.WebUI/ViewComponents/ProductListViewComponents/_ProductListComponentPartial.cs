using Microsoft.AspNetCore.Mvc;
using MultiShop.DtoLayer.CatalogDtos.ProductDtos;
using MultiShop.WebUI.Services.CatalogServices.OfferDiscountServices;
using MultiShop.WebUI.Services.CatalogServices.ProductServices;
using MultiShop.WebUI.Services.CommentServices;
using Newtonsoft.Json;
using System.Text.RegularExpressions;

namespace MultiShop.WebUI.ViewComponents.ProductListViewComponents
{
    public class _ProductListComponentPartial : ViewComponent
    {
        private readonly IProductService _productService;
        private readonly IOfferDiscountService _offerDiscountService;
        private readonly ICommentService _commentService;
        public _ProductListComponentPartial(IProductService productService, IOfferDiscountService offerDiscountService, ICommentService commentService)
        {
            _productService = productService;
            _offerDiscountService = offerDiscountService;
            _commentService = commentService;
        }

        public async Task<IViewComponentResult> InvokeAsync(string id)
        {
            //Kategoriye ait urunler
            var values = await _productService.GetProductWithCategoryByCategoryIdAsync(id);

            //Indirim oraninin Hesaplanmasi
            int discountRate = 0;

            var offerDiscount = await _offerDiscountService.GetOfferDiscountByCategoryId(id);

            if (offerDiscount != null)
            {
                var match = Regex.Match(offerDiscount.Title, @"\d+");

                if (match.Success)
                {
                    discountRate = int.Parse(match.Value);
                }
                else
                {
                    discountRate = 0;
                }
            }

            ViewBag.discountRate = discountRate;
            //Indirim orani bitis


            var commentCounts = new List<int>();
            var productAverageRaitings = new List<int>();

            foreach (var product in values)
            {
                var commentCount = await _commentService.GetCommentCountByProductId(product.ProductId);

                var productComments = await _commentService.GetByProductIdWithCommentAsync(product.ProductId);
                var totalRaiting = productComments.Sum(x => x.Raiting);

                decimal productAverageRaiting = commentCount > 0 ? totalRaiting / commentCount : 0; //urun p ort
                int roundedRaiting = (int)Math.Round(productAverageRaiting);
                productAverageRaitings.Add(roundedRaiting);

                commentCounts.Add(commentCount);
            }
            ViewData["commentCounts"] = commentCounts;

            ViewData["productAverageRaitings"] = productAverageRaitings;
            //yorum ve puan bitis

            

            if (values != null && values.Any())
            {
                return View(values);
            }
            return View();
        }
    }
}
