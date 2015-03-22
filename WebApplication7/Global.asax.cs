using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Routing;

namespace WebApplication7
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            //WebApiConfig.Register(GlobalConfiguration.Configuration); 
            GlobalConfiguration.Configure(WebApiConfig.Register);
            //  Log4Net Configuration
            log4net.Config.XmlConfigurator.Configure();
        }
    }
}
