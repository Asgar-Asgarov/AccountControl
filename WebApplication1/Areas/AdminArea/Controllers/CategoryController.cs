using Microsoft.AspNetCore.Mvc;
using WebApplication1.DAL;
using WebApplication1.Models;
using WebApplication1.ViewModels;

namespace WebApplication1.Areas.AdminArea.Controllers
{
    [Area("AdminArea")]
    public class CategoryController : Controller
    {
        private readonly AppDbContext _appDbContext;

        public CategoryController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public IActionResult Index()
        {
            return View(_appDbContext.Categories.ToList());
        }
        public IActionResult Detail(int id)
        {
            if (id == null) return NotFound();
            Category category = _appDbContext.Categories.SingleOrDefault(c => c.Id == id);
            if (category == null) return NotFound();
            return View(category);
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
    
        public IActionResult Create(Category category)
        {
            if (category == null) return NotFound();
            bool isExistCategory = _appDbContext.Categories.Any(c => c.Name == category.Name);
            if (isExistCategory) 
            {
                ModelState.AddModelError("Name", "Bu adli c movcuddur");
            }

            _appDbContext.Categories.Add(category);
            _appDbContext.SaveChanges();
            return RedirectToAction("Index");

        }
        public IActionResult Delete(int id) 
        {
            if (id == null) return NotFound();
            Category category = _appDbContext.Categories.SingleOrDefault(c => c.Id == id);
            if (category == null) return NotFound();
            _appDbContext.Categories.Remove(category);
            _appDbContext.SaveChanges();
            return RedirectToAction("Index");

        }


        public IActionResult Edit(int id) 
        {
            Category category = _appDbContext.Categories.SingleOrDefault(c => c.Id == id);
            if (category == null) return NotFound();    
            return View(new CategoryUpdateVM {Name=category.Name, Description=category.Description });
        }
        [HttpPost]
        public IActionResult Edit(int id, CategoryUpdateVM categoryUpdateVM)
        {
            if (id == null) return NotFound();
            Category newCategory = _appDbContext.Categories.Find(id);
            if (!ModelState.IsValid) return View();
            bool isExistCategory = _appDbContext.Categories.Any(c => c.Name.ToLower() == categoryUpdateVM.Name.ToLower() && c.Id != id);
            if (isExistCategory)
            {
                ModelState.AddModelError("Name", "Bu adli c movcuddur");
                return View();
            }
            if (newCategory == null) return NotFound();
            newCategory.Name = categoryUpdateVM.Name;
            newCategory.Description = categoryUpdateVM.Description;
            _appDbContext.SaveChanges();

            return RedirectToAction("Index");
        }


    }
}
