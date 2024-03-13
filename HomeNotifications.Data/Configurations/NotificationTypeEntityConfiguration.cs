namespace HomeNotifications.Data.Configurations;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using HomeNotifications.Data.Models;
using HomeNotifications.Data.Seeders;

public class NotificationTypeEntityConfiguration : IEntityTypeConfiguration<NotificationType>
{
    private readonly NotificationTypeSeeder typeSeeder;

    public NotificationTypeEntityConfiguration()
    {
        typeSeeder = new NotificationTypeSeeder();
    }

    public void Configure(EntityTypeBuilder<NotificationType> builder)
    {
        builder.HasData(typeSeeder.GenerateTypes());
    }
}
