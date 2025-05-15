using Microsoft.AspNetCore.Http.HttpResults;
using MultiShop.DtoLayer.IdentityDtos.UserDtos;

namespace MultiShop.WebUI.Services.UserIdentityService
{
    public class UserIdentityService : IUserIdentityService
    {
        private readonly HttpClient _httpClient;

        public UserIdentityService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<ResultUserDto>> GetAllUserList()
        {
            var responseMessage = await _httpClient.GetAsync("/api/Users/GetAllUserList");
            var values = await responseMessage.Content.ReadFromJsonAsync<List<ResultUserDto>>();
            return values;
        }

        public async Task<ResultUserDto> GetUserById(string id)
        {
            var responseMessage = await _httpClient.GetAsync("/api/Users/GetUserById/" + id);
            var values = await responseMessage.Content.ReadFromJsonAsync<ResultUserDto>();
            return values;
        }

        public async Task<string> GetUserRole()
        {
            var responseMessage = await _httpClient.GetAsync("/api/Users/GetUserRole");
            var value = await responseMessage.Content.ReadAsStringAsync();
            return value;
        }
    }
}
