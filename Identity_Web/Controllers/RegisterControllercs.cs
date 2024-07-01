using Identity_Web.Data.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Identity_Web.Controllers
{
    public class RegisterControllercs : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(RegisterViewModel register)
        {
            return View();
        }

    }
}
