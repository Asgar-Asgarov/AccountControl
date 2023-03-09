using WebApplication1.Models;
using WebApplication1.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
namespace WebApplication1.Controllers;

public class AccountController:Controller
{
    private readonly  UserManager<AppUser> _userManager;

    public AccountController(UserManager<AppUser> userManager)
    {
        _userManager = userManager;
    }

    public IActionResult Register()
    {
        // AppUser user = new();

        return View();
    } 
    
    [HttpPost]
    [AutoValidateAntiforgeryToken]
    public async Task<IActionResult> Register (RegisterVm register)
    {
        if(!ModelState.IsValid) return View();
        AppUser user = new();
        user.Email=register.Email;
        user.Fullname=register.Fullname;
        user.UserName=register.Username;
        IdentityResult result = await _userManager.CreateAsync(user,register.Password);
        if(!result.Succeeded)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("",error.Description);
            }
            return View(register);
        }
        return RedirectToAction("index","home");
    }

    public IActionResult Login()
    {
        return View();
    } 
}