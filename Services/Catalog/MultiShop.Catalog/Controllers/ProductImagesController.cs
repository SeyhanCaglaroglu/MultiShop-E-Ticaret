using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MultiShop.Catalog.Dtos.ProductImageDtos;
using MultiShop.Catalog.Services.ProductImageServices;

namespace MultiShop.Catalog.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductImagesController : ControllerBase
    {
        private readonly IProductImageService _productImageService;
        public ProductImagesController(IProductImageService productImageService)
        {
            _productImageService = productImageService;
        }
        [HttpGet]
        public async Task<IActionResult> ProductImagesList()
        {
            var values = await _productImageService.GetAllProductImageAsync();
            return Ok(values);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductImagesById(string id)
        {
            var value = await _productImageService.GetByIdProductImageAsync(id);

            // Eğer gelen değer null ise, hata mesajı ile birlikte 404 döndürebiliriz
            if (value == null)
            {
                return NotFound(new { message = "Ürün resmi bulunamadı." });
            }

            // Eğer değer geçerliyse, başarılı yanıt döndürüyoruz
            return Ok(value);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProductImages(CreateProductImageDto createProductImageDto)
        {
            await _productImageService.CreateProductImageAsync(createProductImageDto);
            return Ok( "Ürün Resmi Başarıyla Eklendi" );
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteProductImages(string id)
        {
            await _productImageService.DeleteProductImageAsync(id);
            return Ok("Ürün Resmi Başarıyla Silindi");
        }
        [HttpPut]
        public async Task<IActionResult> UpdateProductImages(UpdateProductImageDto updateProductImageDto)
        {
            await _productImageService.UpdateProductImageAsync(updateProductImageDto);
            return Ok("Ürün Resmi Başarıyla Güncellendi");
        }
        [HttpGet("GetProductImageByProductId/{id}")]
        public async Task<IActionResult> GetProductImageByProductId(string id)
        {
            var value = await _productImageService.GetByProductIdProductImageAsync(id);
            return Ok(value);
        }
    }
}
