using Hirafeyat.Models;
using Hirafeyat.SellerServices;
using Hirafeyat.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Hirafeyat
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<HirafeyatContext>(options =>
           // add dbcontext
            options.UseSqlServer(builder.Configuration.GetConnectionString("CS")));
          builder.Services.AddIdentity<ApplicationUser,IdentityRole>().AddEntityFrameworkStores<HirafeyatContext>().AddDefaultTokenProviders();
            //regester service
            builder.Services.AddScoped<IOrderService, OrderService>();
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapStaticAssets();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}")
                .WithStaticAssets();

            app.Run();
        }
    }
}
