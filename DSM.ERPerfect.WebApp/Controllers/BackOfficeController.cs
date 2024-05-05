using Microsoft.AspNetCore.Mvc;

namespace DSM.ERPerfect.WebApp.Controllers
{
    public class BackOfficeController : BaseController
    {
        private readonly ILogger<BackOfficeController> _logger;

        public BackOfficeController(ILogger<BackOfficeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
