using Microsoft.AspNetCore.Mvc;
using MultiShop.DtoLayer.IdentityDtos.UserDtos;
using MultiShop.DtoLayer.MessageDtos;
using MultiShop.WebUI.Models;
using MultiShop.WebUI.Services.Interfaces;
using MultiShop.WebUI.Services.MessageServices;
using MultiShop.WebUI.Services.UserIdentityService;

namespace MultiShop.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class MessageController : Controller
    {
        private readonly IMessageService _messageService;
        private readonly IUserService _userService;
        private readonly IUserIdentityService _userIdentityService;
        private readonly ILogger<MessageController> _logger;
        public MessageController(IMessageService messageService, IUserService userService, ILogger<MessageController> logger, IUserIdentityService userIdentityService)
        {
            _messageService = messageService;
            _userService = userService;
            _logger = logger;
            _userIdentityService = userIdentityService;
        }

        public async Task<IActionResult> ReceivedMessageList()
        {
            ViewBag.v1 = "Anasayfa";
            ViewBag.v2 = "Gelen Mesajlar";
            ViewBag.v3 = "Gelen Mesaj Listesi";
            ViewBag.baslik = "Mesaj İşlemleri";

            var user = await _userService.GetUserInfo();
            var values = await _messageService.GetInBoxMessageAsync(user.Id);

            var senderList = new List<ResultUserDto>();

            foreach (var item in values)
            {
                var senderUser = await _userIdentityService.GetUserById(item.ReceivedId);
                
                senderList.Add(senderUser);
            }

            var headerAdminViewModel = new HeaderAdminViewModel
            {
                SenderUsers = senderList,
                resultInBoxMessageDto = values
            };

            if (values != null && senderList != null)
            {
                return View(headerAdminViewModel);
            }

            return View();
        }
        public async Task<IActionResult> ReceivedMessageDetail(int id)
        {
            ViewBag.v1 = "Anasayfa";
            ViewBag.v2 = "Gelen Mesajlar";
            ViewBag.v3 = "Gelen Mesaj Detayı";
            ViewBag.baslik = "Mesaj İşlemleri";

            var value = await _messageService.GetByIdMessageAsync(id);

            var senderUser = await _userIdentityService.GetUserById(value.SenderId);

            ViewBag.SenderUserName = senderUser.name + " " + senderUser.surname;

            if(value != null)
            {
                return View(value);
            }
            return View();
        }
        
        [HttpPost]
        public async Task<IActionResult> CreateMessage(CreateMessageDto createMessageDto)
        {
            try
            {
                // 3. Ek iş kuralları
                if (string.IsNullOrEmpty(createMessageDto.MessageDetail))
                {
                    TempData["ErrorMessage"] = "Mesaj içeriği boş olamaz.";
                    _logger.LogWarning("Hata Mesajı: {ErrorMessage}", TempData["ErrorMessage"]);
                    return View(createMessageDto);
                }

                // 4. Mesaj oluşturma işlemi
                await _messageService.CreateMessageAsync(createMessageDto);

                // 5. Başarılı işlem sonrası yönlendirme
                TempData["SuccessMessage"] = "Mesaj başarıyla oluşturuldu.";
                _logger.LogInformation("Başarı Mesajı: {SuccessMessage}", TempData["SuccessMessage"]);
                return Redirect("/Admin/Message/ReceivedMessageList");
            }
            catch (Exception ex)
            {
                // 6. Hata yakalama ve kullanıcıya bilgi verme
                TempData["ErrorMessage"] = $"Mesaj oluşturulurken bir hata oluştu: {ex.Message}";
                _logger.LogWarning("Hata Mesajı: {ErrorMessage}", TempData["ErrorMessage"]);
                return View(createMessageDto);
            }
        }

        
        public async Task<IActionResult> DeleteReceivedMessage(int id)
        {
            await _messageService.DeleteMessageAsync(id);
            return Redirect("/Admin/Message/ReceivedMessageList");
        }

    }
}
