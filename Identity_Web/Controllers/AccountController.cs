using Identity_Web.Data.DTOs;
using Identity_Web.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp;

namespace Identity_Web.Controllers
{
    public class AccountController : Controller
    {

        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        #region Register
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(RegisterViewModel register)
        {

            if (!ModelState.IsValid)
            {
                return View(register);
            }

            User newUser = new User()
            {
                Name = register.FirstName,
                Family = register.LastName,
                Email = register.Email,
                UserName = register.Email
            };

            var result = _userManager.CreateAsync(newUser, register.Password).Result;

            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                string message = "";
                foreach (var item in result.Errors.ToList())
                {
                    message += item.Description + Environment.NewLine;
                }
                TempData["RegisterError"] = message;
            }
            return View(register);
        }
        #endregion


        #region LogIn

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Login(LoginViewModel login)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("UserName", "خطا ....");
                return View(login);
            }

            _signInManager.SignOutAsync();

            var user = _userManager.FindByNameAsync(login.UserName).Result;
            if (user == null)
            {
                return NotFound();
            }
            var result = _signInManager.PasswordSignInAsync(user, login.Password, login.IsPersistant, true).Result;

            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }
            if (result.RequiresTwoFactor == true)
            {
                //ToDo:
            }
            if (result.IsLockedOut == true)
            {
                //ToDo
            }
            


            return View();
        }
        #endregion

        #region LogOut
      
        public IActionResult LogOut() 
        {
            _signInManager.SignOutAsync();
            return RedirectToAction("Index", "home");
        }
        #endregion
    }
}
