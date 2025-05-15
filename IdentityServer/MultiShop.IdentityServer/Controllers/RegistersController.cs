using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MultiShop.IdentityServer.Dtos;
using MultiShop.IdentityServer.Models;
using System.Linq;
using System;
using System.Threading.Tasks;
using static IdentityServer4.IdentityServerConstants;

namespace MultiShop.IdentityServer.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class RegistersController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public RegistersController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpPost]
        public async Task<IActionResult> UserRegister([FromBody] UserRegisterDto userRegisterDto)
        {
            var values = new ApplicationUser
            {
                UserName = userRegisterDto.UserName,
                Email = userRegisterDto.Email,
                Name = userRegisterDto.Name,
                Surname = userRegisterDto.Surname,
            };

            // Kullanıcıyı oluşturma işlemi
            var result = await _userManager.CreateAsync(values, userRegisterDto.Password);

            // Başarı durumunu kontrol et
            if (result.Succeeded)
            {
                return Ok("Kullanıcı Başarıyla Oluşturuldu !");
            }
            else
            {
                // Hata mesajlarını bir liste olarak al
                var errors = result.Errors.Select(e => e.Description);
                // Hataları logla
                Console.WriteLine($"Kayıt hataları: {string.Join(", ", errors)}");

                // Hata mesajlarını döndür
                return BadRequest(new { Errors = errors });
            }
        }
    }

}
