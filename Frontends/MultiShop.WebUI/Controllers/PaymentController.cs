using Microsoft.AspNetCore.Mvc;

namespace MultiShop.WebUI.Controllers
{
    public class PaymentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Index(string name)
        {
            if (!string.IsNullOrEmpty(name))
            {
                TempData["SaleSuccessMessage"] = $"Sayın {name} Satın Alma İşleminiz Başarı İle Gerçekleşmiştir !";
            }

            return RedirectToAction("Index", "Default");
        }
    }
}
