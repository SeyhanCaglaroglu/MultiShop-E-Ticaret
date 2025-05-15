using Microsoft.AspNetCore.Mvc;
using MultiShop.DtoLayer.CargoDtos.CargoCompanyDtos;
using MultiShop.WebUI.Services.CargoServices.CargoCargoCompanys;

namespace MultiShop.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CargoController : Controller
    {
        private readonly ICargoCargoCompanyService _cargoCargoCompanyService;

        public CargoController(ICargoCargoCompanyService cargoCargoCompanyService)
        {
            _cargoCargoCompanyService = cargoCargoCompanyService;
        }

        public async Task<IActionResult> CargoCompanyList()
        {
            ViewBag.v1 = "Anasayfa";
            ViewBag.v2 = "Kargo";
            ViewBag.v3 = "Kargo Firmaları Listesi";
            ViewBag.baslik = "Kargo İşlemleri";
            var values = await _cargoCargoCompanyService.GetAllCargoCompanyAsync();
            return View(values);
        }
        public IActionResult CreateCargoCompany()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateCargoCompany(CreateCargoCompanyDto createCargoCompanyDto)
        {
            await _cargoCargoCompanyService.CreateCargoCompanyAsync(createCargoCompanyDto);
            return Redirect("/Admin/Cargo/CargoCompanyList");
        }

        public async Task<IActionResult> UpdateCargoCompany(int id)
        {
            var value = await _cargoCargoCompanyService.GetByIdCargoCompanyAsync(id);
            return View(value);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateCargoCompany(UpdateCargoCompanyDto updateCargoCompanyDto)
        {
            await _cargoCargoCompanyService.UpdateCargoCompanyAsync(updateCargoCompanyDto);
            return Redirect("/Admin/Cargo/CargoCompanyList");
        }
        public async Task<IActionResult> DeleteCargoCompany(int id)
        {
            await _cargoCargoCompanyService.DeleteCargoCompanyAsync(id);
            return Redirect("/Admin/Cargo/CargoCompanyList");
        }
    }
}
