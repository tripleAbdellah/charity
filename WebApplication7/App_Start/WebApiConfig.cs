using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Net.Http.Headers;

namespace WebApplication7
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

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


            config.Formatters.JsonFormatter.
            SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));
        }
    }


}
