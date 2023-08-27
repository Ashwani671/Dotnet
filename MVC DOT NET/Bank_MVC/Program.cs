using Bank_MVC.Models;
using Microsoft.EntityFrameworkCore;

namespace Bank_MVC
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<MvcBankContext>(options =>
                        options.UseSqlServer(builder.Configuration.GetConnectionString("mvc_bankContext")));

            builder.Services.AddSession();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseSession();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Customers}/{action=Login}/{id?}");

            app.Run();
        }
    }
}