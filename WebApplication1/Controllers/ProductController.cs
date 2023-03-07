using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.DAL;

namespace WebApplication1.Controllers
{
    public class ProductController : Controller
    {
        private readonly AppDbContext _appDbContext;

        public ProductController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public IActionResult Index()
        {
            var query = _appDbContext.Products.Include(p => p.ProductImages).Include(p => p.Category);
            ViewBag.ProductCount = query.Count();
            var products=query
                .Take(3)
                .ToList();
            return View(products);
        }
        public IActionResult LoadMore(int skip) 
        {
            var products= _appDbContext.Products
                .Include(p=>p.ProductImages) 
                .Include(p=>p.Category)
                .Skip(skip)
                .Take(3)
                .ToList();
            return PartialView("_ProductLoadMorePartial",products);
        }
    }
}
