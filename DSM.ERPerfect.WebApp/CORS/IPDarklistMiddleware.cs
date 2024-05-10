using DSM.ERPerfect.BLL.Interfaces;
using DSM.ERPerfect.Models.CORS;
using DSM.ERPerfect.Models.Entities;
using DSM.ERPerfect.Models.Errors;
using Microsoft.Extensions.Options;
using System.Net;

namespace DSM.ERPerfect.WebApp.CORS
{
    public class IPBannedlistMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<IPBannedlistMiddleware> _logger;
        private IBannedIPBusiness _bannedIPBusiness { get; init; }

        public IPBannedlistMiddleware(RequestDelegate next
            ,ILogger<IPBannedlistMiddleware> logger
            ,IBannedIPBusiness bannedIPBusiness)
        {
            _next = next;
            _logger = logger;
            _bannedIPBusiness = bannedIPBusiness;
        }

        public async Task Invoke(HttpContext context)
        {
            // Check white list ips
            var ipAddress = context.Connection.RemoteIpAddress;
            ResultInfo<List<BannedIP>> resultBannedIPs = _bannedIPBusiness.GetBannedIPs();
            if (resultBannedIPs.HasErrors)
            {
                _logger.LogWarning($"Error recuperando las ips baneadas.");
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                return;
            }

            if (resultBannedIPs.Content != null)
            {
                bool isIPDarklisted = resultBannedIPs.Content.Where(x => IPAddress.Parse(x.IPAddress).Equals(ipAddress)).Any();
                if (isIPDarklisted)
                {
                    _logger.LogWarning($"La petición para la IP: '{ipAddress}' está prohibida.", ipAddress);
                    context.Response.StatusCode = StatusCodes.Status403Forbidden;
                    return;
                }
            }

            await _next.Invoke(context);
        }
    }
}
