namespace MultiShop.Notification.Entities
{
    public class CommentNotification
    {
        public int CommentNotificationId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int UserCommentId { get; set; }
    }
}
