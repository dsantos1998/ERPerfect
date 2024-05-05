using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

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
            return View();
        }
    }
}
