using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using WebApplication1.DAL;
using WebApplication1.Extensions;
using WebApplication1.Models;
using WebApplication1.ViewModels;

namespace WebApplication1.Areas.AdminArea.Controllers
{
    [Area("AdminArea")]
    public class SliderController : Controller
    {
        private readonly AppDbContext _appDbContext;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public SliderController(AppDbContext appDbContext, IWebHostEnvironment webHostEnvironment)
        {
            _appDbContext = appDbContext;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            return View(_appDbContext.Sliders.ToList());
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(SliderCreateVM sliderCreateVM, int id)
        {
            if (sliderCreateVM.Photo == null)
            {
                ModelState.AddModelError("Photo", "Sekli bos qoyma");
                return View();
            }
            if (!sliderCreateVM.Photo.IsImage())
            {
                ModelState.AddModelError("Photo", "only image");
                return View();
            }
            if (sliderCreateVM.Photo.CheckImageSize(500))
            {
                ModelState.AddModelError("Photo", "Olcusu boyukdur");
                return View();
            }


            Slider newSlider = new();
            newSlider.ImageUrl = sliderCreateVM.Photo.SaveImage(_webHostEnvironment, "img", sliderCreateVM.Photo.FileName);
            _appDbContext.Sliders.Add(newSlider);
            _appDbContext.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            if (id == null) return NotFound();
            var slider = _appDbContext.Sliders.FirstOrDefault(s => s.Id == id);
            if (slider == null) return NotFound();
            string fullPath = Path.Combine(_webHostEnvironment.WebRootPath, "img", slider.ImageUrl);
            if (System.IO.File.Exists(fullPath))
            {
                System.IO.File.Delete(fullPath);
            }
            _appDbContext.Sliders.Remove(slider);
            _appDbContext.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id) 
        {
            if (id == null) return NotFound();
            Slider slider = _appDbContext.Sliders.SingleOrDefault(c => c.Id == id);
            if (slider == null) return NotFound();
            return View(new SliderUpdateVM {ImageUrl=slider.ImageUrl });
        }
        [HttpPost]
        public IActionResult Edit(int id, SliderUpdateVM sliderUpdateVM) 
        {
            if (id == null) return NotFound();
            Slider slider = _appDbContext.Sliders.SingleOrDefault(c => c.Id == id);
            if (slider == null) return NotFound();
            if (sliderUpdateVM.Photo!=null) 
            {
                
                if (!sliderUpdateVM.Photo.IsImage())
                {
                    ModelState.AddModelError("Photo", "only image");
                    return View();
                }
                if (sliderUpdateVM.Photo.CheckImageSize(500))
                {
                    ModelState.AddModelError("Photo", "Olcusu boyukdur");
                    return View();
                }
                string fullPath = Path.Combine(_webHostEnvironment.WebRootPath, "img", slider.ImageUrl);
                if (System.IO.File.Exists(fullPath))
                {
                    System.IO.File.Delete(fullPath);
                }
                slider.ImageUrl = sliderUpdateVM.Photo.SaveImage(_webHostEnvironment, "img", sliderUpdateVM.Photo.FileName);
                _appDbContext.SaveChanges();
            }
            return RedirectToAction("Index");
        }

    }
}
