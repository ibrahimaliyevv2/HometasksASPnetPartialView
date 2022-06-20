using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PustokProject.Areas.Manage.ViewModels;
using PustokProject.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PustokProject.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        // GET: /<controller>/
        //public async Task<IActionResult> CreateAdmin()
        //{
        //    AppUser admin = new AppUser
        //    {
        //        FullName = "Super Admin",
        //        UserName = "SuperAdmin",
        //    };

        //    var result = await _userManager.CreateAsync(admin, "Admin123");

        //    if (result.Succeeded)
        //    {
        //        return Ok(result.Errors);
        //    }

        //    return View();
        //}

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(AdminLoginViewModel admin)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            AppUser user = await _userManager.FindByNameAsync(admin.UserName);

            if(user == null)
            {
                ModelState.AddModelError("", "UserName or Password is not correct!");
                return View();
            }

            if(! await _userManager.CheckPasswordAsync(user, admin.Password)){
                ModelState.AddModelError("", "UserName or Password is not correct!");
                return View();
            }

            var result = await _signInManager.PasswordSignInAsync(user, admin.Password, false, false);

            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "UserName or Password is not correct!");
                return View();
            }

            return RedirectToAction("index", "dashboard");
        }

        public async Task<IActionResult> SignOut()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction("login", "account");
        }
    }
}

