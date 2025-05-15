using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MultiShop.DtoLayer.CatalogDtos.CategoryDtos;
using MultiShop.WebUI.Services.CatalogServices.CategoryServices;
using Newtonsoft.Json;
using System.Text;

namespace MultiShop.WebUI.Areas.Admin.Controllers
{
    [Authorize]
    [Area("Admin")]
    public class CategoryController : Controller
    {
        
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.v1 = "Anasayfa";
            ViewBag.v2 = "Kategoriler";
            ViewBag.v3 = "Kategori Listesi";
            ViewBag.baslik = "Kategori İşlemleri";

            var values = await _categoryService.GetAllCategoriesAsync();
            return View(values);
        }
        [HttpGet]
        public IActionResult CreateCategory()
        {
            ViewBag.v1 = "Anasayfa";
            ViewBag.v2 = "Kategoriler";
            ViewBag.v3 = "Kategori Ekle";
            ViewBag.baslik = "Kategori İşlemleri";
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateCategory(CreateCategoryDto createCategoryDto,IFormFile ImageURL)
        {
            try
            {
                if (ImageURL !=null && ImageURL.Length > 0)
                {
                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", ImageURL.FileName);

                    using(var stream = new FileStream(path,FileMode.Create))
                    {
                        ImageURL.CopyTo(stream);
                    }
                    createCategoryDto.ImageUrl = "/images/" + ImageURL.FileName;
                }

                await _categoryService.CreateCategoryAsync(createCategoryDto);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Hata meydana geldi: {ex.Message}");
                Console.WriteLine($"Detaylar: {ex.StackTrace}");
            }

            
            return Redirect("/Admin/Category/Index");

        }
        public async Task<IActionResult> DeleteCategory(string id)
        {
            await _categoryService.DeleteCategoryAsync(id);
            return Redirect("/Admin/Category/Index");

        }
        [HttpGet]
        public async Task<IActionResult> UpdateCategory(string id)
        {
            ViewBag.v1 = "Anasayfa";
            ViewBag.v2 = "Kategoriler";
            ViewBag.v3 = "Kategori Güncelle";
            ViewBag.baslik = "Kategori İşlemleri";

            var values = await _categoryService.GetByIdCategoryAsync(id);

            return View(values);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateCategory(UpdateCategoryDto updateCategoryDto, IFormFile ImageURL)
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
                    updateCategoryDto.ImageUrl = "/images/" + ImageURL.FileName;
                }

                await _categoryService.UpdateCategoryAsync(updateCategoryDto);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Hata meydana geldi: {ex.Message}");
                Console.WriteLine($"Detaylar: {ex.StackTrace}");
            }

            
            return Redirect("/Admin/Category/Index");

        }
    }
}
