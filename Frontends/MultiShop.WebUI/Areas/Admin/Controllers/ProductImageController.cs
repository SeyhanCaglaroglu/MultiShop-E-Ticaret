using Microsoft.AspNetCore.Mvc;
using MultiShop.DtoLayer.CatalogDtos.ProductImageDtos;
using MultiShop.WebUI.Services.CatalogServices.ProductImageServices;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;

namespace MultiShop.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductImageController : Controller
    {
        private readonly IProductImageService _productImageService;

        public ProductImageController(IProductImageService productImageService)
        {
            _productImageService = productImageService;
        }

        public async Task<IActionResult> Index(string id)
        {
            ViewBag.v1 = "Anasayfa";
            ViewBag.v2 = "Ürünler";
            ViewBag.v3 = "Ürün Detayı Resim Listesi";
            ViewBag.baslik = "Ürün İşlemleri";

            GetByIdProductImageDto values = null;

            try
            {
                // Service'ten veri çekiliyor
                values = await _productImageService.GetByProductIdProductImageAsync(id);

                // Eğer values null ise yeni bir nesne oluşturuluyor ve ProductId atanıyor
                if (values == null)
                {
                    values = new GetByIdProductImageDto { ProductId = id };
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

            // Hata olsun ya da olmasın 'values' ile View döndürülüyor
            return View(values ?? new GetByIdProductImageDto { ProductId = id });
        }



        [HttpPost]
        public async Task<IActionResult> Index(string ProductId,string? ProductImageId, string Image1,string Image2, string Image3, string Image4)
        {
            

            if (string.IsNullOrEmpty(ProductImageId))
            {
                CreateProductImageDto createProductImageDto = new CreateProductImageDto();
                createProductImageDto.ProductId = ProductId;
                createProductImageDto.Image1 = Image1;
                createProductImageDto.Image2 = Image2;
                createProductImageDto.Image3 = Image3;
                createProductImageDto.Image4 = Image4;
                await _productImageService.CreateProductImageAsync(createProductImageDto);
            } else
            {
                UpdateProductImageDto updateProductImageDto = new UpdateProductImageDto();
                updateProductImageDto.ProductId = ProductId;
                updateProductImageDto.ProductImageId = ProductImageId;
                updateProductImageDto.Image1 = Image1;
                updateProductImageDto.Image2 = Image2;
                updateProductImageDto.Image3 = Image3;
                updateProductImageDto.Image4 = Image4;
                await _productImageService.UpdateProductImageAsync(updateProductImageDto);
            }
            return Redirect("/Admin/Product/ProductListWithCategory");
        }
    }
}
