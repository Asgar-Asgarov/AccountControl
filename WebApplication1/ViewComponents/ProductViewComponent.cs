using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.DAL;
using WebApplication1.Models;

namespace WebApplication1.ViewComponents
{
    public class ProductViewComponent: ViewComponent
    {
        private readonly AppDbContext _appDbContext;

        public ProductViewComponent(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<IViewComponentResult> InvokeAsync(int take)
        {

            var products = _appDbContext.Products
                .Include(p=>p.Category)
                .Include(p=>p.ProductImages)
                .Take(take)
                .ToList();
            return View(await Task.FromResult(products));
        }
    }
}
