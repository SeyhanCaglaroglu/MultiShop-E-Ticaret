using Microsoft.AspNetCore.Mvc;
using MultiShop.WebUI.Services.CargoServices.CargoCustomerServices;
using MultiShop.WebUI.Services.UserIdentityService;

namespace MultiShop.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UserController : Controller
    {
        private readonly IUserIdentityService _userIdentityService;
        private readonly ICargoCustomerService _cargoCustomerService;
        public UserController(IUserIdentityService userIdentityService, ICargoCustomerService cargoCustomerService)
        {
            _userIdentityService = userIdentityService;
            _cargoCustomerService = cargoCustomerService;
        }

        public async Task<IActionResult> UserList()
        {
            ViewBag.v1 = "Anasayfa";
            ViewBag.v2 = "Kullanıcılar";
            ViewBag.v3 = "Kullanıcı Listesi";
            ViewBag.baslik = "Kullanıcı İşlemleri";

            var values = await _userIdentityService.GetAllUserList();
            return View(values);
        }
        public async Task<IActionResult> UserAdressInfo(string id)
        {
            ViewBag.v1 = "Anasayfa";
            ViewBag.v2 = "Kullanıcılar";
            ViewBag.v3 = "Kullanıcı";
            ViewBag.baslik = "Kullanıcı İşlemleri";
            var value = await _cargoCustomerService.GetCargoCustomerByCustomerId(id);
            return View(value);
        }
    }
}
