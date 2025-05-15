using MultiShop.DtoLayer.IdentityDtos.UserDtos;

namespace MultiShop.WebUI.Models
{
    public class MailViewModel
    {
        public string ReceiveMail { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }

        public ResultUserDto ResultUserDto { get; set; }
        public List<ResultUserDto> ResultUsers { get; set; }
    }
}
