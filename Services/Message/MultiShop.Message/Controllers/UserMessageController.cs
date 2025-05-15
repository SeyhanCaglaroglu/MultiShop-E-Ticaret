using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MultiShop.Message.Dtos;
using MultiShop.Message.Services;

namespace MultiShop.Message.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class UserMessageController : ControllerBase
    {
        private readonly IUserMessageService _userMessageService;

        public UserMessageController(IUserMessageService userMessageService)
        {
            _userMessageService = userMessageService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllMessage()
        {
            var values = await _userMessageService.GetAllMessageAsync();
            return Ok(values);
        }
        [HttpGet("GetMessageInBox")]
        public async Task<IActionResult> GetMessageInBox(string id)
        {
            var values = await _userMessageService.GetInBoxMessageAsync(id);
            return Ok(values);
        }
        [HttpGet("GetMessageSendBox")]
        public async Task<IActionResult> GetMessageSendBox(string id)
        {
            var values = await _userMessageService.GetSendBoxMessageAsync(id);
            return Ok(values);
        }
        [HttpPost]
        public async Task<IActionResult> CreateMessageAsync(CreateMessageDto createMessageDto)
        {
            await _userMessageService.CreateMessageAsync(createMessageDto);
            return Ok("Mesaj Başarıyla Gönderildi");
        }
        [HttpGet("id")]
        public async Task<IActionResult> GetMessageByIdAsync(int id)
        {
            var value = await _userMessageService.GetByIdMessageAsync(id);
            return Ok(value);
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteMessageAsync(int id)
        {
            await _userMessageService.DeleteMessageAsync(id);
            return Ok("Mesaj Başarıyla Silindi");
        }
        [HttpPut]
        public async Task<IActionResult> UpdateMessageAsync(UpdateMessageDto updateMessageDto)
        {
            await _userMessageService.UpdateMessageAsync(updateMessageDto);
            return Ok("Mesaj Başarıyla Güncellendi");
        }
        [HttpGet("GetTotalMessageCount")]
        public async Task<IActionResult> GetTotalMessageCount()
        {
            var value = await _userMessageService.GetTotalMessageCount();
            return Ok(value);
        }
    }
}
