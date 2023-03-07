using System.Configuration;
using WebApplication1.Services.Basket;
using WebApplication1.Services.GetItems;
using WebApplication1.Models;
using Microsoft.AspNetCore.Identity;
using WebApplication1.DAL;

namespace WebApplication1;

public static class ServiceRegistration
{
    public static void FrontToBackServiceRegistration(this IServiceCollection services) 
    {
        services.AddControllersWithViews()
            .AddNewtonsoftJson(options =>
            options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
        );
        services.AddHttpContextAccessor();
        //scope transient singleton
        services.AddScoped<IBasketProductCount,BasketProductCount>();
        services.AddScoped<IGetProducts, GetProducts>();
        services.AddIdentity<AppUser,IdentityRole>(options=>{
            options.Password.RequiredLength=6;
            options.Password.RequireNonAlphanumeric=true;
            options.Password.RequireDigit=true;
            options.Password.RequireLowercase=true;
            options.Password.RequireUppercase=true;

            options.User.RequireUniqueEmail=true;


            options.Lockout.DefaultLockoutTimeSpan=TimeSpan.FromMinutes(20);
            options.Lockout.AllowedForNewUsers=true;
            options.Lockout.MaxFailedAccessAttempts=3;
        }

        ).AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();
    }
   
}
