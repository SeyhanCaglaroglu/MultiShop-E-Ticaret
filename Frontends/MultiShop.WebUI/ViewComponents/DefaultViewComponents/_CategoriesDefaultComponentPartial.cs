using Microsoft.AspNetCore.Mvc;
using MultiShop.DtoLayer.CatalogDtos.CategoryDtos;
using MultiShop.WebUI.Services.CatalogServices.CategoryServices;
using MultiShop.WebUI.Services.CatalogServices.ProductServices;
using Newtonsoft.Json;

namespace MultiShop.WebUI.ViewComponents.DefaultViewComponents
{
    public class _CategoriesDefaultComponentPartial : ViewComponent
    {
        private readonly ICategoryService _categoryService;
        private readonly IProductService _productService;
        public _CategoriesDefaultComponentPartial(ICategoryService categoryService, IProductService productService)
        {
            _categoryService = categoryService;
            _productService = productService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var values = await _categoryService.GetAllCategoriesAsync();

            var productCounts = new List<int>();

            foreach(var category in values)
            {
                var productCount = await _productService.GetProductCountByCategoryId(category.CategoryId);
                productCounts.Add(productCount);
            }

            ViewData["productCounts"] = productCounts;

            if (values != null && values.Any())
            {
                return View(values);
            }
            return View();
        }
    }
}
