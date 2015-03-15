using System.Web.Http;

namespace WebApplication7
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API routes
            config.MapHttpAttributeRoutes();
            /*
            // Other Web API configuration not shown.
            config.Routes.MapHttpRoute(
                name: "WithActionApi",
                routeTemplate: "api/{controller}/{action}/{donorID}"
            );

            config.Routes.MapHttpRoute(
                name: "WithActionApiByName",
                routeTemplate: "api/{controller}/{action}/{nameID}"
            );

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
             */
        }
    }
}