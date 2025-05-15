using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MultiShop.DtoLayer.CatalogDtos.ProductDetailDtos;
using MultiShop.WebUI.Services.CatalogServices.ProductDetailService;
using Newtonsoft.Json;
using System.Text;

namespace MultiShop.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductDetailController : Controller
    {
        private readonly IProductDetailService _productDetailService;

        public ProductDetailController(IProductDetailService productDetailService)
        {
            _productDetailService = productDetailService;
        }

        public async Task<IActionResult> UpdateProductDetail(string id)
        {
            ViewBag.v1 = "Anasayfa";
            ViewBag.v2 = "Ürünler";
            ViewBag.v3 = "Ürün Detayı Güncelleme";
            ViewBag.baslik = "Ürün Detay İşlemleri";

            GetByIdProductDetailDto value = null;

            try
            {
                // Servisten veri çekiliyor
                value = await _productDetailService.GetByProductIdProductDetailAsync(id);

                // Eğer value null ise yeni bir nesne oluşturuluyor ve ProductId atanıyor
                if (value == null)
                {
                    value = new GetByIdProductDetailDto { ProductId = id };
                }
            }
            catch (JsonException jsonEx)
            {
                // JSON hatası loglanıyor
                Console.WriteLine($"JSON Hatası: {jsonEx.Message}");
                ModelState.AddModelError("", "Bir hata oluştu. Lütfen tekrar deneyiniz.");
            }
            catch (Exception ex)
            {
                // Diğer beklenmeyen hatalar loglanıyor
                Console.WriteLine($"Beklenmeyen Hata: {ex.Message}");
                ModelState.AddModelError("", "Bir hata oluştu. Lütfen tekrar deneyiniz.");
            }

            // Hata olsun ya da olmasın 'value' ile View döndürülüyor
            return View(value);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateProductDetail(string ProductId, string ProductDetailId, string ProductDescription, string ProductInfo)
        {
            if (string.IsNullOrEmpty(ProductDetailId))
            {
                CreateProductDetailDto createProductDetailDto = new CreateProductDetailDto();
                createProductDetailDto.ProductId = ProductId;
                createProductDetailDto.ProductDescription = ProductDescription;
                createProductDetailDto.ProductInfo = ProductInfo;
                await _productDetailService.CreateProductDetailAsync(createProductDetailDto);
            }
            else
            {
                UpdateProductDetailDto updateProductDetailDto = new UpdateProductDetailDto();
                updateProductDetailDto.ProductId = ProductId;
                updateProductDetailDto.ProductDetailId = ProductDetailId;
                updateProductDetailDto.ProductInfo = ProductInfo;
                updateProductDetailDto.ProductDescription= ProductDescription;
                await _productDetailService.UpdateProductDetailAsync(updateProductDetailDto);
            }
            
            return Redirect("/Admin/Product/ProductListWithCategory");

        }
    }
}
