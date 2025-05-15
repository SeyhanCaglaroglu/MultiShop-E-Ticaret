using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MultiShop.Cargo.BusinessLayer.Abstract;
using MultiShop.Cargo.DtoLayer.Dtos.CargoDetailDto;
using MultiShop.Cargo.EntityLayer.Concrete;

namespace MultiShop.Cargo.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CargoDetailsController : ControllerBase
    {
        private readonly ICargoDetailService _cargoDetailService;

        public CargoDetailsController(ICargoDetailService cargoDetailService)
        {
            _cargoDetailService = cargoDetailService;
        }
        [HttpGet]
        public async Task<IActionResult> CargoDetailList()
        {
            var values = await _cargoDetailService.TGetAllAsync();
            return Ok(values);
        }
        [HttpPost]
        public async Task<IActionResult> CreateCargoDetail(CreateCargoDetailDto createCargoDetailDto)
        {
            CargoDetail cargoDetail = new()
            {
                Barcode = createCargoDetailDto.Barcode,
                CargoCompanyId = createCargoDetailDto.CargoCompanyId,
                ReveiverCustomer = createCargoDetailDto.ReveiverCustomer,
                SenderCustomer = createCargoDetailDto.SenderCustomer
            };
            await _cargoDetailService.TInsertAsync(cargoDetail);
            return Ok("Kargo Detayı Başarıyla Eklendi !");
        }
        [HttpDelete]
        public async Task<IActionResult> RemoveCargoDetail(int id)
        {
            await _cargoDetailService.TDeleteAsync(id);
            return Ok("Kargo Detayı Başarıyla Silindi !");
        }
        [HttpPut]
        public async Task<IActionResult> UpdateCargoDetail(UpdateCargoDetailDto updateCargoDetailDto)
        {
            CargoDetail cargoDetail = new()
            {
                Barcode=updateCargoDetailDto.Barcode,
                CargoCompanyId = updateCargoDetailDto.CargoCompanyId,
                ReveiverCustomer = updateCargoDetailDto.ReveiverCustomer,
                SenderCustomer= updateCargoDetailDto.SenderCustomer
            };
            await _cargoDetailService.TUpdateAsync(cargoDetail);
            return Ok("Kargo Detayı Başarıyla Silindi !");
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCargoDetailById(int id)
        {
            var value = _cargoDetailService.TGetByIdAsync(id);
            return Ok(value);
        }
    }
}
