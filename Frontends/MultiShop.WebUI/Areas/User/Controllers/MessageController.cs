using Microsoft.AspNetCore.Mvc;
using MultiShop.DtoLayer.MessageDtos;
using MultiShop.WebUI.Services.Interfaces;
using MultiShop.WebUI.Services.MessageServices;

namespace MultiShop.WebUI.Areas.User.Controllers
{
    [Area("User")]
    public class MessageController : Controller
    {
        private readonly IMessageService _messageService;
        private readonly IUserService _userService;
        public MessageController(IMessageService messageService, IUserService userService)
        {
            _messageService = messageService;
            _userService = userService;
        }

        public async Task<IActionResult> InBox()
        {
            var user = await _userService.GetUserInfo();
            ViewBag.userName = user.Name + " " + user.Surname;
            var InBoxMessages = await _messageService.GetInBoxMessageAsync(user.Id);
            return View(InBoxMessages);
        }
        public async Task<IActionResult> DeleteInBox(int id)
        {
            await _messageService.DeleteMessageAsync(id);
            return Redirect("/User/Message/InBox");
        }
        public async Task<IActionResult> DetailInBox(int id)
        {
            var messageDetail = await _messageService.GetByIdMessageAsync(id);
            return View(messageDetail);
        }
        public async Task<IActionResult> SendBox()
        {
            var user = await _userService.GetUserInfo();
            ViewBag.receivedName = user.Name + " " + user.Surname;
            var SendBoxMessages = await _messageService.GetSendBoxMessageAsync(user.Id);
            return View(SendBoxMessages);
        }
        public async Task<IActionResult> DetailSendBox(int id)
        {
            var messageDetail = await _messageService.GetByIdMessageAsync(id);
            return View(messageDetail);
        }
        public async Task<IActionResult> DeleteSendBox(int id)
        {
            await _messageService.DeleteMessageAsync(id);
            return Redirect("/User/Message/SendBox");
        }
        public IActionResult SendMessage()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SendMessage(CreateMessageDto createMessageDto)
        {
            var user = await _userService.GetUserInfo();
            createMessageDto.SenderId = user.Id;
            createMessageDto.ReceivedId = "example";
            createMessageDto.IsRead = false;
            createMessageDto.MessageDate = DateTime.Now.ToUniversalTime();
            await _messageService.CreateMessageAsync(createMessageDto);
            return Redirect("/User/Message/SendBox");

        }
    }
}
