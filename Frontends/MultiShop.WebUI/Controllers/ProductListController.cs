using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MultiShop.DtoLayer.CommentDtos;
using MultiShop.DtoLayer.NotificationDtos;
using MultiShop.WebUI.Services.CatalogServices.OfferDiscountServices;
using MultiShop.WebUI.Services.CatalogServices.ProductServices;
using MultiShop.WebUI.Services.CommentServices;
using MultiShop.WebUI.Services.NotificationServices;
using Newtonsoft.Json;
using System.Text;
using System.Text.RegularExpressions;
using X.PagedList;
using X.PagedList.Extensions;

namespace MultiShop.WebUI.Controllers
{
    public class ProductListController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICommentService _commentService;
        private readonly INotificationService _notificationService;
        private readonly IOfferDiscountService _offerDiscountService;
        public ProductListController(IProductService productService, ICommentService commentService, INotificationService notificationService, IOfferDiscountService offerDiscountService)
        {
            _productService = productService;
            _commentService = commentService;
            _notificationService = notificationService;
            _offerDiscountService = offerDiscountService;
        }

        public async Task<IActionResult> Index(string id, string? range, string? productName, int page = 1)
        {
            ViewBag.Id = id;

            int pageSize = 6; // Her sayfada gösterilecek ürün sayısı

            // Tüm ürünleri al
            var values = await _productService.GetProductsByCategoryId(id);

            // Eğer range parametresi varsa, fiyat filtresi uygula
            var filteredProducts = range != null ? await _productService.GetProductsFilteredByPriceWithCategory(id,range) : null;

            // İşlenecek ürünleri belirle
            var productsToProcess = filteredProducts ?? values;

            if (!string.IsNullOrEmpty(productName))
            {
                productsToProcess = await _productService.GetProductsWithSearch(productName);
            }

            // İndirim oranlarının hesaplanması
            int discountRate = 0;
            var discountRates = new List<int>();

            foreach (var product in productsToProcess)
            {
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
            }

            ViewData["discountRates"] = discountRates;

            // Yorum sayısı ve puan hesaplamaları
            var commentCounts = new List<int>();
            var productAverageRaitings = new List<int>();

            foreach (var product in productsToProcess)
            {
                var commentCount = await _commentService.GetCommentCountByProductId(product.ProductId);

                var productComments = await _commentService.GetByProductIdWithCommentAsync(product.ProductId);
                var totalRaiting = productComments.Sum(x => x.Raiting);

                decimal productAverageRaiting = commentCount > 0 ? totalRaiting / commentCount : 0;
                int roundedRaiting = (int)Math.Round(productAverageRaiting);
                productAverageRaitings.Add(roundedRaiting);

                commentCounts.Add(commentCount);
            }

            ViewData["commentCounts"] = commentCounts;
            ViewData["productAverageRaitings"] = productAverageRaitings;

            // Sayfalama için ürünleri böl
            var paginatedProducts = productsToProcess
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
            .ToList();

            ViewData["CurrentPage"] = page;
            ViewData["TotalPages"] = (int)Math.Ceiling(productsToProcess.Count / (double)pageSize);
            ViewData["ProductName"] = productName;
            ViewData["range"] = range;

            if (paginatedProducts.Any())
            {
                return View(paginatedProducts);
            }

            return View();
        }

        public async Task<IActionResult> AllProducts(string? range, string? productName, int page = 1)
        {
            int pageSize = 6; // Her sayfada gösterilecek ürün sayısı

            // Tüm ürünleri al
            var values = await _productService.GetAllProductAsync();

            // Eğer range parametresi varsa, fiyat filtresi uygula
            var filteredProducts = range != null ? await _productService.GetProductsFilteredByPrice(range) : null;

            // İşlenecek ürünleri belirle
            var productsToProcess = filteredProducts ?? values;

            if (!string.IsNullOrEmpty(productName))
            {
                productsToProcess = await _productService.GetProductsWithSearch(productName);
            }

            // İndirim oranlarının hesaplanması
            int discountRate = 0;
            var discountRates = new List<int>();

            foreach (var product in productsToProcess)
            {
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
            }

            ViewData["discountRates"] = discountRates;

            // Yorum sayısı ve puan hesaplamaları
            var commentCounts = new List<int>();
            var productAverageRaitings = new List<int>();

            foreach (var product in productsToProcess)
            {
                var commentCount = await _commentService.GetCommentCountByProductId(product.ProductId);

                var productComments = await _commentService.GetByProductIdWithCommentAsync(product.ProductId);
                var totalRaiting = productComments.Sum(x => x.Raiting);

                decimal productAverageRaiting = commentCount > 0 ? totalRaiting / commentCount : 0;
                int roundedRaiting = (int)Math.Round(productAverageRaiting);
                productAverageRaitings.Add(roundedRaiting);

                commentCounts.Add(commentCount);
            }

            ViewData["commentCounts"] = commentCounts;
            ViewData["productAverageRaitings"] = productAverageRaitings;

            // Sayfalama için ürünleri böl
            var paginatedProducts = productsToProcess
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            ViewData["CurrentPage"] = page;
            ViewData["TotalPages"] = (int)Math.Ceiling(productsToProcess.Count / (double)pageSize);
            ViewData["ProductName"] = productName;
            ViewData["range"] = range;

            if (paginatedProducts.Any())
            {
                return View(paginatedProducts);
            }

            return View();
        }




        public async Task<IActionResult> ProductDetail(string id, decimal number)
        {
            ViewBag.ProductId = id;
            ViewBag.discountRate = number;

            var commentCount = await _commentService.GetCommentCountByProductId(id);

            var productComments = await _commentService.GetByProductIdWithCommentAsync(id);

            var totalRaiting = productComments.Sum(x => x.Raiting);

            decimal productAverageRaiting = commentCount > 0 ? totalRaiting / commentCount : 0;
            int roundedAverageRaiting = (int)Math.Round(productAverageRaiting);

            ViewBag.commentCount = commentCount;
            ViewBag.roundedAverageRaiting = roundedAverageRaiting;

            return View();
        }
        public PartialViewResult AddComment()
        {
            return PartialView();
        }
        [HttpPost]
        public async Task<IActionResult> AddComment(CreateCommentDto createCommentDto)
        {
            createCommentDto.ImageUrl = "test";

            createCommentDto.CreatedDate = DateTime.Parse(DateTime.Now.ToShortDateString());
            createCommentDto.Status = false;

            var productName = await _productService.GetByIdProductAsync(createCommentDto.ProductId);


            //notification
            CreateNotificationDto createNotificationDto = new CreateNotificationDto();

            createNotificationDto.Title = createCommentDto.NameSurname + " " + productName.ProductName + " Ürününe Yorum Yaptı";
            createNotificationDto.Description = createCommentDto.CommentDetail;
            await _notificationService.CreateNotificationAsync(createNotificationDto);

            await _commentService.CreateCommentAsync(createCommentDto);
            return Redirect($"/ProductList/ProductDetail/{createCommentDto.ProductId}");

        }
    }
}
