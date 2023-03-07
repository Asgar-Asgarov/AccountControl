using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using WebApplication1.DAL;
using WebApplication1.Models.Demo;

namespace WebApplication1.Controllers
{
    public class DemoController : Controller
    {
        private readonly AppDbContext _appDbContext;
        private readonly IConfiguration _configuration;
   
        public DemoController(AppDbContext appDbContext, IConfiguration configuration)
        {
            _appDbContext = appDbContext;
            _configuration = configuration;
          
        }
        
        public IActionResult Index()
        {
            //string connectionString = _configuration["ConnectionStrings:DefaultConnection"];

            //ConnectionString kimi secret olmali value lari secretJson da ve ya envoriment de saxlamaq olar daha tehlukezdir.
            
            //string connectionString = _configuration.GetSection("ConnectionStrings:DefaultConnection").Value;
            var books= _appDbContext.Books
                .Include(b=>b.BookImages)
                .Include(b => b.BookGenres)
                .ThenInclude(bg=>bg.Genre)
                 .Include(b=>b.BookAuthors)
                 .ThenInclude(ba=>ba.Author)
                 .ThenInclude(a=>a.SocialPage)
                .ToList();
            
            return View(books);
        }
    }
}
