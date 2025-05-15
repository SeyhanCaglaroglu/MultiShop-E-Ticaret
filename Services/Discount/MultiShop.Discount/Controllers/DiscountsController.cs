using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MultiShop.Discount.Dtos;
using MultiShop.Discount.Services;

namespace MultiShop.Discount.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class DiscountsController : ControllerBase
    {
        private readonly IDiscountService _discountService;

        public DiscountsController(IDiscountService discountService)
        {
            _discountService = discountService;
        }
        [HttpGet]
        public async Task<IActionResult> CouponList()
        {
            var values = await _discountService.GetAllCouponAsync();
            return Ok(values);
        }
        [HttpPost]
        public async Task<IActionResult> CreateCoupon(CreateCouponDto createCouponDto)
        {
            await _discountService.CreateCouponAsync(createCouponDto);
            return Ok("Kupon Başarı ile Eklendi !");
        }
        [HttpPut]
        public async Task<IActionResult> UpdateCoupon(UpdateCouponDto updateCouponDto)
        {
            await _discountService.UpdateCouponAsync(updateCouponDto);
            return Ok("Kupon Başarı ile güncellendi !");
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteCoupon(int id)
        {
            await _discountService.DeleteCouponAsync(id);
            return Ok("Kupon Başarı ile Silindi !");
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCouponById(int id)
        {
            var value = await _discountService.GetByIdCouponAsync(id);
            return Ok(value);
        }
        [HttpGet("GetCodeDetailByCode")]
        public async Task<IActionResult> GetCodeDetailByCode(string code)
        {
            var value = await _discountService.GetCodeDetailByCodeAsync(code);
            return Ok(value);
        }
        [HttpGet("GetDiscountCouponCount")]
        public async Task<IActionResult> GetDiscountCouponCount()
        {
            var value = await _discountService.GetDiscountCouponCount();
            return Ok(value);
        }
    }
}
