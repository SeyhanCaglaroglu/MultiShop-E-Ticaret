using Microsoft.AspNetCore.Mvc;
using MultiShop.DtoLayer.CatalogDtos.CategoryDtos;
using MultiShop.WebUI.Services.CatalogServices.CategoryServices;
using MultiShop.WebUI.Services.UserIdentityService;
using Newtonsoft.Json;

namespace MultiShop.WebUI.ViewComponents.UILayoutViewComponents
{
    public class _NavbarUILayoutComponentPartial : ViewComponent
    {
        private readonly ICategoryService _categoryService;
        private readonly IUserIdentityService _userIdentityService;
        public _NavbarUILayoutComponentPartial(ICategoryService categoryService, IUserIdentityService userIdentityService)
        {
            _categoryService = categoryService;
            _userIdentityService = userIdentityService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {

            if (User.Identity?.IsAuthenticated == true)
            {
                var userRole = await _userIdentityService.GetUserRole();

                ViewBag.UserRole = userRole;
            }

            var values = await _categoryService.GetAllCategoriesAsync();

            if (values != null && values.Any())
            {
                return View(values);
            }
            return View();
        }
    }
}
