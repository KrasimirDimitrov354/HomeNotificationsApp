namespace HomeNotifications.Data.Configurations;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using HomeNotifications.Data.Models;

public class NotificationEntityConfiguration : IEntityTypeConfiguration<Notification>
{
    public void Configure(EntityTypeBuilder<Notification> builder)
    {
        builder
            .HasOne(n => n.Owner)
            .WithMany(u => u.Notifications)
            .HasForeignKey(n => n.OwnerId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
