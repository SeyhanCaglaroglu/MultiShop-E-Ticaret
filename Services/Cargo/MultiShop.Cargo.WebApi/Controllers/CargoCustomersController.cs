using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MultiShop.Cargo.BusinessLayer.Abstract;
using MultiShop.Cargo.DataAccessLayer.Abstract;
using MultiShop.Cargo.DtoLayer.Dtos.CargoCustomerDto;
using MultiShop.Cargo.EntityLayer.Concrete;

namespace MultiShop.Cargo.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CargoCustomersController : ControllerBase
    {
        private readonly ICargoCustomerService _cargoCustomerService;

        public CargoCustomersController(ICargoCustomerService cargoCustomerService)
        {
            _cargoCustomerService = cargoCustomerService;
        }
        [HttpGet]
        public async Task<IActionResult> CargoCustomerList()
        {
            var values = await _cargoCustomerService.TGetAllAsync();
            return Ok(values);
        }
        [HttpPost]
        public async Task<IActionResult> CreateCargoCustomer(CreateCargoCustomerDto createCargoCustomerDto)
        {
            CargoCustomer cargoCustomer = new()
            {
                Address = createCargoCustomerDto.Address,
                City = createCargoCustomerDto.City,
                District = createCargoCustomerDto.District,
                Email = createCargoCustomerDto.Email,
                Name = createCargoCustomerDto.Name,
                Surname = createCargoCustomerDto.Surname,
                Phone = createCargoCustomerDto.Phone,
                UserCustomerId = createCargoCustomerDto.UserCustomerId

            };
            await _cargoCustomerService.TInsertAsync(cargoCustomer);
            return Ok("Kargo Müşteri Başarıyla Eklendi !");
        }
        [HttpDelete]
        public async Task<IActionResult> RemoveCargoCustomer(int id)
        {
            await _cargoCustomerService.TDeleteAsync(id);
            return Ok("Kargo Müşteri Başarıyla Silindi !");
        }
        [HttpPut]
        public async Task<IActionResult> UpdateCargoCustomer(UpdateCargoCustomerDto updateCargoCustomerDto)
        {
            CargoCustomer cargoCustomer = new()
            {
                Address = updateCargoCustomerDto.Address,
                City = updateCargoCustomerDto.City,
                CargoCustomerId = updateCargoCustomerDto.CargoCustomerId,
                District = updateCargoCustomerDto.District,
                Email = updateCargoCustomerDto.Email,
                Name = updateCargoCustomerDto.Name,
                Phone = updateCargoCustomerDto.Phone,
                Surname = updateCargoCustomerDto.Surname,
                UserCustomerId = updateCargoCustomerDto.UserCustomerId
            };

            await _cargoCustomerService.TUpdateAsync(cargoCustomer);
            return Ok("Kargo Müşteri Başarıyla Güncellendi !");
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCargoCustomerById(int id)
        {
            var value = await _cargoCustomerService.TGetByIdAsync(id);
            return Ok(value);
        }
        [HttpGet("GetCargoCustomerByUserId")]
        public IActionResult GetCargoCustomerByUserId(string id)
        {
            var value = _cargoCustomerService.TGetCargoCustomerById(id);
            return Ok(value);
        }
    }
}
