﻿using Microsoft.AspNetCore.Mvc;
using MultiShop.DtoLayer.CatalogDtos.FeatureDtos;
using MultiShop.WebUI.Services.CatalogServices.FeatureServices;
using Newtonsoft.Json;
using System.Text;

namespace MultiShop.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class FeatureController : Controller
    {
        private readonly IFeatureService _featureService;

        public FeatureController(IFeatureService featureService)
        {
            _featureService = featureService;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.v1 = "Anasayfa";
            ViewBag.v2 = "Öne Çıkanlar";
            ViewBag.v3 = "Öne Çıkan Alan Listesi";
            ViewBag.baslik = "Öne Çıkan Alan İşlemleri";

            var values = await _featureService.GetAllFeatureAsync();

            return View(values);
        }
        public IActionResult CreateFeature()
        {
            ViewBag.v1 = "Anasayfa";
            ViewBag.v2 = "Öne Çıkanlar";
            ViewBag.v3 = "Öne Çıkan Alan Ekle";
            ViewBag.baslik = "Öne Çıkan Alan İşlemleri";

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateFeature(CreateFeatureDto createFeatureDto)
        {
            await _featureService.CreateFeatureAsync(createFeatureDto);
            return Redirect("/Admin/Feature/Index");

        }
        public async Task<IActionResult> UpdateFeature(string id)
        {
            ViewBag.v1 = "Anasayfa";
            ViewBag.v2 = "Öne Çıkanlar";
            ViewBag.v3 = "Öne Çıkan Alan Güncelle";
            ViewBag.baslik = "Öne Çıkan Alan İşlemleri";

            var value = await _featureService.GetByIdFeatureAsync(id);

            return View(value);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateFeature(UpdateFeatureDto updateFeatureDto)
        {
            await _featureService.UpdateFeatureAsync(updateFeatureDto);
            return Redirect("/Admin/Feature/Index");

        }
        public async Task<IActionResult> DeleteFeature(string id)
        {
            await _featureService.DeleteFeatureAsync(id);
            return Redirect("/Admin/Feature/Index");

        }
    }
}
