using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MultiShop.Catalog.Dtos.ProductDtos;
using MultiShop.Catalog.Services.ProductServices;

namespace MultiShop.Catalog.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }
        [HttpGet]
        public async Task<IActionResult> ProductList()
        {
            var values = await _productService.GetAllProductAsync();
            return Ok(values);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(string id)
        {
            var value = await _productService.GetByIdProductAsync(id);
            return Ok(value);
        }
        [HttpPost]
        public async Task<IActionResult> CreateProduct(CreateProductDto createProductDto)
        {
            await _productService.CreateProductAsync(createProductDto);
            return Ok("Ürün Başarıyla Eklendi");
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteProduct(string id)
        {
            await _productService.DeleteProductAsync(id);
            return Ok("Ürün Başarıyla Silindi");
        }
        [HttpPut]
        public async Task<IActionResult> UpdateProduct(UpdateProductDto updateProductDto)
        {
            await _productService.UpdateProductAsync(updateProductDto);
            return Ok("Ürün Başarıyla Güncellendi");
        }
        [HttpGet("ProductListWithCategory")]
        public async Task<IActionResult> ProductListWithCategory()
        {
            var values = await _productService.GetProductWithCategoryAsync();
            return Ok(values);
        }
        [HttpGet("ProductListWithCategoryByCategoryId/{id}")]
        public async Task<IActionResult> ProductListWithCategoryByCategoryId(string id)
        {
            var values = await _productService.GetProductWithCategoryByCategoryIdAsync(id);
            return Ok(values);
        }
        [HttpGet("GetProductCountByCategoryId/{id}")]
        public async Task<IActionResult> GetProductCountByCategoryId(string id)
        {
            var values = await _productService.GetProductCountByCategoryId(id);
            return Ok(values);
        }
        [HttpGet("GetProductsFilteredByPrice")]
        public async Task<IActionResult> GetProductsFilteredByPrice(string range)
        {
            var values = await _productService.GetProductsFilteredByPrice(range);
            return Ok(values);
        }
        [HttpGet("GetProductsFilteredByPriceWithCategory")]
        public async Task<IActionResult> GetProductsFilteredByPriceWithCategory(string id,string range)
        {
            var values = await _productService.GetProductsFilteredByPriceWithCategory(id,range);
            return Ok(values);
        }
        [HttpGet("GetProductsWithSearch")]
        public async Task<IActionResult> GetProductsWithSearch(string productName)
        {
            var values = await _productService.GetProductsWithSearch(productName);
            return Ok(values);
        }
        [HttpGet("GetProductsByCategoryId")]
        public async Task<IActionResult> GetProductsByCategoryId(string id)
        {
            var values = await _productService.GetProductsByCategoryId(id);
            return Ok(values);
        }
    }
}
