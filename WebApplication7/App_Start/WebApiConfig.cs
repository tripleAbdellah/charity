using System.Web.Http;

namespace WebApplication7
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API routes
            config.MapHttpAttributeRoutes();
            config.Filters.Add(new WebApplication7.Filters.SystemExceptionFilter());
        }
    }
}