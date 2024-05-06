using DSM.ERPerfect.Helpers;
using DSM.ERPerfect.Models.Errors;
using Microsoft.AspNetCore.Mvc;

namespace DSM.ERPerfect.WebApp.Controllers
{
    public class BaseController : Controller
    {
        public enum Cookies
        {
            USERLOGIN
        }

        public void LoggedErrors<T>(ILogger<T> logger, List<ResultError> errors) where T : class
        {
            foreach (ResultError error in errors)
            {
                if (!error.IsException)
                    logger.LogWarning($"Warning: '{JSONHelper.JsonSerializer(errors)}'");
                else
                    logger.LogError($"Error: '{JSONHelper.JsonSerializer(errors)}'");
            }
        }

        public IActionResult SendErrorsToView(List<ResultError> errors)
        {
            return StatusCode(403, errors);
        }

        public void DeleteCookies()
        {
            foreach (string cookie in Request.Cookies.Keys)
                Response.Cookies.Delete(cookie);
        }

    }
}
