using DSM.ERPerfect.BLL.Interfaces;
using DSM.ERPerfect.Helpers;
using DSM.ERPerfect.Models.Cookies;
using DSM.ERPerfect.Models.Entities;
using DSM.ERPerfect.Models.Errors;
using DSM.ERPerfect.Models.VM.BackOffice;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DSM.ERPerfect.WebApp.Controllers
{
    public class BackOfficeController : BaseController
    {
        #region Properties

        private readonly ILogger<BackOfficeController> _logger;
        private IUsuarioBusiness _usuarioBusiness { get; init; }
        private ITarifaBusiness _tarifaBusiness { get; init; }
        private IServicioBusiness _servicioBusiness { get; init; }

        #endregion

        #region Constructor

        public BackOfficeController(ILogger<BackOfficeController> logger
            , IUsuarioBusiness usuarioBusiness
            , ITarifaBusiness tarifaBusiness
            , IServicioBusiness servicioBusiness)
        {
            _logger = logger;
            _usuarioBusiness = usuarioBusiness;
            _tarifaBusiness = tarifaBusiness;
            _servicioBusiness = servicioBusiness;
        }

        #endregion

        #region Public Methods

        #region Actions

        public IActionResult Index()
        {
            IndexVM result = new IndexVM();
            result.TarifaVM = new TarifaVM();

            // Check cookie user
            CookieUsuario? cookieUsuario = CheckLoginCookie();
            if (cookieUsuario == null)
            {
                return RedirectToAction("Index", "Login");
            }
            result.CookieUsuario = cookieUsuario;

            // Get active tarifas
            ResultInfo<List<Tarifa>> resultTarifas = _tarifaBusiness.GetTarifas();
            if (resultTarifas.HasErrors)
            {
                LoggedErrors(_logger, resultTarifas.Errors);
                return SendErrorsToView(resultTarifas.Errors);
            }

            if (resultTarifas.Content != null)
            {
                result.TarifaVM.Tarifas = resultTarifas.Content.Where(x => x.FechaBaja == null || x.FechaBaja >= DateTime.Now).ToList();
                result.TarifaVM.Active = true;
            }

            return View(result);
        }

        #endregion

        #region AJAX Calls

        public IActionResult SaveTarifa(string descripcion, string precio)
        {
            // Check cookie user
            CookieUsuario? cookieUsuario = CheckLoginCookie();
            if (cookieUsuario == null)
            {
                List<ResultError> errors = new List<ResultError>() { new ResultError("Usuario no logueado", false, "BackOfficeController.SaveTarifa()") };
                return SendErrorsToView(errors);
            }

            if (string.IsNullOrEmpty(descripcion))
            {
                List<ResultError> errors = new List<ResultError>() { new ResultError("La descripción no puede ir vacía", false, "BackOfficeController.SaveTarifa()") };
                return SendErrorsToView(errors);
            }

            decimal precioParsed = 0.00M;
            if (string.IsNullOrEmpty(precio))
            {
                List<ResultError> errors = new List<ResultError>() { new ResultError("La precio no puede ir vacío", false, "BackOfficeController.SaveTarifa()") };
                return SendErrorsToView(errors);
            }
            else
            {
                precio = precio.Replace(".", ",");
                if (!decimal.TryParse(precio, out precioParsed))
                {
                    List<ResultError> errors = new List<ResultError>() { new ResultError("El formato introducido en el valor del precio no es correcto", false, "BackOfficeController.SaveTarifa()") };
                    return SendErrorsToView(errors);
                }
            }

            // Save tarifa
            Tarifa item = new Tarifa()
            {
                IdTarifa = 0,
                Descripcion = descripcion,
                Precio = precioParsed,
                FechaAlta = DateTime.Now
            };
            var resultSaveTarifa = _tarifaBusiness.NewTarifa(item);
            if (resultSaveTarifa.HasErrors)
            {
                LoggedErrors(_logger, resultSaveTarifa.Errors);
                return SendErrorsToView(resultSaveTarifa.Errors);
            }

            List<ResultError> errores = new List<ResultError>() { new ResultError("Tarifa creada correctamente", null, "Exito") };
            return StatusCode(200, errores);
        }

        public IActionResult DisabledTarifa(int id)
        {
            // Check cookie user
            CookieUsuario? cookieUsuario = CheckLoginCookie();
            if (cookieUsuario == null)
            {
                List<ResultError> errors = new List<ResultError>() { new ResultError("Usuario no logueado", false, "BackOfficeController.DisabledTarifa()") };
                return SendErrorsToView(errors);
            }

            if (id <= 0)
            {
                List<ResultError> errors = new List<ResultError>() { new ResultError("La tarifa seleccionada no está disponible", false, "BackOfficeController.DisabledTarifa()") };
                return SendErrorsToView(errors);
            }

            var resultTarifa = _tarifaBusiness.DisabledTarifa(id);
            if (resultTarifa.HasErrors)
            {
                LoggedErrors(_logger, resultTarifa.Errors);
                return SendErrorsToView(resultTarifa.Errors);
            }

            List<ResultError> errores = new List<ResultError>() { new ResultError("Tarifa desactivada correctamente", null, "Exito") };
            return StatusCode(200, errores);
        }

        public IActionResult EnabledTarifa(int id)
        {
            // Check cookie user
            CookieUsuario? cookieUsuario = CheckLoginCookie();
            if (cookieUsuario == null)
            {
                List<ResultError> errors = new List<ResultError>() { new ResultError("Usuario no logueado", false, "BackOfficeController.EnabledTarifa()") };
                return SendErrorsToView(errors);
            }

            if (id <= 0)
            {
                List<ResultError> errors = new List<ResultError>() { new ResultError("La tarifa seleccionada no está disponible", false, "BackOfficeController.EnabledTarifa()") };
                return SendErrorsToView(errors);
            }

            var resultTarifa = _tarifaBusiness.EnabledTarifa(id);
            if (resultTarifa.HasErrors)
            {
                LoggedErrors(_logger, resultTarifa.Errors);
                return SendErrorsToView(resultTarifa.Errors);
            }

            List<ResultError> errores = new List<ResultError>() { new ResultError("Tarifa activada correctamente", null, "Exito") };
            return StatusCode(200, errores);
        }
        
        public IActionResult EditarTarifa(int id, string descripcion, string precio)
        {
            // Check cookie user
            CookieUsuario? cookieUsuario = CheckLoginCookie();
            if (cookieUsuario == null)
            {
                List<ResultError> errors = new List<ResultError>() { new ResultError("Usuario no logueado", false, "BackOfficeController.EditarTarifa()") };
                return SendErrorsToView(errors);
            }

            if (id <= 0)
            {
                List<ResultError> errors = new List<ResultError>() { new ResultError("La tarifa seleccionada no está disponible", false, "BackOfficeController.EditarTarifa()") };
                return SendErrorsToView(errors);
            }

            if (string.IsNullOrEmpty(descripcion))
            {
                List<ResultError> errors = new List<ResultError>() { new ResultError("La descripción no puede ir vacía", false, "BackOfficeController.EditarTarifa()") };
                return SendErrorsToView(errors);
            }

            decimal precioParsed = 0.00M;
            if (string.IsNullOrEmpty(precio))
            {
                List<ResultError> errors = new List<ResultError>() { new ResultError("La precio no puede ir vacío", false, "BackOfficeController.EditarTarifa()") };
                return SendErrorsToView(errors);
            }
            else
            {
                precio = precio.Replace(".", ",");
                if (!decimal.TryParse(precio, out precioParsed))
                {
                    List<ResultError> errors = new List<ResultError>() { new ResultError("El formato introducido en el valor del precio no es correcto", false, "BackOfficeController.EditarTarifa()") };
                    return SendErrorsToView(errors);
                }
            }

            // Update tarifa
            Tarifa item = new Tarifa()
            {
                IdTarifa = id,
                Descripcion = descripcion,
                Precio = precioParsed,
                FechaAlta = DateTime.Now
            };
            var resultTarifa = _tarifaBusiness.UpdateTarifa(item);
            if (resultTarifa.HasErrors)
            {
                LoggedErrors(_logger, resultTarifa.Errors);
                return SendErrorsToView(resultTarifa.Errors);
            }

            List<ResultError> errores = new List<ResultError>() { new ResultError("Tarifa actualizada correctamente", null, "Exito") };
            return StatusCode(200, errores);
        }


        #endregion

        #region HTML Calls

        public IActionResult LoadTarifas(bool active)
        {
            TarifaVM result = new TarifaVM();
            result.Active = active;

            // Check cookie user
            CookieUsuario? cookieUsuario = CheckLoginCookie();
            if (cookieUsuario == null)
            {
                List<ResultError> errors = new List<ResultError>() { new ResultError("Usuario no logueado", false, "BackOfficeController.EnabledTarifa()") };
                return SendErrorsToView(errors);
            }

            // Get tarifas
            ResultInfo<List<Tarifa>> resultTarifas = _tarifaBusiness.GetTarifas();
            if (resultTarifas.HasErrors)
            {
                LoggedErrors(_logger, resultTarifas.Errors);
                return SendErrorsToView(resultTarifas.Errors);
            }

            if (resultTarifas.Content != null)
            {
                if (active)
                {
                    result.Tarifas = resultTarifas.Content.Where(x => x.FechaBaja == null || x.FechaBaja >= DateTime.Now).ToList();
                }
                else
                {
                    result.Tarifas = resultTarifas.Content.Where(x => x.FechaBaja < DateTime.Now).ToList();
                }
            }
            return PartialView("~/Views/BackOffice/Partials/_TablaTarifas.cshtml", result);
        }

        public IActionResult ShowEditarTarifa(int id)
        {
            // Check cookie user
            CookieUsuario? cookieUsuario = CheckLoginCookie();
            if (cookieUsuario == null)
            {
                List<ResultError> errors = new List<ResultError>() { new ResultError("Usuario no logueado", false, "BackOfficeController.ShowEditarTarifa()") };
                return SendErrorsToView(errors);
            }

            // Get tarifa by id
            ResultInfo<Tarifa> resultTarifa = _tarifaBusiness.GetTarifaById(id);
            if (resultTarifa.HasErrors)
            {
                LoggedErrors(_logger, resultTarifa.Errors);
                return SendErrorsToView(resultTarifa.Errors);
            }

            return PartialView("~/Views/BackOffice/Partials/_EditTarifaForm.cshtml", resultTarifa.Content);
        }

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
