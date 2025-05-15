using MultiShop.DtoLayer.IdentityDtos.UserDtos;
using MultiShop.DtoLayer.MessageDtos;
using MultiShop.DtoLayer.NotificationDtos;

namespace MultiShop.WebUI.Models
{
    public class HeaderAdminViewModel
    {
        public List<ResultInBoxMessageDto> resultInBoxMessageDto {  get; set; }
        public List<ResultNotificationDto> resultNotificationDto { get; set; }
        public List<ResultUserDto> SenderUsers { get; set; }
    }
}
