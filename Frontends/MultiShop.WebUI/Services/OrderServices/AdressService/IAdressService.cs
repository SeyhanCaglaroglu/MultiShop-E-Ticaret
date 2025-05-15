using MultiShop.DtoLayer.OrderDtos.AdressDto;

namespace MultiShop.WebUI.Services.OrderServices.AdressService
{
    public interface IAdressService
    {
        //Task<List<ResultAboutDto>> GetAllAboutsAsync();
        Task CreateAdressAsync(CreateAdressDto createAdressDto);

        //Task UpdateAboutAsync(UpdateAboutDto updateAboutDto);
        //Task DeleteAboutAsync(string id);
        //Task<UpdateAboutDto> GetByIdAboutAsync(string id);
    }
}
