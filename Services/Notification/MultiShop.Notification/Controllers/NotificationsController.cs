using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MultiShop.Notification.Context;
using MultiShop.Notification.Dtos;
using MultiShop.Notification.Entities;

namespace MultiShop.Notification.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationsController : ControllerBase
    {
        private readonly NotificationContext _notificationContext;

        public NotificationsController(NotificationContext notificationContext)
        {
            _notificationContext = notificationContext;
        }

        [HttpGet]
        public IActionResult NotificationList()
        {
            var values = _notificationContext.CommentNotifications.ToList();
            return Ok(values);
        }

        [HttpPost]
        public IActionResult CreateNotification(CreateCommentNotificationDto notificationDto)
        {
            var notification = new CommentNotification
            {
                Title = notificationDto.Title,
                Description = notificationDto.Description,
                UserCommentId = notificationDto.UserCommentId,
            };

            _notificationContext.CommentNotifications.Add(notification);
            _notificationContext.SaveChanges();
            return Ok("Bildirim Başarıyla Eklendi !");
        }

        [HttpGet("{id}")]
        public IActionResult GetByIdNotification(int id)
        {
            var value = _notificationContext.CommentNotifications.Find(id);
            return Ok(value);
        }
        [HttpDelete("DeleteNotification")]
        public IActionResult DeleteNotification(int id)
        {
            var value = _notificationContext.CommentNotifications.Find(id);
            _notificationContext.CommentNotifications.Remove(value);
            _notificationContext.SaveChanges();
            return Ok("Bildirim Başarıyla Silindi !");
        }
        [HttpDelete("DeleteAllNotifications")]
        public IActionResult DeleteAllNotifications()
        {
            var allNotifications = _notificationContext.CommentNotifications.ToList();
            _notificationContext.CommentNotifications.RemoveRange(allNotifications);
            _notificationContext.SaveChanges();
            return Ok("Tüm Bildirimler Başarıyla Silindi !");
        }
        [HttpGet("NotificationCount")]
        public IActionResult NotificationCount()
        {
            var totalCount = _notificationContext.CommentNotifications.Count();
            return Ok(totalCount);
        }


    }
}
