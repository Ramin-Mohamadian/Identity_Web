﻿using Azure.Core;
using Identity_Web.Areas.Admin.Services;
using Identity_Web.Data.Context;
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
        private readonly EmailService _emailService;
        private readonly MyDbContext _myDbContext;
        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, MyDbContext myDbContext)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailService = new EmailService();
            _myDbContext = myDbContext;
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
                var token = _userManager.GenerateEmailConfirmationTokenAsync(newUser).Result;

                string CallBackUrl = Url.Action("ConfirmEmail", "Account", new
                {
                    UserId = newUser.Id
                    ,
                    Token = token
                }
                , protocol: Request.Scheme);

                string body = $"لطفا برای فعالسازی حساب خود روی لینک زیر کلیک کنید </br> " +
                    $"<a href={CallBackUrl}> link</a>";

                _emailService.Excute(newUser.Email, body, "فعال سازی حساب ");

                return RedirectToAction("DisplayEmail", "Account");
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



        #region ForgotPassword
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ForgotPassword(ForgotPasswordDto forgot)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            // var usebr =_userManager.FindByEmailAsync( forgot.Email).Result;
            var user = _myDbContext.Users.FirstOrDefault(u => u.Email == forgot.Email);

            if (user == null)
            {
                return NotFound();
            }

            var token = _userManager.GeneratePasswordResetTokenAsync(user).Result;
            string callbackUrl = Url.Action("RessetPassword", "Account", new { UserId = user.Id, Token = token }, protocol: Request.Scheme);

            string body = $"جهت بازیابی کلمه عبور بر روی لینک زیر کلیک کنید </br><a href={callbackUrl}>  RessetPasswordLink </a>";
            _emailService.Excute(user.Email, body, "بازیابی کلمه عبور");
            return PartialView("SuccessResetPassword");
        }




        public IActionResult RessetPassword(string UserId, string Token)
        {
            ResetPasswordDto ResetPassword = new ResetPasswordDto()
            {
                UserId = UserId,
                Token = Token,
            };
            return View(ResetPassword);
        }

        [HttpPost]
        public IActionResult RessetPassword(ResetPasswordDto reset)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var user=_userManager.FindByIdAsync(reset.UserId).Result;
            var result=_userManager.ResetPasswordAsync(user, reset.Token, reset.Password).Result;
            if(result.Succeeded)
            {
                return View("~/Views/Account/View.cshtml");
            }
            return View(reset);
        }
        #endregion



        #region LogIn

        public IActionResult ConfirmEmail(string UserId, string Token)
        {
            if (UserId == null || Token == null)
            {
                return BadRequest();
            }
            var user = _userManager.FindByIdAsync(UserId).Result;
            if (user == null)
            {
                return NotFound();
            }

            var confirm = _userManager.ConfirmEmailAsync(user, Token).Result;
            if (confirm.Succeeded)
            {
                return RedirectToAction("Login", "Account");
            }

            return NoContent();
        }

        public IActionResult DisplayEmail()
        {
            return View();
        }


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
