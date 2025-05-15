using MultiShop.DtoLayer.CargoDtos.CargoCustomerDtos;

namespace MultiShop.WebUI.Services.CargoServices.CargoCustomerServices
{
    public class CargoCustomerService : ICargoCustomerService
    {
        private readonly HttpClient _httpClient;

        public CargoCustomerService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<GetCargoCustomerById> GetCargoCustomerByCustomerId(string id)
        {
            var responseMessage = await _httpClient.GetAsync("CargoCustomers/GetCargoCustomerByUserId?id=" + id);
            var value = await responseMessage.Content.ReadFromJsonAsync<GetCargoCustomerById>();
            return value;
        }

        public async Task UpdateCargoCustomer(UpdateCargoCustomerDto updateCargoCustomerDto)
        {
            await _httpClient.PutAsJsonAsync<UpdateCargoCustomerDto>("CargoCustomers", updateCargoCustomerDto);

        }
    }
}
