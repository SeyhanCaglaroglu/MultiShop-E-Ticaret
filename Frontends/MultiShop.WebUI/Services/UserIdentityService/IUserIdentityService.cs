using MultiShop.DtoLayer.IdentityDtos.UserDtos;

namespace MultiShop.WebUI.Services.UserIdentityService
{
    public interface IUserIdentityService
    {
        Task<List<ResultUserDto>> GetAllUserList();
        Task<ResultUserDto> GetUserById(string id);
        Task<string> GetUserRole();
    }
}
