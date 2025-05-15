using System.Threading.Tasks;

namespace MultiShop.IdentityServer.Services
{
    public interface IGetUserRoleService
    {
        Task<string> GetUserRole(string userName);
    }
}
