using Microsoft.AspNetCore.Mvc;
using MultiShop.WebUI.Services.Interfaces;

namespace MultiShop.WebUI.Areas.User.ViewComponents.UserLayoutViewComponents
{
    public class _NavbarUserLayoutComponentPartial : ViewComponent
    {
        private readonly IUserService _userService;

        public _NavbarUserLayoutComponentPartial(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var user = await _userService.GetUserInfo();

            ViewBag.UserNameSurname = user.Name + " " + user.Surname;

            return View();
        }
    }
}
