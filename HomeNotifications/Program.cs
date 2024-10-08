namespace HomeNotifications.Web;

using Microsoft.EntityFrameworkCore;

using HomeNotifications.Data;
using HomeNotifications.WebApi.Controllers;
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
        builder.Services.AddApplicationControllers(typeof(UserController));

        builder.Services.AddRazorPages();

        builder.Services.AddApplicationCookieAuthentication();

        WebApplication app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseExceptionHandler("/Error");
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapRazorPages();

        app.Run();
    }
}
