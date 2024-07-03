using Identity_Web.Areas.Admin.Models.DTOs;
using Identity_Web.Areas.Admin.Models.DTOs.Roles;
using Identity_Web.Data.DTOs;
using Identity_Web.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Identity_Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UsersController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        public UsersController(UserManager<User> userManager, RoleManager<Role> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
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




        public IActionResult Delete(string id)
        {
            var user = _userManager.FindByIdAsync(id).Result;

            EditUserViewModel editUser = new EditUserViewModel()
            {
                Id = user.Id,
                FirstName = user.Name,
                LastName = user.Family,
                UserName = user.UserName,
                PhoneNumber = user.PhoneNumber
            };
            return View(editUser);

        }


        [HttpPost]
        public IActionResult Delete(EditUserViewModel userDelete) 
        {
            var user = _userManager.FindByIdAsync(userDelete.Id).Result;

            var result=_userManager.DeleteAsync(user).Result;
            if (result.Succeeded) 
            {
                return RedirectToAction("Index", "Users", new { area = "Admin" });
            }
            return View(userDelete);
        }



        public IActionResult AddUserRole(string Id) 
        {

            var user = _userManager.FindByIdAsync(Id).Result;

            var roles = new List<SelectListItem>(
                _roleManager.Roles.Select(p => new SelectListItem
                {
                    Text = p.Name,
                    Value = p.Name,
                }
                ).ToList());

            return View(new AddUserRoleDto 
            {
                Id = Id,
                Roles = roles,
                Email = user.Email,
                Role = user.Name
            });
        }

        [HttpPost]
        public IActionResult AddUserRole(AddUserRoleDto newRole)
        {
            var user = _userManager.FindByIdAsync(newRole.Id).Result;
            var result = _userManager.AddToRoleAsync(user, newRole.Role).Result;
            return RedirectToAction("UserRoles", "Users", new { Id = user.Id, area = "admin" });
        }

    }
}
