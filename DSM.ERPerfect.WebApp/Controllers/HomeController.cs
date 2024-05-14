using DSM.ERPerfect.BLL;
using DSM.ERPerfect.BLL.Interfaces;
using DSM.ERPerfect.Helpers;
using DSM.ERPerfect.Models.Cookies;
using DSM.ERPerfect.Models.Entities;
using DSM.ERPerfect.Models.Errors;
using DSM.ERPerfect.Models.Statistics;
using DSM.ERPerfect.Models.VM.Home;
using Microsoft.AspNetCore.Mvc;

namespace DSM.ERPerfect.WebApp.Controllers
{
    public class HomeController : BaseController
    {
        #region Properties

        private readonly ILogger<HomeController> _logger;
        private IUsuarioBusiness _usuarioBusiness { get; init; }
        private IFacturaBusiness _facturaBusiness { get; init; }
        private IClienteBusiness _clienteBusiness { get; init; }

        #endregion

        #region Constructor

        public HomeController(ILogger<HomeController> logger
            , IUsuarioBusiness usuarioBusiness
            , IFacturaBusiness facturaBusiness
            , IClienteBusiness clienteBusiness)
        {
            _logger = logger;
            _usuarioBusiness = usuarioBusiness;
            _facturaBusiness = facturaBusiness;
            _clienteBusiness = clienteBusiness;
        }

        #endregion

        #region Public Methods

        #region Action Calls

        public IActionResult Index()
        {
            IndexVM result = new IndexVM();

            // Check cookie user
            CookieUsuario? cookieUsuario = CheckLoginCookie();
            if (cookieUsuario == null)
            {
                return RedirectToAction("Index", "Login");
            }
            result.CookieUsuario = cookieUsuario;

            // Get pending bills statistic
            ResultInfo<int> resultPendingBills = _facturaBusiness.GetPendingBills();
            if (resultPendingBills.HasErrors)
            {
                LoggedErrors(_logger, resultPendingBills.Errors);
                return SendErrorsToView(resultPendingBills.Errors);
            }
            result.TotalPendingBills = resultPendingBills.Content;

            return View(result);
        }

        public IActionResult Clients()
        {
            ClienteVM result = new ClienteVM();
            result.Active = true;

            // Check cookie user
            CookieUsuario? cookieUsuario = CheckLoginCookie();
            if (cookieUsuario == null)
            {
                return RedirectToAction("Index", "Login");
            }
            result.CookieUsuario = cookieUsuario;

            // Get clientes
            ResultInfo<List<Cliente>> resultClientes = _clienteBusiness.GetClientes();
            if (resultClientes.HasErrors)
            {
                LoggedErrors(_logger, resultClientes.Errors);
                return SendErrorsToView(resultClientes.Errors);
            }
            result.Clientes = resultClientes.Content;

            return View(result);
        }

        #endregion

        #region AJAX Calls

        public IActionResult SaveCliente(string nombre, string? apellidos, int? telefono, string? email, string? dni)
        {
            // Check cookie user
            CookieUsuario? cookieUsuario = CheckLoginCookie();
            if (cookieUsuario == null)
            {
                List<ResultError> errors = new List<ResultError>() { new ResultError("Usuario no logueado", false, "HomeController.SaveCliente()") };
                return SendErrorsToView(errors);
            }

            if (string.IsNullOrEmpty(nombre))
            {
                List<ResultError> errors = new List<ResultError>() { new ResultError("El nombre no puede ser vacío", false, "HomeController.SaveCliente()") };
                return SendErrorsToView(errors);
            }

            // Save cliente
            Cliente item = new Cliente()
            {
                IdCliente = 0,
                Nombre = nombre,
                Apellidos = apellidos,
                Telefono = telefono != null ? telefono.ToString() : null,
                Email = email,
                DNI = dni,
                FechaAlta = DateTime.Now
            };
            var resultSaveCliente = _clienteBusiness.NewCliente(item);
            if (resultSaveCliente.HasErrors)
            {
                LoggedErrors(_logger, resultSaveCliente.Errors);
                return SendErrorsToView(resultSaveCliente.Errors);
            }

            List<ResultError> errores = new List<ResultError>() { new ResultError("Cliente creado correctamente", null, "Exito") };
            return StatusCode(200, errores);
        }

        public IActionResult DisabledCliente(int id)
        {
            // Check cookie user
            CookieUsuario? cookieUsuario = CheckLoginCookie();
            if (cookieUsuario == null)
            {
                List<ResultError> errors = new List<ResultError>() { new ResultError("Usuario no logueado", false, "HomeController.DisabledCliente()") };
                return SendErrorsToView(errors);
            }

            if (id <= 0)
            {
                List<ResultError> errors = new List<ResultError>() { new ResultError("El cliente seleccionado no está disponible", false, "HomeController.DisabledCliente()") };
                return SendErrorsToView(errors);
            }

            var resultCliente = _clienteBusiness.DisabledCliente(id);
            if (resultCliente.HasErrors)
            {
                LoggedErrors(_logger, resultCliente.Errors);
                return SendErrorsToView(resultCliente.Errors);
            }

            List<ResultError> errores = new List<ResultError>() { new ResultError("Cliente desactivado correctamente", null, "Exito") };
            return StatusCode(200, errores);
        }

        public IActionResult EnabledCliente(int id)
        {
            // Check cookie user
            CookieUsuario? cookieUsuario = CheckLoginCookie();
            if (cookieUsuario == null)
            {
                List<ResultError> errors = new List<ResultError>() { new ResultError("Usuario no logueado", false, "HomeController.EnabledCliente()") };
                return SendErrorsToView(errors);
            }

            if (id <= 0)
            {
                List<ResultError> errors = new List<ResultError>() { new ResultError("El cliente seleccionado no está disponible", false, "HomeController.EnabledCliente()") };
                return SendErrorsToView(errors);
            }

            var resultCliente = _clienteBusiness.EnabledCliente(id);
            if (resultCliente.HasErrors)
            {
                LoggedErrors(_logger, resultCliente.Errors);
                return SendErrorsToView(resultCliente.Errors);
            }

            List<ResultError> errores = new List<ResultError>() { new ResultError("Cliente activado correctamente", null, "Exito") };
            return StatusCode(200, errores);
        }

        public IActionResult EditarCliente(int id, string nombre, string? apellidos, int? telefono, string? email, string? dni)
        {
            // Check cookie user
            CookieUsuario? cookieUsuario = CheckLoginCookie();
            if (cookieUsuario == null)
            {
                List<ResultError> errors = new List<ResultError>() { new ResultError("Usuario no logueado", false, "HomeController.EditarCliente()") };
                return SendErrorsToView(errors);
            }

            if (id <= 0)
            {
                List<ResultError> errors = new List<ResultError>() { new ResultError("El cliente seleccionada no está disponible", false, "HomeController.EditarCliente()") };
                return SendErrorsToView(errors);
            }

            if (string.IsNullOrEmpty(nombre))
            {
                List<ResultError> errors = new List<ResultError>() { new ResultError("El nombre no puede ir vacío", false, "HomeController.EditarCliente()") };
                return SendErrorsToView(errors);
            }

            // Update cliente
            Cliente item = new Cliente()
            {
                IdCliente = id,
                Nombre = nombre,
                Apellidos = apellidos,
                Telefono = telefono != null ? telefono.ToString() : null,
                Email = email,
                DNI = dni,
                FechaAlta = DateTime.Now
            };
            var resultCliente = _clienteBusiness.UpdateCliente(item);
            if (resultCliente.HasErrors)
            {
                LoggedErrors(_logger, resultCliente.Errors);
                return SendErrorsToView(resultCliente.Errors);
            }

            List<ResultError> errores = new List<ResultError>() { new ResultError("Cliente actualizado correctamente", null, "Exito") };
            return StatusCode(200, errores);
        }

        #endregion

        #region HTML Calls

        #region Statistics

        public IActionResult GetPaymentBills()
        {
            // Check cookie user
            CookieUsuario? cookieUsuario = CheckLoginCookie();
            if (cookieUsuario == null)
            {
                return RedirectToAction("Index", "Login");
            }

            // Get completed bills
            ResultInfo<List<PaymentBills>> resultCompleteBills = _facturaBusiness.GetPaymentBills();
            if (resultCompleteBills.HasErrors)
            {
                LoggedErrors(_logger, resultCompleteBills.Errors);
                return SendErrorsToView(resultCompleteBills.Errors);
            }

            return PartialView("~/Views/Home/Partials/_ChartFacturacionCompletaAnual.cshtml", resultCompleteBills.Content);
        }

        public IActionResult GetTop5ServicioMes()
        {
            // Check cookie user
            CookieUsuario? cookieUsuario = CheckLoginCookie();
            if (cookieUsuario == null)
            {
                return RedirectToAction("Index", "Login");
            }

            // Get completed bills
            ResultInfo<List<Top5Servicios>> resultTop5Servicios = _facturaBusiness.GetTop5ServicioMes();
            if (resultTop5Servicios.HasErrors)
            {
                LoggedErrors(_logger, resultTop5Servicios.Errors);
                return SendErrorsToView(resultTop5Servicios.Errors);
            }

            return PartialView("~/Views/Home/Partials/_ChartTop5Servicios.cshtml", resultTop5Servicios.Content);
        }

        #endregion

        #region Client

        public IActionResult LoadClientes(bool active)
        {
            ClienteVM result = new ClienteVM();
            result.Active = active;

            // Check cookie user
            CookieUsuario? cookieUsuario = CheckLoginCookie();
            if (cookieUsuario == null)
            {
                List<ResultError> errors = new List<ResultError>() { new ResultError("Usuario no logueado", false, "HomeController.EnabledCliente()") };
                return SendErrorsToView(errors);
            }

            // Get clientes
            ResultInfo<List<Cliente>> resultClientes = _clienteBusiness.GetClientes();
            if (resultClientes.HasErrors)
            {
                LoggedErrors(_logger, resultClientes.Errors);
                return SendErrorsToView(resultClientes.Errors);
            }

            if (resultClientes.Content != null)
            {
                if (active)
                {
                    result.Clientes = resultClientes.Content.Where(x => x.FechaBaja == null || x.FechaBaja >= DateTime.Now).ToList();
                }
                else
                {
                    result.Clientes = resultClientes.Content.Where(x => x.FechaBaja == null || x.FechaBaja < DateTime.Now).ToList();
                }
            }
            return PartialView("~/Views/Home/Partials/_TablaClientes.cshtml", result);
        }

        public IActionResult ShowEditarCliente(int id)
        {
            // Check cookie user
            CookieUsuario? cookieUsuario = CheckLoginCookie();
            if (cookieUsuario == null)
            {
                List<ResultError> errors = new List<ResultError>() { new ResultError("Usuario no logueado", false, "HomeController.ShowEditarCliente()") };
                return SendErrorsToView(errors);
            }

            // Get cliente by id
            ResultInfo<Cliente> resultCliente = _clienteBusiness.GetClienteById(id);
            if (resultCliente.HasErrors)
            {
                LoggedErrors(_logger, resultCliente.Errors);
                return SendErrorsToView(resultCliente.Errors);
            }

            return PartialView("~/Views/Home/Partials/_EditClienteForm.cshtml", resultCliente.Content);
        }

        #endregion

        #endregion

        #endregion

        #region Private Methods

        public CookieUsuario? CheckLoginCookie()
        {
            CookieUsuario? cookieUsuario = null;

            string? cookie = Request.Cookies[Cookies.USERLOGIN.ToString()];
            if (!string.IsNullOrEmpty(cookie))
            {
                ResultInfo<CookieUsuario> resCookie = JSONHelper.JsonDeserializer<CookieUsuario>(cookie);
                if (resCookie.HasErrors)
                {
                    return cookieUsuario;
                }

                // Get user
                ResultInfo<Usuario> resultUsuario = _usuarioBusiness.GetUsuarioByRowGUID(resCookie.Content.RowGuid);
                if (resultUsuario.HasErrors)
                {
                    LoggedErrors(_logger, resultUsuario.Errors);
                    return cookieUsuario;
                }

                // Max 5 tries
                if (resultUsuario.Content.Intentos > 5)
                {
                    return cookieUsuario;
                }

                cookieUsuario = resCookie.Content;
            }

            return cookieUsuario;
        }

        #endregion
    }
}
