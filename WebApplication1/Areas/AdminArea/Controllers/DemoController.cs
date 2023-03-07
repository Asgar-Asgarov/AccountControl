using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApplication1.DAL;
using WebApplication1.Extensions;
using WebApplication1.Models;
using WebApplication1.Models.Demo;
using WebApplication1.ViewModels;

namespace WebApplication1.Areas.AdminArea.Controllers
{
    [Area("AdminArea")]
    public class DemoController : Controller
    {
        private readonly AppDbContext _appDbContext;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public DemoController(AppDbContext appDbContext, IWebHostEnvironment webHostEnvironment)
        {
            _appDbContext = appDbContext;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            return View();
        } 
        public IActionResult Create()
        {
            ViewBag.Authors = new SelectList(_appDbContext.Authors.ToList(),"Id","Name");
            ViewBag.Genres = new SelectList(_appDbContext.Genres.ToList(),"Id","Name");
            return View();
        }
        [HttpPost]
        public IActionResult Create(BookCreateVM createVM)
        {
            ViewBag.Authors = new SelectList(_appDbContext.Authors.ToList(), "Id", "Name");
            ViewBag.Genres = new SelectList(_appDbContext.Genres.ToList(), "Id", "Name");
            Book newBook = new();
            List<BookImage> bookImages = new();

            foreach (var photo in createVM.Photos)
            {

                if (!photo.IsImage())
                {
                    ModelState.AddModelError("Photos", "only image");
                    return View();
                }
                if (photo.CheckImageSize(500))
                {
                    ModelState.AddModelError("Photos", "Olcusu boyukdur");
                    return View();
                }
                BookImage newBookImage = new();
                newBookImage.ImageUrl = photo.SaveImage(_webHostEnvironment, "img", photo.FileName);
                bookImages.Add(newBookImage);
            }


            List<BookGenre> bookGenres= new();
            List<BookAuthor> bookAuthors= new();
            foreach (var item in createVM.GenreIds)
            {
                BookGenre bookGenre = new();
                bookGenre.BookId = newBook.Id;
                bookGenre.GenreId = item;
                bookGenres.Add(bookGenre);
                        
            }
            foreach (var item in createVM.AuthorIds)
            {
                BookAuthor bookAuthor = new();
                bookAuthor.BookId = newBook.Id;
                bookAuthor.AuthorId = item;
                bookAuthors.Add(bookAuthor);
                        
            }

            newBook.Name=createVM.Name;
            newBook.BookGenres = bookGenres;
            newBook.BookAuthors= bookAuthors;
            newBook.BookImages= bookImages;
            _appDbContext.Books.Add(newBook);
            _appDbContext.SaveChanges();
            
            return View();
        }
    }
}
