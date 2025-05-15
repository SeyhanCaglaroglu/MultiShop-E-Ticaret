using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.DtoLayer.NotificationDtos
{
    public class CreateNotificationDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int UserCommentId { get; set; }
    }
}
