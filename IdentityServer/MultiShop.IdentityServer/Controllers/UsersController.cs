using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MultiShop.IdentityServer.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using static IdentityServer4.IdentityServerConstants;

namespace MultiShop.IdentityServer.Controllers
{
    [Authorize(LocalApi.PolicyName)]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UsersController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
        [HttpGet("GetUser")]
        public async Task<IActionResult> GetUser()
        {
            var userClaim = User.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Sub);
            var user = await _userManager.FindByIdAsync(userClaim.Value);
            return Ok(new
            {
                Id = user.Id,
                Email = user.Email,
                UserName = user.UserName,
                Name = user.Name,
                Surname = user.Surname

            });
        }
        [HttpGet("GetAllUserList")]
        public async Task<IActionResult> GetAllUserList()
        {
            var users = await _userManager.Users.ToListAsync();
            return Ok(users);
        }
        [HttpGet("GetUserById/{id}")]
        public async Task<IActionResult> GetUserById(string id)
        {
            // Kullanıcıyı ID'ye göre sorgula
            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == id);

            if (user == null)
            {
                // Kullanıcı bulunamazsa, 404 Not Found döndür
                return NotFound(new { message = "Kullanıcı bulunamadı." });
            }

            // Kullanıcı bilgilerini döndür
            return Ok(user);
        }

        [HttpGet("GetUserRole")]
        public async Task<IActionResult> GetUserRole()
        {
            var userClaim = User.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Sub);
            if (userClaim == null)
            {
                return Unauthorized(new { message = "Kullanıcı kimliği doğrulanamadı." });
            }

            var user = await _userManager.FindByIdAsync(userClaim.Value);
            if (user == null)
            {
                return NotFound(new { message = "Kullanıcı bulunamadı." });
            }

            var roles = await _userManager.GetRolesAsync(user);

            // İlk rolü döndür
            return Ok(roles.FirstOrDefault());
        }



    }
}
