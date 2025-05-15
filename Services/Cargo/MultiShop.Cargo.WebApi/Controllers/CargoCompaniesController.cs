using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MultiShop.Cargo.BusinessLayer.Abstract;
using MultiShop.Cargo.DtoLayer.Dtos.CargoCompanyDto;
using MultiShop.Cargo.EntityLayer.Concrete;

namespace MultiShop.Cargo.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CargoCompaniesController : ControllerBase
    {
        private readonly ICargoCompanyService _cargoCompanyService;

        public CargoCompaniesController(ICargoCompanyService cargoCompanyService)
        {
            _cargoCompanyService = cargoCompanyService;
        }

        [HttpGet]
        public async Task<IActionResult> CargoCompanyList()
        {
            var values = await _cargoCompanyService.TGetAllAsync();
            return Ok(values);
        }
        [HttpPost]
        public async Task<IActionResult> CreateCargoCompany(CreateCargoCompanyDto createCargoCompanyDto)
        {
            CargoCompany cargoCompany = new()
            {
                CargoCompanyName = createCargoCompanyDto.CargoCompanyName,
            };
            await _cargoCompanyService.TInsertAsync(cargoCompany);
            return Ok("Kargo Şirketi Başarıyla Eklendi !");
        }
        [HttpDelete]
        public async Task<IActionResult> RemoveCargoCompany(int id)
        {
            await _cargoCompanyService.TDeleteAsync(id);
            return Ok("Kargo Şirketi Başarıyla Silindi !");
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCargoCompanyById(int id)
        {
            var value = await _cargoCompanyService.TGetByIdAsync(id);
            return Ok(value);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateCargoCompany(UpdateCargoCompanyDto updateCargoCompanyDto)
        {
            CargoCompany cargoCompany = new()
            {
                CargoCompanyId = updateCargoCompanyDto.CargoCompanyId,
                CargoCompanyName = updateCargoCompanyDto.CargoCompanyName
            };
            await _cargoCompanyService.TUpdateAsync(cargoCompany);
            return Ok("Kargo Şirketi Başarıyla Güncellendi !");
        }
    }
}
