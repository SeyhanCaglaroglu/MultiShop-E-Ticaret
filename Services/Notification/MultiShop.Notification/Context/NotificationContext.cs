using Microsoft.EntityFrameworkCore;
using MultiShop.Notification.Entities;

namespace MultiShop.Notification.Context
{
    public class NotificationContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("example");
        }

        public DbSet<CommentNotification> CommentNotifications { get; set; }
    }
}
