using Microsoft.AspNetCore.Mvc;
using MultiShop.DtoLayer.CommentDtos;
using MultiShop.WebUI.Services.CommentServices;
using Newtonsoft.Json;

namespace MultiShop.WebUI.ViewComponents.ProductDetailViewComponents
{
    public class _ReviewProductDetailComponentPartial : ViewComponent
    {
        private readonly ICommentService _commentService;

        public _ReviewProductDetailComponentPartial(ICommentService commentService)
        {
            _commentService = commentService;
        }

        public async Task<IViewComponentResult> InvokeAsync(string id)
        {
            var values = await _commentService.GetByProductIdWithCommentAsync(id);

            var acttiveCommentCount = values.Where(x=>x.Status == true).Count();
            ViewBag.acttiveCommentCount = acttiveCommentCount;

            if (values != null && values.Any())
            {
                return View(values);
            }
            return View();
        }
    }
}
