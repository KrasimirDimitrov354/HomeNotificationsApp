namespace HomeNotifications.Data.Configurations;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using HomeNotifications.Data.Models;
using HomeNotifications.Data.Seeders;

public class NotificationUserEntityConfiguration : IEntityTypeConfiguration<NotificationUser>
{
    private readonly NotificationUserSeeder userSeeder;

    public NotificationUserEntityConfiguration()
    {
        userSeeder = new NotificationUserSeeder();
    }

    public void Configure(EntityTypeBuilder<NotificationUser> builder)
    {
        builder.HasData(userSeeder.GenerateUsers());
    }
}
