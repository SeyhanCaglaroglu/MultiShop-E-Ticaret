using MultiShop.DtoLayer.OrderDtos.AdressDto;

namespace MultiShop.WebUI.Services.OrderServices.AdressService
{
    public class AdressService : IAdressService
    {
        private readonly HttpClient _httpClient;

        public AdressService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task CreateAdressAsync(CreateAdressDto createAdressDto)
        {
            await _httpClient.PostAsJsonAsync<CreateAdressDto>("adresses",createAdressDto);
        }
    }
}
