using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MultiShop.DtoLayer.CatalogDtos.CategoryDtos;
using MultiShop.DtoLayer.CatalogDtos.ProductDtos;
using MultiShop.WebUI.Services.CatalogServices.CategoryServices;
using MultiShop.WebUI.Services.CatalogServices.ProductServices;
using Newtonsoft.Json;
using System.Text;

namespace MultiShop.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {

        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;

        public ProductController(IProductService productService, ICategoryService categoryService)
        {

            _productService = productService;
            _categoryService = categoryService;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.v1 = "Anasayfa";
            ViewBag.v2 = "Ürünler";
            ViewBag.v3 = "Ürün Listesi";
            ViewBag.baslik = "Ürün İşlemleri";

            var values = await _productService.GetAllProductAsync();

            return View(values);
        }
        public async Task<IActionResult> ProductListWithCategory()
        {
            ViewBag.v1 = "Anasayfa";
            ViewBag.v2 = "Ürünler";
            ViewBag.v3 = "Ürün Listesi";
            ViewBag.baslik = "Ürün İşlemleri";

            var values = await _productService.GetProductWithCategoryAsync();

            return View(values);
        }
        [HttpGet]
        public async Task<IActionResult> CreateProduct()
        {
            ViewBag.v1 = "Anasayfa";
            ViewBag.v2 = "Ürünler";
            ViewBag.v3 = "Ürün Ekle";
            ViewBag.baslik = "Ürün İşlemleri";


            var values = await _categoryService.GetAllCategoriesAsync();

            List<SelectListItem> categoryValues = (from item in values select new SelectListItem
                                                   {
                                                       Text = item.CategoryName,
                                                       Value = item.CategoryId
                                                   }).ToList();
            ViewBag.CategoryValues = categoryValues;

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateProduct(CreateProductDto createProductDto, IFormFile ImageURL)
        {
            try
            {
                if (ImageURL != null && ImageURL.Length > 0)
                {
                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", ImageURL.FileName);

                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        ImageURL.CopyTo(stream);
                    }
                    createProductDto.ProductImageUrl = "/images/" + ImageURL.FileName;
                }

                await _productService.CreateProductAsync(createProductDto);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Hata meydana geldi: {ex.Message}");
                Console.WriteLine($"Detaylar: {ex.StackTrace}");
            }


            return Redirect("/Admin/Product/ProductListWithCategory");

        }
        public async Task<IActionResult> DeleteProduct(string id)
        {
            await _productService.DeleteProductAsync(id);
            return Redirect("/Admin/Product/ProductListWithCategory");

        }
        public async Task<IActionResult> UpdateProduct(string id)
        {
            ViewBag.v1 = "Anasayfa";
            ViewBag.v2 = "Ürünler";
            ViewBag.v3 = "Ürünü Güncelle";
            ViewBag.baslik = "Ürün İşlemleri";

            var productValue = await _productService.GetByIdProductAsync(id);
            var categoryValues = await _categoryService.GetAllCategoriesAsync();


            List<SelectListItem> categoryValuesList = (from item in categoryValues
                                                       select new SelectListItem
                                                       {
                                                           Text = item.CategoryName,
                                                           Value = item.CategoryId
                                                       }).ToList();
            ViewBag.CategoryValues = categoryValuesList;



            return View(productValue);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateProduct(UpdateProductDto updateProductDto)
        {
            await _productService.UpdateProductAsync(updateProductDto);
            return Redirect("/Admin/Product/ProductListWithCategory");

        }
    }
}
