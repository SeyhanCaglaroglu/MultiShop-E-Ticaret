using Microsoft.AspNetCore.Mvc;
using MultiShop.DtoLayer.OrderDtos.AdressDto;
using MultiShop.WebUI.Services.Interfaces;
using MultiShop.WebUI.Services.OrderServices.AdressService;

namespace MultiShop.WebUI.Controllers
{
    public class OrderController : Controller
    {
        private readonly IAdressService _adressService;
        private readonly IUserService _userService;
        public OrderController(IAdressService adressService, IUserService userService)
        {
            _adressService = adressService;
            _userService = userService;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Index(CreateAdressDto createAdressDto)
        {
            var user = await _userService.GetUserInfo();
            
            createAdressDto.UserId = user.Id;
            createAdressDto.Description = "aaa";

            await _adressService.CreateAdressAsync(createAdressDto);
            return RedirectToAction("Index","Payment");
        }
    }
}
