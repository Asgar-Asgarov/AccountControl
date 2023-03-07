using Microsoft.AspNetCore.Mvc;
using WebApplication1.DAL;
using WebApplication1.Models;
using WebApplication1.ViewModels;

namespace WebApplication1.ViewComponents
{
    public class FooterViewComponent:ViewComponent
    {
        private readonly AppDbContext _appDbContext;

        public FooterViewComponent(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            FooterViewModel footerVM= new FooterViewModel();
            footerVM.Archives = _appDbContext.Archives.ToList();
            footerVM.Companies= _appDbContext.Companies.ToList();
            footerVM.SocialMedias= _appDbContext.SocialMedias.ToList();
            footerVM.CustomerServices= _appDbContext.CustomerServices.ToList();

            return View(await Task.FromResult(footerVM));
        }
    }
}
