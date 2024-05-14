using DSM.ERPerfect.BLL.Interfaces;
using DSM.ERPerfect.Helpers;
using DSM.ERPerfect.Models.Cookies;
using DSM.ERPerfect.Models.Entities;
using DSM.ERPerfect.Models.Errors;
using DSM.ERPerfect.Models.VM.Login;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DSM.ERPerfect.WebApp.Controllers
{
    public class LoginController : BaseController
    {
        private readonly ILogger<LoginController> _logger;
        private IUsuarioBusiness _usuariosBusiness { get; init; }
        private IRolBusiness _rolBusiness { get; init; }
        private IBannedIPBusiness _bannedIPBusiness { get; init; }

        public LoginController(ILogger<LoginController> logger
            , IUsuarioBusiness usuariosBusiness
            , IRolBusiness rolBusiness
            , IBannedIPBusiness bannedIPBusiness)
        {
            _logger = logger;
            _usuariosBusiness = usuariosBusiness;
            _rolBusiness = rolBusiness;
            _bannedIPBusiness = bannedIPBusiness;
        }

        public IActionResult Index()
        {
            DeleteCookies();

            return View();
        }

        [ValidateAntiForgeryToken]
        public IActionResult LogIn(LoginVM login)
        {
            // Check user with database
            ResultInfo<Usuario> resultUsuario = _usuariosBusiness.GetUsuarioByLogin(login.UserName);
            if (resultUsuario.HasErrors)
            {
                LoggedErrors(_logger, resultUsuario.Errors);
                return StatusCode(StatusCodes.Status401Unauthorized, "Credenciales inválidas");
            }

            // Check if user is still active
            if(resultUsuario.Content.FechaBaja != null)
            {
                if(resultUsuario.Content.FechaBaja <= DateTime.Now)
                {
                    List<ResultError> errors = new List<ResultError>();
                    errors.Add(new ResultError($"El usuario con login '{login.UserName}' está dado de baja desde el día: '{resultUsuario.Content.FechaBaja.Value.ToString("dd/MM/yyyy HH:mm:ss")}'", false, "LoginController.LogIn(LoginVM)"));
                    LoggedErrors(_logger, errors);
                    return StatusCode(StatusCodes.Status401Unauthorized, "Credenciales inválidas");
                }
            }

            // Check login limits
            if (resultUsuario.Content.Intentos >= 10)
            {
                // Ban request IP
                string ip = Request.HttpContext.Connection.RemoteIpAddress != null ? Request.HttpContext.Connection.RemoteIpAddress.ToString() : "";
                ResultInfo<int> resultBannedIP = _bannedIPBusiness.NewBannedIP(ip);
                if (resultBannedIP.HasErrors)
                {
                    LoggedErrors(_logger, resultBannedIP.Errors);
                    return StatusCode(StatusCodes.Status500InternalServerError, "Ha habido un error, por favor, contacte con el administrador del sistema.");
                }
            }
            else if(resultUsuario.Content.Intentos >= 5)
            {
                // Block user
                resultUsuario.Content.Intentos++;
                resultUsuario.Content.FechaUltimoLogin = DateTime.Now;
                resultUsuario.Content.FechaBloqueo = DateTime.Now;
                ResultInfo<int> resultLimitLogin = _usuariosBusiness.UpdateUsuario(resultUsuario.Content);
                if (resultLimitLogin.HasErrors)
                {
                    LoggedErrors(_logger, resultLimitLogin.Errors);
                    return StatusCode(StatusCodes.Status500InternalServerError, "Ha habido un error, por favor, contacte con el administrador del sistema.");
                }

                List<ResultError> errors = new List<ResultError>();
                errors.Add(new ResultError($"Intentos superados para el usuario '{login.UserName}'", false, "LoginController.LogIn(LoginVM)"));
                LoggedErrors(_logger, errors);
                return StatusCode(StatusCodes.Status401Unauthorized, "Credenciales inválidas");
            }

            // Match password
            byte[] derivedKey = EncryptHelper.GenerateDerivedKey(login.Password);
            string passwordHashed = EncryptHelper.EncryptPassword(login.Password, derivedKey);
            if(passwordHashed != resultUsuario.Content.Password)
            {
                // Increase tries to the user
                resultUsuario.Content.Intentos++;
                resultUsuario.Content.FechaUltimoLogin = DateTime.Now;
                ResultInfo<int> resultLimitLogin = _usuariosBusiness.UpdateUsuario(resultUsuario.Content);
                if(resultLimitLogin.HasErrors)
                {
                    LoggedErrors(_logger, resultLimitLogin.Errors);
                    return StatusCode(StatusCodes.Status500InternalServerError, "Ha habido un error, por favor, contacte con el administrador del sistema.");
                }

                List<ResultError> errors = new List<ResultError>();
                errors.Add(new ResultError($"Intento de login para el usuario '{login.UserName}' fallido", false, "LoginController.LogIn(LoginVM)"));
                LoggedErrors(_logger, errors);
                return StatusCode(StatusCodes.Status401Unauthorized, "Credenciales inválidas");
            }
            else
            {
                // Reset tries
                resultUsuario.Content.Intentos = 0;
                resultUsuario.Content.FechaUltimoLogin = DateTime.Now;
                resultUsuario.Content.FechaBloqueo = null;
                ResultInfo<int> resultLimitLogin = _usuariosBusiness.UpdateUsuario(resultUsuario.Content);
                if (resultLimitLogin.HasErrors)
                {
                    LoggedErrors(_logger, resultLimitLogin.Errors);
                    return StatusCode(StatusCodes.Status500InternalServerError, "Ha habido un error, por favor, contacte con el administrador del sistema.");
                }
            }

            // Save cookie user information
            CookieUsuario cookieUsuario = new CookieUsuario()
            {
                Login = resultUsuario.Content.Login,
                RowGuid = resultUsuario.Content.RowGUID,
                Nombre = resultUsuario.Content.Nombre,
                Apellidos = resultUsuario.Content.Apellidos
            };

            // Get Roles
            var resultRoles = _rolBusiness.GetRoles();
            if (resultRoles.HasErrors)
            {
                LoggedErrors(_logger, resultRoles.Errors);
                return StatusCode(StatusCodes.Status500InternalServerError, "Ha habido un error, por favor, contacte con el administrador del sistema.");
            }

            // Set rol description to cookie user 
            if(resultRoles.Content != null)
            {
                if(resultRoles.Content.Count > 0)
                {
                    Rol? rol = resultRoles.Content.Where(x => x.IdRol == resultUsuario.Content.IdRol).FirstOrDefault();
                    if(rol != null)
                    {
                        cookieUsuario.Rol = rol.Descripcion;
                    }
                }
            }

            Response.Cookies.Append(Cookies.USERLOGIN.ToString(), JSONHelper.JsonSerializer(cookieUsuario), new CookieOptions { Expires = DateTime.Now.AddDays(1) });

            return RedirectToAction("Index", "Home");
        }

    }
}
