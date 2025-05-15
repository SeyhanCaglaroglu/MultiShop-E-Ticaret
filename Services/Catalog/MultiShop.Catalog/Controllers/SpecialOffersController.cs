using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MultiShop.Catalog.Dtos.SpecialOfferDtos;
using MultiShop.Catalog.Services.SpecialOfferServices;

namespace MultiShop.Catalog.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class SpecialOffersController : ControllerBase
    {
        private readonly ISpecialOfferService _specialOfferService;
        public SpecialOffersController(ISpecialOfferService specialOfferService)
        {
            _specialOfferService = specialOfferService;
        }
        [HttpGet]
        public async Task<IActionResult> SpecialOfferList()
        {
            var values = await _specialOfferService.GetAllSpecialOffersAsync();
            return Ok(values);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSpecialOfferById(string id)
        {
            var value = await _specialOfferService.GetByIdSpecialOfferAsync(id);
            return Ok(value);
        }
        //GetProductByProductId
        [HttpGet("GetProductByProductId")]
        public async Task<IActionResult> GetProductByProductId(string id)
        {
            var value = await _specialOfferService.GetByIdSpecialOfferAsync(id);
            return Ok(value);
        }
        [HttpPost]
        public async Task<IActionResult> CreateSpecialOffer(CreateSpecialOfferDto createSpecialOfferDto)
        {
            await _specialOfferService.CreateSpecialOfferAsync(createSpecialOfferDto);
            return Ok("Özel Teklif Başarıyla Eklendi!");
        }
        [HttpPut]
        public async Task<IActionResult> UpdateSpecialOffer(UpdateSpecialOfferDto updateSpecialOfferDto)
        {
            await _specialOfferService.UpdateSpecialOfferAsync(updateSpecialOfferDto);
            return Ok("Özel Teklif Başarıyla Güncellendi!");
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteSpecialOffer(string id)
        {
            await _specialOfferService.DeleteSpecialOfferAsync(id);
            return Ok("Özel Teklif Başarıyla Silindi!");
        }
    }
}
