using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using MultiShop.WebUI.Models;
using MultiShop.WebUI.Services.Interfaces;
using MultiShop.WebUI.Services.UserIdentityService;

namespace MultiShop.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class MailController : Controller
    {
        private readonly IUserIdentityService _userIdentityService;
        private readonly IUserService _userService;
        public MailController(IUserIdentityService userIdentityService, IUserService userService)
        {
            _userIdentityService = userIdentityService;
            _userService = userService;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.v1 = "Anasayfa";
            ViewBag.v2 = "Kullanıcılar";
            ViewBag.v3 = "Kullanıcı Listesi";
            ViewBag.baslik = "Mail İşlemleri";

            var users = await _userIdentityService.GetAllUserList();
            return View(users);
        }
        public async Task<IActionResult> SendMailToAUser(string id)
        {
            var user = await _userIdentityService.GetUserById(id);

            var mailViewModel = new MailViewModel
            {
                ResultUserDto = user,

            };

            return View(mailViewModel);
        }
        [HttpPost]
        public IActionResult SendMailToAUser(string ReceiveMail, string Subject, string Content)
        {
            MimeMessage mimeMessage =  new MimeMessage();

            MailboxAddress mailboxAddressFrom = new MailboxAddress("MultiShop", "example@gmail.com");
            mimeMessage.From.Add(mailboxAddressFrom);

            MailboxAddress mailboxAddressTo = new MailboxAddress("User",ReceiveMail);
            mimeMessage.To.Add(mailboxAddressTo);

            var bodyBuilder = new BodyBuilder();
            bodyBuilder.TextBody = Content;

            mimeMessage.Body = bodyBuilder.ToMessageBody();
            mimeMessage.Subject = Subject;

            SmtpClient smtpClient = new SmtpClient();
            smtpClient.Connect("smtp.gmail.com",587,false);
            smtpClient.Authenticate("example@gmail.com", "example");
            smtpClient.Send(mimeMessage);
            smtpClient.Disconnect(true);

            return Redirect("/Admin/Mail/Index");
        }
        public async Task<IActionResult> SendMailToAllUser()
        {
            var users = await _userIdentityService.GetAllUserList();

            var mailViewModel = new MailViewModel
            {
                ResultUsers = users
            };

            return View(mailViewModel);
        }
        [HttpPost]
        public IActionResult SendMailToAllUser(string ReceiveMailList, string Subject, string Content)
        {
            var emailList = ReceiveMailList.Split(',').Select(x => x.Trim()).ToList();

            foreach (var email in emailList)
            {
                MimeMessage mimeMessage = new MimeMessage();

                MailboxAddress mailboxAddressFrom = new MailboxAddress("MultiShop", "example@gmail.com");
                mimeMessage.From.Add(mailboxAddressFrom);

                MailboxAddress mailboxAddressTo = new MailboxAddress("User", email);
                mimeMessage.To.Add(mailboxAddressTo);

                var bodyBuilder = new BodyBuilder();
                bodyBuilder.TextBody = Content;

                mimeMessage.Body = bodyBuilder.ToMessageBody();
                mimeMessage.Subject = Subject;

                using (var smtpClient = new SmtpClient())
                {
                    smtpClient.Connect("smtp.gmail.com", 587, false);
                    smtpClient.Authenticate("example@gmail.com", "example");
                    smtpClient.Send(mimeMessage);
                    smtpClient.Disconnect(true);
                }
            }

            return Redirect("/Admin/Mail/Index");
        }

    }
}
