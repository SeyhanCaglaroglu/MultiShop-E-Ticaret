using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.DtoLayer.NotificationDtos
{
    public class ResultNotificationDto
    {
        public int CommentNotificationId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int UserCommentId { get; set; }
    }
}
