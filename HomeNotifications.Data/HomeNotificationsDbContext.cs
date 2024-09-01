namespace HomeNotifications.Data;

using System.Reflection;

using Microsoft.EntityFrameworkCore;

using HomeNotifications.Data.Models;

public class HomeNotificationsDbContext : DbContext
{
    public HomeNotificationsDbContext(DbContextOptions<HomeNotificationsDbContext> options)
        : base(options)
    {

    }

    public DbSet<NotificationUser> Users { get; set; } = null!;
    public DbSet<UserRole> UserRoles { get; set; } = null!;
    public DbSet<Notification> Notifications { get; set; } = null!;
    public DbSet<NotificationType> NotificationTypes { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder builder)
    {
        Assembly configAssembly = Assembly.GetAssembly(typeof(HomeNotificationsDbContext)) ??
                                  Assembly.GetExecutingAssembly();

        builder.ApplyConfigurationsFromAssembly(configAssembly);

        base.OnModelCreating(builder);
    }
}
