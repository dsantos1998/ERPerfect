using DSM.ERPerfect.BLL;
using DSM.ERPerfect.BLL.Interfaces;
using DSM.ERPerfect.Helpers;
using DSM.ERPerfect.Models.Cookies;
using DSM.ERPerfect.Models.Entities;
using DSM.ERPerfect.Models.Errors;
using DSM.ERPerfect.Models.Statistics;
using DSM.ERPerfect.Models.VM.Home;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Diagnostics;

namespace DSM.ERPerfect.WebApp.Controllers
{
    public class HomeController : BaseController
    {
        #region Properties

        private readonly ILogger<HomeController> _logger;
        private IUsuarioBusiness _usuarioBusiness { get; init; }
        private IFacturaBusiness _facturaBusiness { get; init; }
        private IClienteBusiness _clienteBusiness { get; init; }
        private IServicioBusiness _servicioBusiness { get; init; }
        private IFormaPagoBusiness _formaPagoBusiness { get; init; }
        private IFacturaServicioBusiness _facturaServicioBusiness { get; init; }
        private ITarifaBusiness _tarifaBusiness { get; init; }
        private ITarifaServicioBusiness _tarifaServicioBusiness { get; init; }

        #endregion

        #region Constructor

        public HomeController(ILogger<HomeController> logger
            , IUsuarioBusiness usuarioBusiness
            , IFacturaBusiness facturaBusiness
            , IClienteBusiness clienteBusiness
            , IServicioBusiness servicioBusiness
            , IFormaPagoBusiness formaPagoBusiness
            , IFacturaServicioBusiness facturaServicioBusiness
            , ITarifaBusiness tarifaBusiness
            , ITarifaServicioBusiness tarifaServicioBusiness)
        {
            _logger = logger;
            _usuarioBusiness = usuarioBusiness;
            _facturaBusiness = facturaBusiness;
            _clienteBusiness = clienteBusiness;
            _servicioBusiness = servicioBusiness;
            _formaPagoBusiness = formaPagoBusiness;
            _facturaServicioBusiness = facturaServicioBusiness;
            _tarifaBusiness = tarifaBusiness;
            _tarifaServicioBusiness = tarifaServicioBusiness;
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
            result.Clientes = resultClientes.Content.Where(x => x.FechaBaja >= DateTime.Now).ToList();

            return View(result);
        }
        
        public IActionResult Bills()
        {
            FacturaVM result = new FacturaVM();

            // Check cookie user
            CookieUsuario? cookieUsuario = CheckLoginCookie();
            if (cookieUsuario == null)
            {
                List<ResultError> errors = new List<ResultError>() { new ResultError("Usuario no logueado", false, "HomeController.EnabledFactura()") };
                return SendErrorsToView(errors);
            }
            result.CookieUsuario = cookieUsuario;

            // Get facturas
            ResultInfo<List<Factura>> resultFacturas = _facturaBusiness.GetFacturasPending();
            if (resultFacturas.HasErrors)
            {
                LoggedErrors(_logger, resultFacturas.Errors);
                return SendErrorsToView(resultFacturas.Errors);
            }
            result.Facturas = resultFacturas.Content;

            if(resultFacturas.Content != null)
            {
                if(resultFacturas.Content.Count > 0)
                {
                    List<FacturaServicio> facturaServicios = new List<FacturaServicio>();
                    foreach(var item in resultFacturas.Content)
                    {
                        var resultFacturaServicio = _facturaServicioBusiness.GetFacturaServicioByIdFactura(item.IdFactura);
                        if (resultFacturaServicio.HasErrors)
                        {
                            LoggedErrors(_logger, resultFacturaServicio.Errors);
                            return SendErrorsToView(resultFacturaServicio.Errors);
                        }
                        facturaServicios.AddRange(resultFacturaServicio.Content);
                    }
                    result.FacturasServicios = facturaServicios;
                }
            }

            // Get tarifas
            var resultTarifa = _tarifaBusiness.GetTarifas();
            if (resultTarifa.HasErrors)
            {
                LoggedErrors(_logger, resultTarifa.Errors);
                return SendErrorsToView(resultTarifa.Errors);
            }
            result.Tarifas = resultTarifa.Content;

            // Get tarifas servicios
            var resultTarifaServicio = _tarifaServicioBusiness.GetTarifaServicio();
            if (resultTarifaServicio.HasErrors)
            {
                LoggedErrors(_logger, resultTarifaServicio.Errors);
                return SendErrorsToView(resultTarifaServicio.Errors);
            }
            result.TarifasServicios = resultTarifaServicio.Content;

            // Get servicios
            var resultServicios = _servicioBusiness.GetServicios();
            if (resultServicios.HasErrors)
            {
                LoggedErrors(_logger, resultServicios.Errors);
                return SendErrorsToView(resultServicios.Errors);
            }
            result.Servicios = resultServicios.Content;

            // Get Clientes
            var resultClientes = _clienteBusiness.GetClientes();
            if (resultClientes.HasErrors)
            {
                LoggedErrors(_logger, resultClientes.Errors);
                return SendErrorsToView(resultClientes.Errors);
            }
            result.Clientes = resultClientes.Content;

            // Get FormaPago
            ResultInfo<List<FormaPago>> resultFormasPago = _formaPagoBusiness.GetFormasPago();
            if (resultFormasPago.HasErrors)
            {
                LoggedErrors(_logger, resultFormasPago.Errors);
                return SendErrorsToView(resultFormasPago.Errors);
            }
            result.FormasPago = resultFormasPago.Content;

            return View(result);
        }

        #endregion

        #region AJAX Calls

        #region Clients

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

        #region Bills

        public IActionResult SaveFacturaPendiente(int idCliente, int idServicio, int idFormaPago)
        {
            // Check cookie user
            CookieUsuario? cookieUsuario = CheckLoginCookie();
            if (cookieUsuario == null)
            {
                List<ResultError> errors = new List<ResultError>() { new ResultError("Usuario no logueado", false, "HomeController.SaveFacturaPendiente()") };
                return SendErrorsToView(errors);
            }

            // Get usuario
            ResultInfo<Usuario> resultGetUsuario = _usuarioBusiness.GetUsuarioByRowGUID(cookieUsuario.RowGuid);
            if (resultGetUsuario.HasErrors)
            {
                LoggedErrors(_logger, resultGetUsuario.Errors);
                return SendErrorsToView(resultGetUsuario.Errors);
            }

            // Save factura pendiente
            Factura factura = new Factura()
            {
                IdFactura = 0,
                IdCliente = idCliente,
                IdFormaPago = idFormaPago,
                NFactura = null,
                FechaCreacion = DateTime.Now,
                FechaCobro = null,
                NAbono = null,
                FechaAbono = null
            };
            ResultInfo<int> resultNewFactura = _facturaBusiness.NewFactura(factura);
            if (resultNewFactura.HasErrors)
            {
                LoggedErrors(_logger, resultNewFactura.Errors);
                return SendErrorsToView(resultNewFactura.Errors);
            }

            FacturaServicio facturaServicio = new FacturaServicio()
            {
                IdFacturaServicio = 0,
                IdFactura = resultNewFactura.Content,
                IdServicio = idServicio,
                IdUsuario = resultGetUsuario.Content.IdUsuario,
                FechaCreacion = DateTime.Now,
                FechaEliminacion = null,
                IdUsuarioEliminacion = null
            };
            ResultInfo<int> resultNewFacturaServicio = _facturaServicioBusiness.NewFacturaServicio(facturaServicio);
            if (resultNewFacturaServicio.HasErrors)
            {
                LoggedErrors(_logger, resultNewFacturaServicio.Errors);
                return SendErrorsToView(resultNewFacturaServicio.Errors);
            }

            List<ResultError> errores = new List<ResultError>() { new ResultError("Factura creada correctamente", null, "Exito") };
            return StatusCode(StatusCodes.Status200OK, errores);
        }

        public IActionResult CobrarFacturaPendiente(int idFactura)
        {
            // Check cookie user
            CookieUsuario? cookieUsuario = CheckLoginCookie();
            if (cookieUsuario == null)
            {
                List<ResultError> errors = new List<ResultError>() { new ResultError("Usuario no logueado", false, "HomeController.CobrarFacturaPendiente()") };
                return SendErrorsToView(errors);
            }

            Factura factura = new Factura()
            {
                IdFactura = idFactura,
                IdCliente = 0,
                IdFormaPago = 0,
                NFactura = null,
                FechaCreacion = DateTime.Now,
                NAbono = null,
                FechaCobro = null,
                FechaAbono = null
            };
            var resultCobrarFactura = _facturaBusiness.UpdateFacturaPaid(factura);
            if (resultCobrarFactura.HasErrors)
            {
                LoggedErrors(_logger, resultCobrarFactura.Errors);
                return SendErrorsToView(resultCobrarFactura.Errors);
            }

            List<ResultError> errores = new List<ResultError>() { new ResultError("Factura cobrada correctamente", null, "Exito") };
            return StatusCode(StatusCodes.Status200OK, errores);
        }

        public IActionResult EditFacturaPendiente(int idFactura, int idCliente, int idServicio, int idFormaPago)
        {
            // Check cookie user
            CookieUsuario? cookieUsuario = CheckLoginCookie();
            if (cookieUsuario == null)
            {
                List<ResultError> errors = new List<ResultError>() { new ResultError("Usuario no logueado", false, "HomeController.EditFacturaPendiente()") };
                return SendErrorsToView(errors);
            }

            var resultUpdateFactura = _facturaBusiness.UpdateFactura(idFactura, idCliente, idServicio, idFormaPago);
            if (resultUpdateFactura.HasErrors)
            {
                LoggedErrors(_logger, resultUpdateFactura.Errors);
                return SendErrorsToView(resultUpdateFactura.Errors);
            }

            List<ResultError> errores = new List<ResultError>() { new ResultError("Factura editada correctamente", null, "Exito") };
            return StatusCode(StatusCodes.Status200OK, errores);
        }

        public IActionResult AbonarFactura(int idFactura)
        {
            // Check cookie user
            CookieUsuario? cookieUsuario = CheckLoginCookie();
            if (cookieUsuario == null)
            {
                List<ResultError> errors = new List<ResultError>() { new ResultError("Usuario no logueado", false, "HomeController.AbonarFactura()") };
                return SendErrorsToView(errors);
            }

            Factura factura = new Factura()
            {
                IdFactura = idFactura,
                IdCliente = 0,
                IdFormaPago = 0,
                NFactura = null,
                FechaCreacion = DateTime.Now,
                NAbono = null,
                FechaCobro = null,
                FechaAbono = null
            };
            var resultCobrarFactura = _facturaBusiness.UpdateFacturaRefound(factura);
            if (resultCobrarFactura.HasErrors)
            {
                LoggedErrors(_logger, resultCobrarFactura.Errors);
                return SendErrorsToView(resultCobrarFactura.Errors);
            }

            List<ResultError> errores = new List<ResultError>() { new ResultError("Factura abonada correctamente", null, "Exito") };
            return StatusCode(StatusCodes.Status200OK, errores);
        }

        #endregion

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
                    result.Clientes = resultClientes.Content.Where(x => x.FechaBaja >= DateTime.Now).ToList();
                }
                else
                {
                    result.Clientes = resultClientes.Content.Where(x => x.FechaBaja < DateTime.Now).ToList();
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

        #region Facturas

        public IActionResult LoadFacturasPendientes()
        {
            FacturaVM result = new FacturaVM();

            // Check cookie user
            CookieUsuario? cookieUsuario = CheckLoginCookie();
            if (cookieUsuario == null)
            {
                List<ResultError> errors = new List<ResultError>() { new ResultError("Usuario no logueado", false, "HomeController.EnabledFactura()") };
                return SendErrorsToView(errors);
            }

            // Get facturas
            ResultInfo<List<Factura>> resultFacturas = _facturaBusiness.GetFacturasPending();
            if (resultFacturas.HasErrors)
            {
                LoggedErrors(_logger, resultFacturas.Errors);
                return SendErrorsToView(resultFacturas.Errors);
            }
            result.Facturas = resultFacturas.Content;

            if (resultFacturas.Content.Count > 0)
            {
                List<FacturaServicio> facturaServicios = new List<FacturaServicio>();
                foreach (var item in resultFacturas.Content)
                {
                    var resultFacturaServicio = _facturaServicioBusiness.GetFacturaServicioByIdFactura(item.IdFactura);
                    if (resultFacturaServicio.HasErrors)
                    {
                        LoggedErrors(_logger, resultFacturaServicio.Errors);
                        return SendErrorsToView(resultFacturaServicio.Errors);
                    }
                    facturaServicios.AddRange(resultFacturaServicio.Content);
                }
                result.FacturasServicios = facturaServicios;
            }

            // Get tarifas
            var resultTarifa = _tarifaBusiness.GetTarifas();
            if (resultTarifa.HasErrors)
            {
                LoggedErrors(_logger, resultTarifa.Errors);
                return SendErrorsToView(resultTarifa.Errors);
            }
            result.Tarifas = resultTarifa.Content;

            // Get tarifas servicios
            var resultTarifaServicio = _tarifaServicioBusiness.GetTarifaServicio();
            if (resultTarifaServicio.HasErrors)
            {
                LoggedErrors(_logger, resultTarifaServicio.Errors);
                return SendErrorsToView(resultTarifaServicio.Errors);
            }
            result.TarifasServicios = resultTarifaServicio.Content;

            // Get servicios
            var resultServicios = _servicioBusiness.GetServicios();
            if (resultServicios.HasErrors)
            {
                LoggedErrors(_logger, resultServicios.Errors);
                return SendErrorsToView(resultServicios.Errors);
            }
            result.Servicios = resultServicios.Content;

            // Get Clientes
            var resultClientes = _clienteBusiness.GetClientes();
            if (resultClientes.HasErrors)
            {
                LoggedErrors(_logger, resultClientes.Errors);
                return SendErrorsToView(resultClientes.Errors);
            }
            result.Clientes = resultClientes.Content;

            // Get FormaPago
            ResultInfo<List<FormaPago>> resultFormasPago = _formaPagoBusiness.GetFormasPago();
            if (resultFormasPago.HasErrors)
            {
                LoggedErrors(_logger, resultFormasPago.Errors);
                return SendErrorsToView(resultFormasPago.Errors);
            }
            result.FormasPago = resultFormasPago.Content;

            return PartialView("~/Views/Home/Partials/_TablaFacturasPendientes.cshtml", result);
        }

        public IActionResult ShowEditarFacturaPendiente(int id)
        {
            FacturaVM result = new FacturaVM();

            // Check cookie user
            CookieUsuario? cookieUsuario = CheckLoginCookie();
            if (cookieUsuario == null)
            {
                List<ResultError> errors = new List<ResultError>() { new ResultError("Usuario no logueado", false, "HomeController.ShowEditarFacturaPendiente()") };
                return SendErrorsToView(errors);
            }

            // Get factura by id
            ResultInfo<Factura> resultFactura = _facturaBusiness.GetFacturaById(id);
            if (resultFactura.HasErrors)
            {
                LoggedErrors(_logger, resultFactura.Errors);
                return SendErrorsToView(resultFactura.Errors);
            }
            result.Factura = resultFactura.Content;

            // Get factura servicio
            var resultFacturaServicio = _facturaServicioBusiness.GetFacturaServicioByIdFactura(resultFactura.Content.IdFactura);
            if (resultFacturaServicio.HasErrors)
            {
                LoggedErrors(_logger, resultFacturaServicio.Errors);
                return SendErrorsToView(resultFacturaServicio.Errors);
            }
            result.FacturaServicio = resultFacturaServicio.Content[0];

            // Get tarifas
            var resultTarifa = _tarifaBusiness.GetTarifas();
            if (resultTarifa.HasErrors)
            {
                LoggedErrors(_logger, resultTarifa.Errors);
                return SendErrorsToView(resultTarifa.Errors);
            }
            result.Tarifas = resultTarifa.Content;

            // Get tarifas servicios
            var resultTarifaServicio = _tarifaServicioBusiness.GetTarifaServicio();
            if (resultTarifaServicio.HasErrors)
            {
                LoggedErrors(_logger, resultTarifaServicio.Errors);
                return SendErrorsToView(resultTarifaServicio.Errors);
            }
            result.TarifasServicios = resultTarifaServicio.Content;

            // Get servicios
            var resultServicios = _servicioBusiness.GetServicios();
            if (resultServicios.HasErrors)
            {
                LoggedErrors(_logger, resultServicios.Errors);
                return SendErrorsToView(resultServicios.Errors);
            }
            result.Servicios = resultServicios.Content;

            // Get Clientes
            var resultClientes = _clienteBusiness.GetClientes();
            if (resultClientes.HasErrors)
            {
                LoggedErrors(_logger, resultClientes.Errors);
                return SendErrorsToView(resultClientes.Errors);
            }
            result.Clientes = resultClientes.Content;

            // Get FormaPago
            ResultInfo<List<FormaPago>> resultFormasPago = _formaPagoBusiness.GetFormasPago();
            if (resultFormasPago.HasErrors)
            {
                LoggedErrors(_logger, resultFormasPago.Errors);
                return SendErrorsToView(resultFormasPago.Errors);
            }
            result.FormasPago = resultFormasPago.Content;

            return PartialView("~/Views/Home/Partials/_EditarFacturaPendienteForm.cshtml", result);
        }

        public IActionResult LoadFacturasCobradas()
        {
            FacturaVM result = new FacturaVM();

            // Check cookie user
            CookieUsuario? cookieUsuario = CheckLoginCookie();
            if (cookieUsuario == null)
            {
                List<ResultError> errors = new List<ResultError>() { new ResultError("Usuario no logueado", false, "HomeController.LoadFacturasCobradas()") };
                return SendErrorsToView(errors);
            }
            result.CookieUsuario = cookieUsuario;

            // Get facturas
            ResultInfo<List<Factura>> resultFacturas = _facturaBusiness.GetFacturas();
            if (resultFacturas.HasErrors)
            {
                LoggedErrors(_logger, resultFacturas.Errors);
                return SendErrorsToView(resultFacturas.Errors);
            }

            if (resultFacturas.Content != null)
            {
                result.Facturas = resultFacturas.Content.Where(x => x.NFactura != null && x.NAbono == null).OrderByDescending(x => x.FechaCobro).ToList();
            }
            else
            {
                result.Facturas = new List<Factura>();
            }

            if (result.Facturas.Count > 0)
            {
                List<FacturaServicio> facturaServicios = new List<FacturaServicio>();
                foreach (var item in result.Facturas)
                {
                    var resultFacturaServicio = _facturaServicioBusiness.GetFacturaServicioByIdFactura(item.IdFactura);
                    if (resultFacturaServicio.HasErrors)
                    {
                        LoggedErrors(_logger, resultFacturaServicio.Errors);
                        return SendErrorsToView(resultFacturaServicio.Errors);
                    }
                    facturaServicios.AddRange(resultFacturaServicio.Content);
                }
                result.FacturasServicios = facturaServicios;
            }

            // Get tarifas
            var resultTarifa = _tarifaBusiness.GetTarifas();
            if (resultTarifa.HasErrors)
            {
                LoggedErrors(_logger, resultTarifa.Errors);
                return SendErrorsToView(resultTarifa.Errors);
            }
            result.Tarifas = resultTarifa.Content;

            // Get tarifas servicios
            var resultTarifaServicio = _tarifaServicioBusiness.GetTarifaServicio();
            if (resultTarifaServicio.HasErrors)
            {
                LoggedErrors(_logger, resultTarifaServicio.Errors);
                return SendErrorsToView(resultTarifaServicio.Errors);
            }
            result.TarifasServicios = resultTarifaServicio.Content;

            // Get servicios
            var resultServicios = _servicioBusiness.GetServicios();
            if (resultServicios.HasErrors)
            {
                LoggedErrors(_logger, resultServicios.Errors);
                return SendErrorsToView(resultServicios.Errors);
            }
            result.Servicios = resultServicios.Content;

            // Get Clientes
            var resultClientes = _clienteBusiness.GetClientes();
            if (resultClientes.HasErrors)
            {
                LoggedErrors(_logger, resultClientes.Errors);
                return SendErrorsToView(resultClientes.Errors);
            }
            result.Clientes = resultClientes.Content;

            // Get FormaPago
            ResultInfo<List<FormaPago>> resultFormasPago = _formaPagoBusiness.GetFormasPago();
            if (resultFormasPago.HasErrors)
            {
                LoggedErrors(_logger, resultFormasPago.Errors);
                return SendErrorsToView(resultFormasPago.Errors);
            }
            result.FormasPago = resultFormasPago.Content;

            return PartialView("~/Views/Home/Partials/_TablaFacturasCobradas.cshtml", result);
        }

        public IActionResult LoadFacturasAbonadas()
        {
            FacturaVM result = new FacturaVM();

            // Check cookie user
            CookieUsuario? cookieUsuario = CheckLoginCookie();
            if (cookieUsuario == null)
            {
                List<ResultError> errors = new List<ResultError>() { new ResultError("Usuario no logueado", false, "HomeController.LoadFacturasAbonadas()") };
                return SendErrorsToView(errors);
            }
            result.CookieUsuario = cookieUsuario;

            // Get facturas
            ResultInfo<List<Factura>> resultFacturas = _facturaBusiness.GetFacturas();
            if (resultFacturas.HasErrors)
            {
                LoggedErrors(_logger, resultFacturas.Errors);
                return SendErrorsToView(resultFacturas.Errors);
            }

            if (resultFacturas.Content != null)
            {
                result.Facturas = resultFacturas.Content.Where(x => x.NAbono != null).OrderByDescending(x => x.NAbono).ToList();
            }
            else
            {
                result.Facturas = new List<Factura>();
            }

            if (result.Facturas.Count > 0)
            {
                List<FacturaServicio> facturaServicios = new List<FacturaServicio>();
                foreach (var item in result.Facturas)
                {
                    var resultFacturaServicio = _facturaServicioBusiness.GetFacturaServicioByIdFactura(item.IdFactura);
                    if (resultFacturaServicio.HasErrors)
                    {
                        LoggedErrors(_logger, resultFacturaServicio.Errors);
                        return SendErrorsToView(resultFacturaServicio.Errors);
                    }
                    facturaServicios.AddRange(resultFacturaServicio.Content);
                }
                result.FacturasServicios = facturaServicios;
            }

            // Get tarifas
            var resultTarifa = _tarifaBusiness.GetTarifas();
            if (resultTarifa.HasErrors)
            {
                LoggedErrors(_logger, resultTarifa.Errors);
                return SendErrorsToView(resultTarifa.Errors);
            }
            result.Tarifas = resultTarifa.Content;

            // Get tarifas servicios
            var resultTarifaServicio = _tarifaServicioBusiness.GetTarifaServicio();
            if (resultTarifaServicio.HasErrors)
            {
                LoggedErrors(_logger, resultTarifaServicio.Errors);
                return SendErrorsToView(resultTarifaServicio.Errors);
            }
            result.TarifasServicios = resultTarifaServicio.Content;

            // Get servicios
            var resultServicios = _servicioBusiness.GetServicios();
            if (resultServicios.HasErrors)
            {
                LoggedErrors(_logger, resultServicios.Errors);
                return SendErrorsToView(resultServicios.Errors);
            }
            result.Servicios = resultServicios.Content;

            // Get Clientes
            var resultClientes = _clienteBusiness.GetClientes();
            if (resultClientes.HasErrors)
            {
                LoggedErrors(_logger, resultClientes.Errors);
                return SendErrorsToView(resultClientes.Errors);
            }
            result.Clientes = resultClientes.Content;

            // Get FormaPago
            ResultInfo<List<FormaPago>> resultFormasPago = _formaPagoBusiness.GetFormasPago();
            if (resultFormasPago.HasErrors)
            {
                LoggedErrors(_logger, resultFormasPago.Errors);
                return SendErrorsToView(resultFormasPago.Errors);
            }
            result.FormasPago = resultFormasPago.Content;

            return PartialView("~/Views/Home/Partials/_TablaFacturasCobradas.cshtml", result);
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
