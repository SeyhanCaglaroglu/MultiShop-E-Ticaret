﻿using Microsoft.AspNetCore.Mvc;
using MultiShop.DtoLayer.CatalogDtos.BrandDtos;
using MultiShop.WebUI.Services.CatalogServices.BrandServices;
using Newtonsoft.Json;

namespace MultiShop.WebUI.ViewComponents.DefaultViewComponents
{
    public class _VendorDefaultComponentPartial : ViewComponent
    {
        private readonly IBrandService _brandService;

        public _VendorDefaultComponentPartial(IBrandService brandService)
        {
            _brandService = brandService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var values = await _brandService.GetAllBrandsAsync();

            if (values != null && values.Any())
            {
                return View(values);
            }
            return View();
        }
    }
}
