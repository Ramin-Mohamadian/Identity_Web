using Identity_Web.Areas.Admin.Models.DTOs;
using Identity_Web.Data.DTOs;
using Identity_Web.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Identity_Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UsersController : Controller
    {
        private readonly UserManager<User> _userManager;
        public UsersController(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            var users=_userManager.Users.Select(p=> new UserListDto()
            {
                Id = p.Id,
                UserName = p.UserName,
                FirstName=p.Name,
                LastName=p.Family,
                PhoneNumber=p.PhoneNumber,
                EmailConfirmed=p.EmailConfirmed,
                AccessFaildCount = p.AccessFailedCount
            }).ToList();


            return View(users);
        }


        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(RegisterViewModel register)
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
                return RedirectToAction("Index", "Users", new {area="Admin"});
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


        public IActionResult Edit(string id) 
        { 
            var user =_userManager.FindByIdAsync(id).Result;

            EditUserViewModel editUser = new EditUserViewModel()
            {
                Id=user.Id,
                FirstName=user.Name,
                LastName=user.Family,
                UserName=user.UserName,
                PhoneNumber=user.PhoneNumber
            };
            return View(editUser);
        }

        [HttpPost]
        public IActionResult Edit(EditUserViewModel editUser)
        {
            var user=_userManager.FindByIdAsync(editUser.Id).Result;

            user.UserName= editUser.UserName;
            user.Name=editUser.FirstName;
            user.Family=editUser.LastName;
            user.PhoneNumber=editUser.PhoneNumber;


           var result= _userManager.UpdateAsync(user).Result;

            if(result.Succeeded)
            {
                return RedirectToAction("Index", "Users", new { area = "Admin" });
            }

            return View(editUser);
        }
    }
}
