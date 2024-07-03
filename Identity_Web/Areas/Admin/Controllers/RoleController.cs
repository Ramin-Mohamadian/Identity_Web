using Identity_Web.Areas.Admin.Models.DTOs.Roles;
using Identity_Web.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Identity_Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class RoleController : Controller
    {
        private readonly RoleManager<Role> _roleManager;
        private readonly UserManager<User> _userManager;
        public RoleController(RoleManager<Role> roleManager, UserManager<User> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public IActionResult Index()
        {

            var roles = _roleManager.Roles.Select(p => new RoleListDto
            {
                Id = p.Id,
                FirstName = p.Name,
                Description = p.Description,

            }).ToList();
            return View(roles);
        }


        public IActionResult Create()
        {

            return View();
        }

        [HttpPost]
        public IActionResult Create(AddNewRoleDto addNewRole)
        {
            Role role = new Role()
            {
                Name = addNewRole.Name,
                Description = addNewRole.Description,
            };

            var result = _roleManager.CreateAsync(role).Result;
            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Role", new { area = "Admin" });
            }
            return View();
        }



        public IActionResult Edit(string id)
        {
            var role = _roleManager.FindByIdAsync(id).Result;
            AddNewRoleDto editRole = new AddNewRoleDto()
            {
                Name = role.Name,
                Description = role.Description,
            };


            return View(editRole);
        }

        [HttpPost]
        public IActionResult Edit(AddNewRoleDto editRole)
        { 
            var role=_roleManager.FindByNameAsync(editRole.Name).Result;    
            role.Name=editRole.Name;
            role.Description=editRole.Description;
            var result=_roleManager.UpdateAsync(role).Result;
            if (result.Succeeded) 
            {
                return RedirectToAction("Index", "Role", new { area = "Admin" });
            }

            return View(); 
        }



    }
}
