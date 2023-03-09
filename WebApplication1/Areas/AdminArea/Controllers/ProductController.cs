using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication1.DAL;
using WebApplication1.Extensions;
using WebApplication1.Models;
using WebApplication1.Models.Demo;
using WebApplication1.ViewModels;

namespace WebApplication1.Areas.AdminArea.Controllers
{
    [Area("AdminArea")]
    public class ProductController : Controller
    {
        private readonly AppDbContext _appDbContext;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductController(AppDbContext appDbContext, IWebHostEnvironment webHostEnvironment)
        {
            _appDbContext = appDbContext;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            return View(_appDbContext.Products.Include(p => p.ProductImages).Include(p => p.Category).ToList());
        }
        public IActionResult Create()
        {
            //ViewBag.Categories = _appDbContext.Categories.ToList();            
            ViewBag.Categories = new SelectList(_appDbContext.Categories.ToList(),"Id","Name");
            return View();
        }
        [HttpPost]
        public IActionResult Create(ProductCreateVM createVM) 
        {
            ViewBag.Categories = new SelectList(_appDbContext.Categories.ToList(), "Id", "Name");
            if (!ModelState.IsValid) return View();
            Product newProduct = new();
            List<ProductImage>? productImages = new();

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
                ProductImage newProductImage = new();
                newProductImage.ImageUrl = photo.SaveImage(_webHostEnvironment, "img", photo.FileName);
                productImages.Add(newProductImage);
            }


            newProduct.Name= createVM.Name;
            newProduct.Price= createVM.Price;
            newProduct.ProductImages= productImages;
            newProduct.CategoryId= createVM.CategoryId;
            _appDbContext.Products.Add(newProduct);
            _appDbContext.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id) 
        {
            if (id == null) return NotFound();
            Product existProduct=_appDbContext.Products.Include(p=>p.ProductImages).FirstOrDefault(p=>p.Id==id);
            if (existProduct == null) return NotFound();
            foreach (var item in existProduct.ProductImages)
            {
                string fullPath = Path.Combine(_webHostEnvironment.WebRootPath, "img", item.ImageUrl);
                if (System.IO.File.Exists(fullPath))
                {
                    System.IO.File.Delete(fullPath);
                }
            }
            _appDbContext.Products.Remove(existProduct);
            _appDbContext.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            if (id == null) return NotFound();
            Product product = _appDbContext.Products.Include(p => p.ProductImages).FirstOrDefault(c => c.Id == id);
            if (product == null) return NotFound();
            ProductUpdateVM productUpdateVM = new();
            ViewBag.Categories = new SelectList(_appDbContext.Categories.ToList(), "Id", "Name");
            productUpdateVM.Name = product.Name;
            productUpdateVM.Price = product.Price;
            productUpdateVM.CategoryId = product.CategoryId;
            productUpdateVM.ProductImages = product.ProductImages;

            return View(productUpdateVM);
        }
        [HttpPost]
        public IActionResult Edit(ProductUpdateVM updateVM, int id)
        {
            if (id == null) return NotFound();
            Product product = _appDbContext.Products.Include(p => p.ProductImages).FirstOrDefault(p => p.Id == id);
            if(product == null) return NotFound();  
            if (!ModelState.IsValid) return View();
            List<ProductImage> productImages = new();
            if (updateVM.Photos != null)
            {
                foreach (var photo in updateVM.Photos)
                {
                    if (!photo.IsImage())
                    {
                        ModelState.AddModelError("Photo", "only image");
                        return View();
                    }
                    if (photo.CheckImageSize(1000))
                    {
                        ModelState.AddModelError("Photo", "Olcusu boyukdur");
                        return View();
                    }
                    ProductImage newProductImage = new();
                    newProductImage.ImageUrl = photo.SaveImage(_webHostEnvironment, "img", photo.FileName);
                    productImages.Add(newProductImage);


                }
                product.ProductImages = productImages;
            }
            product.Name = updateVM.Name;
            product.Price = updateVM.Price;
            product.CategoryId = updateVM.CategoryId;

            _appDbContext.SaveChanges();


            return RedirectToAction("Index");
        }
    }
}
