namespace MultiShop.Notification.Dtos
{
    public class CreateCommentNotificationDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int UserCommentId { get; set; }
    }
}
