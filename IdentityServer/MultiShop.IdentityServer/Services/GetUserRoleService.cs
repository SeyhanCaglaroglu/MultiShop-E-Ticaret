using Microsoft.AspNetCore.Identity;
using MultiShop.IdentityServer.Models;
using System.Linq;
using System.Threading.Tasks;

namespace MultiShop.IdentityServer.Services
{
    public class GetUserRoleService : IGetUserRoleService
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public GetUserRoleService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<string> GetUserRole(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);
            if (user == null)
            {
                return null;
            }
            var roles = await _userManager.GetRolesAsync(user);
            return roles.FirstOrDefault();
        }
    }
}
