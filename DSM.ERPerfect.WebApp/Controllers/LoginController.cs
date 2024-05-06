using DSM.ERPerfect.Models.VM;
using Microsoft.AspNetCore.Mvc;

namespace DSM.ERPerfect.WebApp.Controllers
{
    public class LoginController : BaseController
    {
        private readonly ILogger<LoginController> _logger;

        public LoginController(ILogger<LoginController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            DeleteCookies();

            return View();
        }

        public IActionResult LogIn(LoginVM login)
        {
            // TODO: Check user with database
            // TODO: Save cookie user information


            return RedirectToAction("Index", "Home");
        }

    }
}
