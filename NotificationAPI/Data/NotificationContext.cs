using Microsoft.EntityFrameworkCore;
using NotificationAPI.Models;

namespace NotificationAPI.Data
{
    public class NotificationContext : DbContext
    {
        public NotificationContext(DbContextOptions<NotificationContext> opt) : base(opt)
        {

        }

        public DbSet<Notification> Notifications { get; set; }   
    }
}
