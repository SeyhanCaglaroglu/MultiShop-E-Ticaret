using Microsoft.AspNetCore.Mvc;
using MultiShop.DtoLayer.CommentDtos;
using MultiShop.WebUI.Services.CommentServices;
using Newtonsoft.Json;
using System.Text;

namespace MultiShop.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CommentController : Controller
    {
        private readonly ICommentService _commentService;

        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.v1 = "Anasayfa";
            ViewBag.v2 = "Yorumlar";
            ViewBag.v3 = "Yorum Listesi";
            ViewBag.baslik = "Yorum İşlemleri";

            var values = await _commentService.GetAllCommentsAsync();

            if (values != null && values.Any())
            {
                return View(values);
            }

            return View();
        }

        public async Task<IActionResult> DeleteComment(string id)
        {
            await _commentService.DeleteCommentAsync(id);
            return Redirect("/Admin/Comment/Index");

        }
        [HttpGet]
        public async Task<IActionResult> UpdateComment(string id)
        {
            ViewBag.v1 = "Anasayfa";
            ViewBag.v2 = "Yorumlar";
            ViewBag.v3 = "Marka Güncelle";
            ViewBag.baslik = "Marka İşlemleri";

            var value = await _commentService.GetByIdCommentAsync(id);
            if (value != null)
            {
                return View(value);
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> UpdateComment(UpdateCommentDto updateCommentDto,string Status)
        {
            if (Status == "Aktif")
            {
                updateCommentDto.Status = true;
            }
            else
            {
                updateCommentDto.Status = false;
            }


            await _commentService.UpdateCommentAsync(updateCommentDto);
            return Redirect("/Admin/Comment/Index");

        }
    }
}
