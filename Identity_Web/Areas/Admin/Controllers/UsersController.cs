using Identity_Web.Areas.Admin.Models.DTOs;
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
    }
}
