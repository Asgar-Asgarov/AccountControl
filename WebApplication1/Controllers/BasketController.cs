using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using WebApplication1.DAL;
using WebApplication1.Models;
using WebApplication1.ViewModels;

namespace WebApplication1.Controllers
{
    public class BasketController : Controller
    {
        private readonly AppDbContext _appDbContext;

        public BasketController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public IActionResult Index()
        {
            //HttpContext.Session.SetString("name","Ceyhun");
            Response.Cookies.Append("name", "Ceyhun", new CookieOptions { MaxAge = TimeSpan.FromHours(1) });
            return Content("set olundu");
        }

        public async Task<IActionResult> Add(int id, string name)
        {
            if (id == null) return NotFound();

            Product product = await _appDbContext.Products.FindAsync(id);

            List<BasketVM> products;
            if (Request.Cookies["basket"] == null)
            {
                products = new();
            }
            else
            {
                products = JsonConvert.DeserializeObject<List<BasketVM>>(Request.Cookies["basket"]);
            }
            BasketVM existProduct = products.FirstOrDefault(p => p.Id == id);
            if (existProduct == null)
            {
                BasketVM basketVM = new();
                basketVM.Id = product.Id;
                basketVM.BasketCount = 1;
                products.Add(basketVM);
            }
            else
            {
                existProduct.BasketCount++;
            }
            Response.Cookies.Append("basket", JsonConvert.SerializeObject(products), new CookieOptions { MaxAge = TimeSpan.FromHours(1) });
            return RedirectToAction(nameof(Index), "Home");



        }
        public async Task<IActionResult> DeleteItem(int id)
        {
            List<BasketVM> products = JsonConvert.DeserializeObject<List<BasketVM>>(Request.Cookies["basket"]);
            products.Remove(products.FirstOrDefault(p => p.Id == id));
            Response.Cookies.Append("basket", JsonConvert.SerializeObject(products), new CookieOptions { MaxAge = TimeSpan.FromHours(1) });
            return RedirectToAction(nameof(ShowBasket));

        }
        public  IActionResult IncreaseCount(int id) 
        {
            List<BasketVM> products = JsonConvert.DeserializeObject<List<BasketVM>>(Request.Cookies["basket"]);
            BasketVM product=products.FirstOrDefault(p => p.Id == id);
            product.BasketCount++;
            Response.Cookies.Append("basket", JsonConvert.SerializeObject(products), new CookieOptions { MaxAge = TimeSpan.FromHours(1) });
            return RedirectToAction(nameof(ShowBasket));
        }
        public IActionResult DecreaseCount(int id)
        {
            List<BasketVM> products = JsonConvert.DeserializeObject<List<BasketVM>>(Request.Cookies["basket"]);
            BasketVM product = products.FirstOrDefault(p => p.Id == id);
            if (product.BasketCount > 1) 
            {
                product.BasketCount--;
            }
            Response.Cookies.Append("basket", JsonConvert.SerializeObject(products), new CookieOptions { MaxAge = TimeSpan.FromHours(1) });
            return RedirectToAction(nameof(ShowBasket));
        }

        public IActionResult ShowBasket()
        {
            //string name = HttpContext.Session.GetString("name");
            //string name = Request.Cookies["name"];
            List<BasketVM> products;
            string basket = Request.Cookies["basket"];
            if (basket == null)
            {
                products = new();
            }
            else 
            {
                products= JsonConvert.DeserializeObject<List<BasketVM>>(basket);
                foreach (var item in products) 
                {
                    Product currentProduct = _appDbContext.Products.Include(p => p.ProductImages).FirstOrDefault(p =>p.Id==item.Id);
                    item.Name = currentProduct.Name; 
                    item.Price = currentProduct.Price;
                    item.ImageUrl = currentProduct.ProductImages.FirstOrDefault().ImageUrl;
                }
            }
            

            return View(products);
        }
    }
}
