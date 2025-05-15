using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MultiShop.Comment.Context;
using MultiShop.Comment.Entities;

namespace MultiShop.Comment.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly CommentContext _context;
        private int commentCountIncrease;
        public CommentsController(CommentContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult CommentList()
        {
            var values = _context.UserComments.ToList();
            return Ok(values);
        }
        [HttpGet("{id}")]
        public IActionResult GetByIdComment(int id)
        {
            var value = _context.UserComments.Find(id);
            return Ok(value);
        }
        [HttpGet("GetCommentByProductId/{id}")]
        public IActionResult GetCommentByProductId(string id)
        {
            var value = _context.UserComments.Where(x=>x.ProductId==id).ToList();
            return Ok(value);
        }
        [HttpPost]
        public IActionResult CreateComment(UserComment comment)
        {
            comment.CreatedDate = DateTime.Now;
            _context.UserComments.Add(comment);
            _context.SaveChanges();

            return Ok("Yorum Başarıyla Eklendi!");
        }
        [HttpPut]
        public IActionResult UpdateComment(UserComment comment)
        {
            _context.UserComments.Update(comment);
            _context.SaveChanges();
            return Ok("Yorum Başarıyla Güncellendi!");
        }
        [HttpDelete]
        public IActionResult DeleteComment(int id)
        {
            var value = _context.UserComments.Find(id);
            _context.UserComments.Remove(value);
            _context.SaveChanges();

            return Ok("Yorum Başarıyla Silindi!");
        }
        [HttpGet("GetActiveCommentCount")]
        public IActionResult GetActiveCommentCount()
        {
            var value = _context.UserComments.Where(x=>x.Status == true).Count();
            return Ok(value);
        }
        [HttpGet("GetPassiveCommentCount")]
        public IActionResult GetPassiveCommentCount()
        {
            var value = _context.UserComments.Where(x => x.Status == false).Count();
            return Ok(value);
        }
        [HttpGet("GetTotalCommentCount")]
        public IActionResult GetTotalCommentCount()
        {
            var value = _context.UserComments.Count();
            return Ok(value);
        }
        
    }
}
