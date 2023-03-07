using System.Configuration;
using WebApplication1.Services.Basket;
using WebApplication1.Services.GetItems;

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
    }
   
}
