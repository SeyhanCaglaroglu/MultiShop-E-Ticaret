using Microsoft.AspNetCore.Mvc;
using MultiShop.DtoLayer.IdentityDtos.UserDtos;
using MultiShop.WebUI.Models;
using MultiShop.WebUI.Services.Interfaces;
using MultiShop.WebUI.Services.MessageServices;
using MultiShop.WebUI.Services.NotificationServices;
using MultiShop.WebUI.Services.UserIdentityService;

namespace MultiShop.WebUI.Areas.Admin.ViewComponents.AdminLayoutViewComponents
{
    public class _HeaderAdminLayoutComponentPartial : ViewComponent
    {
        private readonly INotificationService _notificationService;
        private readonly IUserService _userService;
        private readonly IMessageService _messageService;
        private readonly IUserIdentityService _userIdentityService;
        public _HeaderAdminLayoutComponentPartial(INotificationService notificationService, IUserService userService, IMessageService messageService, IUserIdentityService userIdentityService)
        {
            _notificationService = notificationService;
            _userService = userService;
            _messageService = messageService;
            _userIdentityService = userIdentityService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            //receivedMessage
            var user = await _userService.GetUserInfo();
            var receivedMessage = await _messageService.GetInBoxMessageAsync(user.Id);
            ViewBag.receivedMessageCount = receivedMessage.Count();

            //notificationCount
            int NotificationCount = await _notificationService.NotificationCount();
            ViewBag.NotificationCount = NotificationCount;

            //notificationList
            var notifications = await _notificationService.GetAllNotificationsAsync();


            var senderUsers = new List<ResultUserDto>();  // Kullanıcı bilgilerini tutacak bir liste oluştur

            // Her mesaj için gönderen bilgilerini al ve listeye ekle
            foreach (var message in receivedMessage)
            {
                var senderUser = await _userIdentityService.GetUserById(message.SenderId);
                senderUsers.Add(senderUser); // Gönderen kullanıcı bilgilerini ekle
            }

            if (notifications != null && receivedMessage != null)
            {
                var headerAdminViewModel = new HeaderAdminViewModel
                {
                    resultInBoxMessageDto = receivedMessage,
                    resultNotificationDto = notifications,
                    SenderUsers = senderUsers
                };

                return View(headerAdminViewModel);
            }
            return View();
        }
    }
}
