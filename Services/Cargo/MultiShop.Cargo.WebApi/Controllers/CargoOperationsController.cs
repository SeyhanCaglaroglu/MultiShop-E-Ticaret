using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MultiShop.Cargo.BusinessLayer.Abstract;
using MultiShop.Cargo.DtoLayer.Dtos.CargoOperationDto;
using MultiShop.Cargo.EntityLayer.Concrete;

namespace MultiShop.Cargo.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CargoOperationsController : ControllerBase
    {
        private readonly ICargoOperationService _cargoOperationService;

        public CargoOperationsController(ICargoOperationService cargoOperationService)
        {
            _cargoOperationService = cargoOperationService;
        }
        [HttpGet]
        public async Task<IActionResult> CargoOperationList()
        {
            var values = await _cargoOperationService.TGetAllAsync();
            return Ok(values);
        }
        [HttpPost]
        public async Task<IActionResult> CreateCargoOperation(CreateCargoOperationDto createCargoOperationDto)
        {
            CargoOperation cargoOperation = new()
            {
                Barcode = createCargoOperationDto.Barcode,
                Description = createCargoOperationDto.Description,
                OperationDate = createCargoOperationDto.OperationDate
            };
            await _cargoOperationService.TInsertAsync(cargoOperation);
            return Ok("Kargo Operasyonu Başarıyla Oluşturuldu !");
        }
        [HttpDelete]
        public async Task<IActionResult> RemoveCargoOperation(int id)
        {
            await _cargoOperationService.TDeleteAsync(id);
            return Ok("Kargo Operasyonu Başarıyla Silindi !");
        }
        [HttpPut]
        public async Task<IActionResult> UpdateCargoOperation(UpdateCargoOperationDto updateCargoOperationDto)
        {
            CargoOperation cargoOperation = new()
            {
                Barcode= updateCargoOperationDto.Barcode,
                Description= updateCargoOperationDto.Description,
                CargoOperationId = updateCargoOperationDto.CargoOperationId,
                OperationDate = updateCargoOperationDto.OperationDate
            };
            await _cargoOperationService.TUpdateAsync(cargoOperation);
            return Ok("Kargo Operasyonu Başarıyla Güncellendi !");
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCargoOperationById(int id)
        {
            var value = await _cargoOperationService.TGetByIdAsync(id);
            return Ok(value);
        }
    }
}
