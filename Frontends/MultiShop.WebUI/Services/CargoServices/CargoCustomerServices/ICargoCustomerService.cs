using MultiShop.DtoLayer.CargoDtos.CargoCustomerDtos;

namespace MultiShop.WebUI.Services.CargoServices.CargoCustomerServices
{
    public interface ICargoCustomerService
    {
        Task<GetCargoCustomerById> GetCargoCustomerByCustomerId(string id);
        Task UpdateCargoCustomer(UpdateCargoCustomerDto updateCargoCustomerDto);
    }
}
