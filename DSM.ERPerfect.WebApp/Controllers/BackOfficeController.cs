using DSM.ERPerfect.BLL.Interfaces;
using DSM.ERPerfect.Helpers;
using DSM.ERPerfect.Models.Cookies;
using DSM.ERPerfect.Models.Entities;
using DSM.ERPerfect.Models.Errors;
using DSM.ERPerfect.Models.Queries;
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
        private ITarifaServicioBusiness _tarifaServicioBusiness { get; init; }
        private IRolBusiness _rolBusiness { get; init; }

        #endregion

        #region Constructor

        public BackOfficeController(ILogger<BackOfficeController> logger
            , IUsuarioBusiness usuarioBusiness
            , ITarifaBusiness tarifaBusiness
            , IServicioBusiness servicioBusiness
            , ITarifaServicioBusiness tarifaServicioBusiness
            , IRolBusiness rolBusiness)
        {
            _logger = logger;
            _usuarioBusiness = usuarioBusiness;
            _tarifaBusiness = tarifaBusiness;
            _servicioBusiness = servicioBusiness;
            _tarifaServicioBusiness = tarifaServicioBusiness;
            _rolBusiness = rolBusiness;
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

        #region Tarifas

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

        #region Servicios

        public IActionResult SaveServicio(string descripcion, int idTarifa)
        {
            // Check cookie user
            CookieUsuario? cookieUsuario = CheckLoginCookie();
            if (cookieUsuario == null)
            {
                List<ResultError> errors = new List<ResultError>() { new ResultError("Usuario no logueado", false, "BackOfficeController.SaveServicio()") };
                return SendErrorsToView(errors);
            }

            if (string.IsNullOrEmpty(descripcion))
            {
                List<ResultError> errors = new List<ResultError>() { new ResultError("La descripción no puede ir vacía", false, "BackOfficeController.SaveServicio()") };
                return SendErrorsToView(errors);
            }

            if (idTarifa <= 0)
            {
                List<ResultError> errors = new List<ResultError>() { new ResultError("Es necesario seleccionar una tarifa", false, "BackOfficeController.SaveServicio()") };
                return SendErrorsToView(errors);
            }

            // Save servicio
            Servicio item = new Servicio()
            {
                IdServicio = 0,
                Descripcion = descripcion,
                FechaAlta = DateTime.Now
            };
            var resultSaveServicio = _servicioBusiness.NewServicio(item);
            if (resultSaveServicio.HasErrors)
            {
                LoggedErrors(_logger, resultSaveServicio.Errors);
                return SendErrorsToView(resultSaveServicio.Errors);
            }

            // Save TarifaServicio
            TarifaServicio itemTarifaServicio = new TarifaServicio()
            {
                IdTarifaServicio = 0,
                IdServicio = resultSaveServicio.Content,
                IdTarifa = idTarifa,
                FechaAlta = DateTime.Now
            };
            var resultSaveTarifaServicio = _tarifaServicioBusiness.NewTarifaServicio(itemTarifaServicio);
            if (resultSaveTarifaServicio.HasErrors)
            {
                LoggedErrors(_logger, resultSaveTarifaServicio.Errors);
                return SendErrorsToView(resultSaveTarifaServicio.Errors);
            }

            List<ResultError> errores = new List<ResultError>() { new ResultError("Servicio creado correctamente", null, "Exito") };
            return StatusCode(200, errores);
        }

        public IActionResult DisabledServicio(int idTarifa, int idServicio)
        {
            // Check cookie user
            CookieUsuario? cookieUsuario = CheckLoginCookie();
            if (cookieUsuario == null)
            {
                List<ResultError> errors = new List<ResultError>() { new ResultError("Usuario no logueado", false, "BackOfficeController.DisabledServicio()") };
                return SendErrorsToView(errors);
            }

            if (idTarifa <= 0)
            {
                List<ResultError> errors = new List<ResultError>() { new ResultError("La tarifa seleccionada no está disponible", false, "BackOfficeController.DisabledServicio()") };
                return SendErrorsToView(errors);
            }

            if (idServicio <= 0)
            {
                List<ResultError> errors = new List<ResultError>() { new ResultError("El servicio seleccionado no está disponible", false, "BackOfficeController.DisabledServicio()") };
                return SendErrorsToView(errors);
            }

            // Get TarifaServicio
            ResultInfo<List<TarifaServicioQuery>> resultTarifaServicio = _tarifaServicioBusiness.GetTarifaServicio();
            if (resultTarifaServicio.HasErrors)
            {
                LoggedErrors(_logger, resultTarifaServicio.Errors);
                return SendErrorsToView(resultTarifaServicio.Errors);
            }
            TarifaServicioQuery? tarifaServicio = resultTarifaServicio.Content.Where(x => x.IdServicio == idServicio && x.IdTarifa == idTarifa).FirstOrDefault();

            if (tarifaServicio != null)
            {
                var resultServicio = _servicioBusiness.DisabledServicio(tarifaServicio.IdServicio);
                if (resultServicio.HasErrors)
                {
                    LoggedErrors(_logger, resultServicio.Errors);
                    return SendErrorsToView(resultServicio.Errors);
                }

                var resultDisableTarifaServicio = _tarifaServicioBusiness.DisabledTarifaServicio(tarifaServicio.IdTarifaServicio);
                if (resultDisableTarifaServicio.HasErrors)
                {
                    LoggedErrors(_logger, resultDisableTarifaServicio.Errors);
                    return SendErrorsToView(resultDisableTarifaServicio.Errors);
                }
            }
            else
            {
                List<ResultError> errors = new List<ResultError>() { new ResultError($"No existe ninguna TarifaServicio con IdServicio: '{idServicio}' e IdTarifa: '{idTarifa}'", false, "BackOfficeController.DisabledServicio()") };
                return SendErrorsToView(errors);
            }

            List<ResultError> errores = new List<ResultError>() { new ResultError("Servicio desactivado correctamente", null, "Exito") };
            return StatusCode(200, errores);
        }

        public IActionResult EnabledServicio(int idTarifa, int idServicio)
        {
            // Check cookie user
            CookieUsuario? cookieUsuario = CheckLoginCookie();
            if (cookieUsuario == null)
            {
                List<ResultError> errors = new List<ResultError>() { new ResultError("Usuario no logueado", false, "BackOfficeController.EnabledServicio()") };
                return SendErrorsToView(errors);
            }

            if (idTarifa <= 0)
            {
                List<ResultError> errors = new List<ResultError>() { new ResultError("La tarifa seleccionada no está disponible", false, "BackOfficeController.EnabledServicio()") };
                return SendErrorsToView(errors);
            }

            if (idServicio <= 0)
            {
                List<ResultError> errors = new List<ResultError>() { new ResultError("El servicio seleccionado no está disponible", false, "BackOfficeController.EnabledServicio()") };
                return SendErrorsToView(errors);
            }

            // Get TarifaServicio
            ResultInfo<List<TarifaServicioQuery>> resultTarifaServicio = _tarifaServicioBusiness.GetTarifaServicio();
            if (resultTarifaServicio.HasErrors)
            {
                LoggedErrors(_logger, resultTarifaServicio.Errors);
                return SendErrorsToView(resultTarifaServicio.Errors);
            }
            TarifaServicioQuery? tarifaServicio = resultTarifaServicio.Content.Where(x => x.IdServicio == idServicio && x.IdTarifa == idTarifa).FirstOrDefault();

            if (tarifaServicio != null)
            {
                var resultServicio = _servicioBusiness.EnabledServicio(tarifaServicio.IdServicio);
                if (resultServicio.HasErrors)
                {
                    LoggedErrors(_logger, resultServicio.Errors);
                    return SendErrorsToView(resultServicio.Errors);
                }

                var resultEnableTarifaServicio = _tarifaServicioBusiness.EnabledTarifaServicio(tarifaServicio.IdTarifaServicio);
                if (resultEnableTarifaServicio.HasErrors)
                {
                    LoggedErrors(_logger, resultEnableTarifaServicio.Errors);
                    return SendErrorsToView(resultEnableTarifaServicio.Errors);
                }
            }
            else
            {
                List<ResultError> errors = new List<ResultError>() { new ResultError($"No existe ninguna TarifaServicio con IdServicio: '{idServicio}' e IdTarifa: '{idTarifa}'", false, "BackOfficeController.EnabledServicio()") };
                return SendErrorsToView(errors);
            }

            List<ResultError> errores = new List<ResultError>() { new ResultError("Servicio activado correctamente", null, "Exito") };
            return StatusCode(200, errores);
        }

        public IActionResult EditarServicio(int id, string descripcion, int idTarifa, int idServicio)
        {
            // Check cookie user
            CookieUsuario? cookieUsuario = CheckLoginCookie();
            if (cookieUsuario == null)
            {
                List<ResultError> errors = new List<ResultError>() { new ResultError("Usuario no logueado", false, "BackOfficeController.EditarServicio()") };
                return SendErrorsToView(errors);
            }

            if (id <= 0 || idServicio <= 0)
            {
                List<ResultError> errors = new List<ResultError>() { new ResultError("La servicio seleccionado no está disponible", false, "BackOfficeController.EditarServicio()") };
                return SendErrorsToView(errors);
            }

            if (idTarifa <= 0)
            {
                List<ResultError> errors = new List<ResultError>() { new ResultError("Es necesario seleccionar una tarifa", false, "BackOfficeController.EditarServicio()") };
                return SendErrorsToView(errors);
            }

            if (string.IsNullOrEmpty(descripcion))
            {
                List<ResultError> errors = new List<ResultError>() { new ResultError("La descripción no puede ir vacía", false, "BackOfficeController.EditarServicio()") };
                return SendErrorsToView(errors);
            }

            // Update servicio
            Servicio item = new Servicio()
            {
                IdServicio = idServicio,
                Descripcion = descripcion,
                FechaAlta = DateTime.Now
            };
            var resultServicio = _servicioBusiness.UpdateServicio(item);
            if (resultServicio.HasErrors)
            {
                LoggedErrors(_logger, resultServicio.Errors);
                return SendErrorsToView(resultServicio.Errors);
            }

            // Update TarifaServicio
            TarifaServicio itemTarifaServicio = new TarifaServicio()
            {
                IdTarifaServicio = id,
                IdServicio = idServicio,
                IdTarifa = idTarifa,
                FechaAlta = DateTime.Now
            };
            var resultTarifaServicio = _tarifaServicioBusiness.UpdateTarifaServicio(itemTarifaServicio);
            if (resultTarifaServicio.HasErrors)
            {
                LoggedErrors(_logger, resultTarifaServicio.Errors);
                return SendErrorsToView(resultTarifaServicio.Errors);
            }

            List<ResultError> errores = new List<ResultError>() { new ResultError("Servicio actualizado correctamente", null, "Exito") };
            return StatusCode(200, errores);
        }

        #endregion

        #region Usuarios

        public IActionResult SaveUsuario(string login, string password, string nombre, string apellidos, int idRol)
        {
            // Check cookie user
            CookieUsuario? cookieUsuario = CheckLoginCookie();
            if (cookieUsuario == null)
            {
                List<ResultError> errors = new List<ResultError>() { new ResultError("Usuario no logueado", false, "BackOfficeController.SaveUsuario()") };
                return SendErrorsToView(errors);
            }

            if (string.IsNullOrEmpty(login))
            {
                List<ResultError> errors = new List<ResultError>() { new ResultError("El login no puede ir vacío", false, "BackOfficeController.SaveUsuario()") };
                return SendErrorsToView(errors);
            }

            if (string.IsNullOrEmpty(password))
            {
                List<ResultError> errors = new List<ResultError>() { new ResultError("El password no puede ir vacío", false, "BackOfficeController.SaveUsuario()") };
                return SendErrorsToView(errors);
            }

            if (string.IsNullOrEmpty(nombre))
            {
                List<ResultError> errors = new List<ResultError>() { new ResultError("El nombre no puede ir vacío", false, "BackOfficeController.SaveUsuario()") };
                return SendErrorsToView(errors);
            }

            if (string.IsNullOrEmpty(apellidos))
            {
                List<ResultError> errors = new List<ResultError>() { new ResultError("Los apellidos no puede ir vacios", false, "BackOfficeController.SaveUsuario()") };
                return SendErrorsToView(errors);
            }

            if (idRol <= 0)
            {
                List<ResultError> errors = new List<ResultError>() { new ResultError("Es necesario seleccionar un Rol", false, "BackOfficeController.SaveUsuario()") };
                return SendErrorsToView(errors);
            }

            // Save usuario
            Usuario item = new Usuario()
            {
                IdUsuario = 0,
                IdRol = idRol,
                Login = login,
                Password = password,
                Nombre = nombre,
                Apellidos = apellidos,
                FechaAlta = DateTime.Now,
                RowGUID = new Guid(),
                Intentos = 0
            };
            var resultSaveUsuario = _usuarioBusiness.NewUsuario(item);
            if (resultSaveUsuario.HasErrors)
            {
                LoggedErrors(_logger, resultSaveUsuario.Errors);
                return SendErrorsToView(resultSaveUsuario.Errors);
            }

            List<ResultError> errores = new List<ResultError>() { new ResultError("Usuario creado correctamente", null, "Exito") };
            return StatusCode(200, errores);
        }

        public IActionResult DisabledUsuario(int idUsuario)
        {
            // Check cookie user
            CookieUsuario? cookieUsuario = CheckLoginCookie();
            if (cookieUsuario == null)
            {
                List<ResultError> errors = new List<ResultError>() { new ResultError("Usuario no logueado", false, "BackOfficeController.DisabledUsuario()") };
                return SendErrorsToView(errors);
            }

            if (idUsuario <= 0)
            {
                List<ResultError> errors = new List<ResultError>() { new ResultError("El usuario seleccionado no está disponible", false, "BackOfficeController.DisabledUsuario()") };
                return SendErrorsToView(errors);
            }

            // Disable usuario
            ResultInfo<int> resultDisabledUsuario = _usuarioBusiness.DisabledUsuario(idUsuario);
            if (resultDisabledUsuario.HasErrors)
            {
                LoggedErrors(_logger, resultDisabledUsuario.Errors);
                return SendErrorsToView(resultDisabledUsuario.Errors);
            }

            List<ResultError> errores = new List<ResultError>() { new ResultError("Usuario desactivado correctamente", null, "Exito") };
            return StatusCode(200, errores);
        }

        public IActionResult EnabledUsuario(int idUsuario)
        {
            // Check cookie user
            CookieUsuario? cookieUsuario = CheckLoginCookie();
            if (cookieUsuario == null)
            {
                List<ResultError> errors = new List<ResultError>() { new ResultError("Usuario no logueado", false, "BackOfficeController.EnabledUsuario()") };
                return SendErrorsToView(errors);
            }

            if (idUsuario <= 0)
            {
                List<ResultError> errors = new List<ResultError>() { new ResultError("El usuario seleccionado no está disponible", false, "BackOfficeController.EnabledUsuario()") };
                return SendErrorsToView(errors);
            }

            // Get TarifaUsuario
            ResultInfo<int> resultEnabledUsuario = _usuarioBusiness.EnabledUsuario(idUsuario);
            if (resultEnabledUsuario.HasErrors)
            {
                LoggedErrors(_logger, resultEnabledUsuario.Errors);
                return SendErrorsToView(resultEnabledUsuario.Errors);
            }

            List<ResultError> errores = new List<ResultError>() { new ResultError("Usuario activado correctamente", null, "Exito") };
            return StatusCode(200, errores);
        }

        public IActionResult EditarUsuario(int idUsuario, string login, string password, string nombre, string apellidos, int idRol)
        {
            // Check cookie user
            CookieUsuario? cookieUsuario = CheckLoginCookie();
            if (cookieUsuario == null)
            {
                List<ResultError> errors = new List<ResultError>() { new ResultError("Usuario no logueado", false, "BackOfficeController.EditarUsuario()") };
                return SendErrorsToView(errors);
            }

            if (string.IsNullOrEmpty(login))
            {
                List<ResultError> errors = new List<ResultError>() { new ResultError("El login no puede ir vacío", false, "BackOfficeController.EditarUsuario()") };
                return SendErrorsToView(errors);
            }

            if (string.IsNullOrEmpty(password))
            {
                List<ResultError> errors = new List<ResultError>() { new ResultError("El password no puede ir vacío", false, "BackOfficeController.EditarUsuario()") };
                return SendErrorsToView(errors);
            }

            if (string.IsNullOrEmpty(nombre))
            {
                List<ResultError> errors = new List<ResultError>() { new ResultError("El nombre no puede ir vacío", false, "BackOfficeController.EditarUsuario()") };
                return SendErrorsToView(errors);
            }

            if (string.IsNullOrEmpty(apellidos))
            {
                List<ResultError> errors = new List<ResultError>() { new ResultError("Los apellidos no puede ir vacios", false, "BackOfficeController.EditarUsuario()") };
                return SendErrorsToView(errors);
            }

            if (idRol <= 0)
            {
                List<ResultError> errors = new List<ResultError>() { new ResultError("Es necesario seleccionar un Rol", false, "BackOfficeController.EditarUsuario()") };
                return SendErrorsToView(errors);
            }

            if (idUsuario <= 0)
            {
                List<ResultError> errors = new List<ResultError>() { new ResultError("El usuario seleccionado no está disponible", false, "BackOfficeController.EditarUsuario()") };
                return SendErrorsToView(errors);
            }

            // Update usuario
            Usuario item = new Usuario()
            {
                IdUsuario = idUsuario,
                IdRol = idRol,
                Login = login,
                Password = password,
                Nombre = nombre,
                Apellidos = apellidos,
                FechaAlta = DateTime.Now,
                RowGUID = new Guid(),
                Intentos = 0
            };
            var resultSaveUsuario = _usuarioBusiness.UpdateUsuario(item, true);
            if (resultSaveUsuario.HasErrors)
            {
                LoggedErrors(_logger, resultSaveUsuario.Errors);
                return SendErrorsToView(resultSaveUsuario.Errors);
            }

            List<ResultError> errores = new List<ResultError>() { new ResultError("Usuario actualizado correctamente", null, "Exito") };
            return StatusCode(200, errores);
        }

        #endregion

        #endregion

        #region HTML Calls

        #region Tarifas

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

        #region Servicios

        public IActionResult LoadServicios(bool active)
        {
            ServicioVM result = new ServicioVM();
            result.Active = active;

            // Check cookie user
            CookieUsuario? cookieUsuario = CheckLoginCookie();
            if (cookieUsuario == null)
            {
                List<ResultError> errors = new List<ResultError>() { new ResultError("Usuario no logueado", false, "BackOfficeController.EnabledServicio()") };
                return SendErrorsToView(errors);
            }

            // Get tarifas
            ResultInfo<List<Tarifa>> resultTarifas = _tarifaBusiness.GetTarifas();
            if (resultTarifas.HasErrors)
            {
                LoggedErrors(_logger, resultTarifas.Errors);
                return SendErrorsToView(resultTarifas.Errors);
            }
            result.Tarifas = resultTarifas.Content;

            // Get servicios
            ResultInfo<List<TarifaServicioQuery>> resultServicios = _tarifaServicioBusiness.GetTarifaServicio();
            if (resultServicios.HasErrors)
            {
                LoggedErrors(_logger, resultServicios.Errors);
                return SendErrorsToView(resultServicios.Errors);
            }

            if (resultServicios.Content != null)
            {
                if (active)
                {
                    result.Servicios = resultServicios.Content.Where(x => x.FechaBaja == null || x.FechaBaja >= DateTime.Now).ToList();
                }
                else
                {
                    result.Servicios = resultServicios.Content.Where(x => x.FechaBaja < DateTime.Now).ToList();
                }
            }
            return PartialView("~/Views/BackOffice/Partials/_TablaServicios.cshtml", result);
        }

        public IActionResult ShowEditarServicio(int idServicio, int idTarifa)
        {
            ServicioVM result = new ServicioVM();

            // Check cookie user
            CookieUsuario? cookieUsuario = CheckLoginCookie();
            if (cookieUsuario == null)
            {
                List<ResultError> errors = new List<ResultError>() { new ResultError("Usuario no logueado", false, "BackOfficeController.ShowEditarServicio()") };
                return SendErrorsToView(errors);
            }

            // Get TarifaServicio
            ResultInfo<List<TarifaServicioQuery>> resultTarifaServicio = _tarifaServicioBusiness.GetTarifaServicio();
            if (resultTarifaServicio.HasErrors)
            {
                LoggedErrors(_logger, resultTarifaServicio.Errors);
                return SendErrorsToView(resultTarifaServicio.Errors);
            }
            TarifaServicioQuery? tarifaServicio = resultTarifaServicio.Content.Where(x => x.IdServicio == idServicio && x.IdTarifa == idTarifa).FirstOrDefault();

            // Get Tarifas
            if (tarifaServicio != null)
            {
                ResultInfo<List<Tarifa>> resultTarifa = _tarifaBusiness.GetTarifas();
                if (resultTarifa.HasErrors)
                {
                    LoggedErrors(_logger, resultTarifa.Errors);
                    return SendErrorsToView(resultTarifa.Errors);
                }

                if(resultTarifa.Content != null)
                {
                    result.Tarifas = resultTarifa.Content;
                }
                result.TarifaServicio = tarifaServicio;

                return PartialView("~/Views/BackOffice/Partials/_EditServicioForm.cshtml", result);
            }
            else
            {
                List<ResultError> errors = new List<ResultError>() { new ResultError($"No existe ninguna TarifaServicio con IdServicio: '{idServicio}' e IdTarifa: '{idTarifa}'", false, "BackOfficeController.ShowEditarServicio()") };
                return SendErrorsToView(errors);
            }
        }

        #endregion

        #region Usuarios

        public IActionResult LoadUsuarios(bool active)
        {
            UsuarioVM result = new UsuarioVM();
            result.Active = active;

            // Check cookie user
            CookieUsuario? cookieUsuario = CheckLoginCookie();
            if (cookieUsuario == null)
            {
                List<ResultError> errors = new List<ResultError>() { new ResultError("Usuario no logueado", false, "BackOfficeController.EnabledUsuario()") };
                return SendErrorsToView(errors);
            }

            // Get roles
            ResultInfo<List<Rol>> resultRoles = _rolBusiness.GetRoles();
            if (resultRoles.HasErrors)
            {
                LoggedErrors(_logger, resultRoles.Errors);
                return SendErrorsToView(resultRoles.Errors);
            }
            result.Roles = resultRoles.Content;

            // Get usuarios
            ResultInfo<List<Usuario>> resultUsuarios = _usuarioBusiness.GetUsuarios();
            if (resultUsuarios.HasErrors)
            {
                LoggedErrors(_logger, resultUsuarios.Errors);
                return SendErrorsToView(resultUsuarios.Errors);
            }

            if (resultUsuarios.Content != null)
            {
                if (active)
                {
                    result.Usuarios = resultUsuarios.Content.Where(x => x.FechaBaja == null || x.FechaBaja >= DateTime.Now).ToList();
                }
                else
                {
                    result.Usuarios = resultUsuarios.Content.Where(x => x.FechaBaja < DateTime.Now).ToList();
                }
            }
            return PartialView("~/Views/BackOffice/Partials/_TablaUsuarios.cshtml", result);
        }

        public IActionResult ShowEditarUsuario(int idUsuario)
        {
            UsuarioVM result = new UsuarioVM();

            // Check cookie user
            CookieUsuario? cookieUsuario = CheckLoginCookie();
            if (cookieUsuario == null)
            {
                List<ResultError> errors = new List<ResultError>() { new ResultError("Usuario no logueado", false, "BackOfficeController.ShowEditarUsuario()") };
                return SendErrorsToView(errors);
            }

            // Get TarifaUsuario
            ResultInfo<Usuario> resultUsuario = _usuarioBusiness.GetUsuarioById(idUsuario);
            if (resultUsuario.HasErrors)
            {
                LoggedErrors(_logger, resultUsuario.Errors);
                return SendErrorsToView(resultUsuario.Errors);
            }
            result.Usuario = resultUsuario.Content;

            // Get roles
            ResultInfo<List<Rol>> resultRoles = _rolBusiness.GetRoles();
            if (resultRoles.HasErrors)
            {
                LoggedErrors(_logger, resultRoles.Errors);
                return SendErrorsToView(resultRoles.Errors);
            }
            result.Roles = resultRoles.Content;

            return PartialView("~/Views/BackOffice/Partials/_EditUsuarioForm.cshtml", result);
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
