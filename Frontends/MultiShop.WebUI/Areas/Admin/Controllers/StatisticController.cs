using Microsoft.AspNetCore.Mvc;
using MultiShop.WebUI.Services.StatisticServices.CatalogStatisticServices;
using MultiShop.WebUI.Services.StatisticServices.CommentStatisticServices;
using MultiShop.WebUI.Services.StatisticServices.DiscountStatisticServices;
using MultiShop.WebUI.Services.StatisticServices.MessageStatisticServices;
using MultiShop.WebUI.Services.StatisticServices.UserStatisticServices;

namespace MultiShop.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class StatisticController : Controller
    {
        private readonly ICatalogStatisticService _catalogStatisticService;
        private readonly IUserStatisticServices _userStatisticService;
        private readonly ICommentStatisticService _commentStatisticService;
        private readonly IDiscountStatisticService _discountStatisticService;
        private readonly IMessageStatisticService _messageStatisticService;
        public StatisticController(ICatalogStatisticService catalogStatisticService, IUserStatisticServices userStatisticService, ICommentStatisticService commentStatisticService, IDiscountStatisticService discountStatisticService, IMessageStatisticService messageStatisticService)
        {
            _catalogStatisticService = catalogStatisticService;
            _userStatisticService = userStatisticService;
            _commentStatisticService = commentStatisticService;
            _discountStatisticService = discountStatisticService;
            _messageStatisticService = messageStatisticService;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.v1 = "Anasayfa";
            ViewBag.v2 = "İstatistikler";
            ViewBag.v3 = "İstatistik Listesi";
            ViewBag.baslik = "İstatistik İşlemleri";

            //CatalogStatistics
            var BrandCount = await _catalogStatisticService.GetBrandCount();
            var CategoryCount = await _catalogStatisticService.GetCategoryCount();
            var ProductCount = await _catalogStatisticService.GetProductCount();
            var ProductAvgPrice = await _catalogStatisticService.GetProductAvgPrice();
            var MaxPriceProductName = await _catalogStatisticService.GetMaxPriceProductName();
            var MinPriceProductName = await _catalogStatisticService.GetMinPriceProductName();

            ViewBag.BrandCount = BrandCount;
            ViewBag.CategoryCount = CategoryCount;
            ViewBag.ProductCount = ProductCount;
            ViewBag.ProductAvgPrice = ProductAvgPrice;
            ViewBag.MaxPriceProductName = MaxPriceProductName;
            ViewBag.MinPriceProductName = MinPriceProductName;

            //UserStatistic
            var UserCount = await _userStatisticService.GetUserCount();
            ViewBag.UserCount = UserCount;

            //CommentStatistic
            var ActiveCommentCount = await _commentStatisticService.GetActiveCommentCount();
            var PassiveCommentCount = await _commentStatisticService.GetPassiveCommentCount();
            var TotalCommentCount = await _commentStatisticService.GetTotalCommentCount();

            ViewBag.ActiveCommentCount = ActiveCommentCount;
            ViewBag.PassiveCommentCount = PassiveCommentCount;
            ViewBag.TotalCommentCount = TotalCommentCount;

            //DisocuntStatistic
            var DiscountCouponCount = await _discountStatisticService.GetDiscountCouponCount();
            ViewBag.DiscountCouponCount = DiscountCouponCount;

            //MessageStatistic
            var TotalMessageCount = await _messageStatisticService.GetTotalMessageCount();
            ViewBag.TotalMessageCount = TotalMessageCount;
            return View();
        }
    }
}
