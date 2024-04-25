namespace HomeNotifications.WebApi;

using Microsoft.EntityFrameworkCore;

using HomeNotifications.Data;
using HomeNotifications.Services.Data.Interfaces;
using HomeNotifications.Web.Infrastructure.Extensions;

public class Program
{
    public static void Main(string[] args)
    {
        WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

        string connectionString = builder.Configuration
            .GetConnectionString("DefaultConnection")
            ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

        builder.Services.AddDbContext<HomeNotificationsDbContext>(options => options.UseSqlServer(connectionString));
        builder.Services.AddApplicationServices(typeof(IUserService));

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();

        builder.Services.AddApplicationSwaggerGen();
        builder.Services.AddApplicationJWTAuthentication();

        WebApplication app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}");

        app.Run();
    }
}
