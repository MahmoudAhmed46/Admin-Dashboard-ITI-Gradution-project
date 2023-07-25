using Amazon.Application.Contracts;
using Amazon.Application.Services;
using Amazon.Infrastrucure;
using AmazonAdmin.Application.Contracts;
using AmazonAdmin.Application.Services;
using AmazonAdmin.Context;
using AmazonAdmin.Domain;
using AmazonAdmin.Infrastructure;
using AmazonAdmin.Infrastrucure;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AmazonAdminDashboardMVC
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
			builder.Services.AddDbContext<ApplicationContext>(options =>
			{
				options.UseSqlServer(builder.Configuration.GetConnectionString("CS"));
			});
			builder.Services.AddIdentity<ApplicationUser, IdentityRole>(option =>
			{
			}).AddEntityFrameworkStores<ApplicationContext>();
			builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
			builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie();

			builder.Services.AddScoped<IProductReposatory, ProductReposatory>();
			builder.Services.AddScoped<IProductServices, ProductSrvices>();
			builder.Services.AddScoped<ICategoryReposatory, CategoryReposatory>();
			builder.Services.AddScoped<IcategoryServices, CategoryService>();
			builder.Services.AddScoped<ISubCategoryReposatory, SubCategoryReposatory>();
			builder.Services.AddScoped<ISubcategoryServices, SubCategoryService>();
            builder.Services.AddScoped<IOrderReposatory, OrderRepository>();
            builder.Services.AddScoped<IOrderService, OrderService>();
			builder.Services.AddScoped<IOrderItemService, OrderItemServices>();
            builder.Services.AddScoped<IOrderItemReposatory, OrderItemReposatory>();
			builder.Services.AddScoped<IImageReposatory, ImageReposatory>();
			builder.Services.AddScoped<IUserReposatory, UserRepository>();
            //builder.Services.AddScoped<>
            builder.Services.AddScoped<IImageService, ImageService>();
            builder.Services.AddScoped<IRatingRepository, RatingRepository>();
            builder.Services.AddScoped<IRatingService, RatingService>();
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
                

            app.Run();
        }
    }
}