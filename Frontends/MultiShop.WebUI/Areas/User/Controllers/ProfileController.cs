using Microsoft.AspNetCore.Mvc;
using MultiShop.DtoLayer.CargoDtos.CargoCustomerDtos;
using MultiShop.WebUI.Services.CargoServices.CargoCustomerServices;
using MultiShop.WebUI.Services.Interfaces;

namespace MultiShop.WebUI.Areas.User.Controllers
{
    [Area("User")]
    public class ProfileController : Controller
    {
        private readonly IUserService _userService;
        private readonly ICargoCustomerService _cargoCustomerService;
        public ProfileController(IUserService userService, ICargoCustomerService cargoCustomerService)
        {
            _userService = userService;
            _cargoCustomerService = cargoCustomerService;
        }

        public async Task<IActionResult> Index()
        {
            var user = await _userService.GetUserInfo();

            var userInformation = await _cargoCustomerService.GetCargoCustomerByCustomerId(user.Id);

            return View(userInformation);
        }
        public async Task<IActionResult> UpdateProfile(string id)
        {
            var userInformation = await _cargoCustomerService.GetCargoCustomerByCustomerId(id);
            return View(userInformation);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateProfile(UpdateCargoCustomerDto updateCargoCustomerDto)
        {
            await _cargoCustomerService.UpdateCargoCustomer(updateCargoCustomerDto);
            return Redirect("/User/Profile/Index");
        }
    }
}
