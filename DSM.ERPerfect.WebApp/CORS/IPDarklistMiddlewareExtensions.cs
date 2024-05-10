namespace DSM.ERPerfect.WebApp.CORS
{
    public static class IPDarklistMiddlewareExtensions
    {
        public static IApplicationBuilder UseIPDarklist(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<IPBannedlistMiddleware>();
        }
    }
}
