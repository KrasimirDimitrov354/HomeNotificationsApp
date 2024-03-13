namespace HomeNotifications.Data.Configurations;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using HomeNotifications.Data.Models;
using HomeNotifications.Data.Seeders;

public class UserRoleEntityConfiguration : IEntityTypeConfiguration<UserRole>
{
    private readonly UserRoleSeeder roleSeeder;

    public UserRoleEntityConfiguration()
    {
        roleSeeder = new UserRoleSeeder();
    }

    public void Configure(EntityTypeBuilder<UserRole> builder)
    {
        builder.HasData(roleSeeder.GenerateRoles());
    }
}
