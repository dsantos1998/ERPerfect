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

        #endregion

        #region Constructor

        public HomeController(ILogger<HomeController> logger
            , IUsuarioBusiness usuarioBusiness
            , IFacturaBusiness facturaBusiness)
        {
            _logger = logger;
            _usuarioBusiness = usuarioBusiness;
            _facturaBusiness = facturaBusiness;
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

        #endregion

        #region HTML Calls

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
