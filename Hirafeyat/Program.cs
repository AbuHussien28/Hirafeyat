using Hirafeyat.AdminRepository;
using Hirafeyat.AdminServices;
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
             options.UseSqlServer(builder.Configuration.GetConnectionString("CS")));
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options => 
            {
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredLength = 6;
                options.Password.RequireDigit = false;
            })
                .AddEntityFrameworkStores<HirafeyatContext>()
                .AddDefaultTokenProviders();
            //regester service
            builder.Services.AddScoped<IOrderService, OrderService>();

            builder.Services.AddScoped<IProductRepository, ProductService>();
            builder.Services.AddScoped<ICategoryRepository, CategoryService>();



            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IOrderRepositoryAdmin, OrderRepositoryAdmin>();
            builder.Services.AddScoped<IOrderAdminService, OrderAdminService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseStaticFiles();
            app.MapStaticAssets();
            app.MapControllerRoute(
                name: "default",
                  //pattern: "{controller=Seller}/{action=Orders}")
                  //pattern: "{controller=Account}/{action=Login}")
                 // pattern: "{controller=User}/{action=Sellers}")
                 pattern: "{controller=AdminOrder}/{action=Index}")
                //pattern: "{controller=User}/{action=Customers}")
                //pattern: "{controller=Home}/{action=Index}/{id?}")
                .WithStaticAssets();

            app.Run();
        }
    }
}
