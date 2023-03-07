using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebApplication1.DAL;
using WebApplication1.Models;
using WebApplication1.ViewModels;

namespace WebApplication1.ViewComponents;

public class HeaderViewComponent:ViewComponent
{
    private readonly AppDbContext _appDbContext;

    public HeaderViewComponent(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }
    public async Task<IViewComponentResult> InvokeAsync() {
        string basket = Request.Cookies["basket"];
        List<BasketVM> products;
        if (basket == null)
        {
            ViewBag.BasketProductCount = 0;
            ViewBag.TotalPrice = 0;
        }
        else
        {
            products = JsonConvert.DeserializeObject<List<BasketVM>>(basket);
            ViewBag.BasketProductCount = products.Sum(p => p.BasketCount);
            ViewBag.TotalPrice =products.Sum(p => p.BasketCount * p.Price);
        }
        

        Bio bio=_appDbContext.Bios.FirstOrDefault();

        return View(await Task.FromResult(bio));
    }
}
