using DSM.ERPerfect.Helpers;
using DSM.ERPerfect.Models.Cookies;
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
            if (resultadoGuardarUsuarioLog.HasErrors)
            {
                LoggedErrors(_logger, resultadoGuardarUsuarioLog.Errors);
                return StatusCode(StatusCodes.Status401Unauthorized, "Credenciales inválidas");
            }

            // TODO: Save cookie user information
            CookieUsuario cookieUsuario = new CookieUsuario()
            {
                Login = resultUsuario.Content.Nickname,
                UserGuid = resultUsuario.Content.UserGuid,
                Nombre = resultUsuario.Content.Nombre,
                Apellidos = resultUsuario.Content.Apellidos
            };
            Response.Cookies.Append(Cookies.USERLOGIN.ToString(), JSONHelper.JsonSerializer(cookieUsuario), new CookieOptions { Expires = DateTime.Now.AddDays(1) });

            return RedirectToAction("Index", "Home");
        }

    }
}
