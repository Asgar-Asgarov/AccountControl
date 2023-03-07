using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using WebApplication1.DAL;
using WebApplication1.Models;
using WebApplication1.Services.GetItems;
using WebApplication1.ViewModels;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _appDbContext;
        private readonly IGetProducts _getProducts;

        
        public HomeController(AppDbContext appDbContext, IGetProducts getProducts)
        { 
            _appDbContext = appDbContext;
            _getProducts = getProducts;
        }

        public IActionResult Index()
        {
            HomeViewModel homeVM = new HomeViewModel(); 
            homeVM.Sliders= _appDbContext.Sliders.ToList();
            homeVM.SliderDetail = _appDbContext.SliderDetails.FirstOrDefault() ;
            homeVM.Categories= _appDbContext.Categories.ToList();
            homeVM.Products = _getProducts.GetProductsFromDataBase();

            return View(homeVM);
        }
    }
}