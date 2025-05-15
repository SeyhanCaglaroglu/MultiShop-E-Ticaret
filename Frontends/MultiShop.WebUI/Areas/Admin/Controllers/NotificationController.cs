using Microsoft.AspNetCore.Mvc;
using MultiShop.WebUI.Services.NotificationServices;

namespace MultiShop.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class NotificationController : Controller
    {
        private readonly INotificationService _notificationService;

        public NotificationController(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        public async Task<IActionResult> NotificationList()
        {
            ViewBag.v1 = "Anasayfa";
            ViewBag.v2 = "Bildirimler";
            ViewBag.v3 = "Bildirim Listesi";
            ViewBag.baslik = "Bildirim İşlemleri";

            var values = await _notificationService.GetAllNotificationsAsync();

            if (values != null && values.Any())
            {
                return View(values);
            }
            return View();
        }
        
        public async Task<IActionResult> DeleteNotification(int id)
        {
             await _notificationService.DeleteNotificationAsync(id);
            return Redirect("/Admin/Notification/NotificationList");
        }
        
        public async Task<IActionResult> DeleteAllNotification()
        {
            await _notificationService.DeleteAllNotificationAsync();
            return Redirect("/Admin/Notification/NotificationList");
        }
    }
}
