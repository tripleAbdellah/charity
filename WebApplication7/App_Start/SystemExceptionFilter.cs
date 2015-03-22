using System;
using System.Net;
using System.Net.Http;
using System.Web.Http.Filters;

using log4net;

namespace WebApplication7.Filters
{

    public class SystemExceptionFilter : ExceptionFilterAttribute
    {
        private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public override void OnException(HttpActionExecutedContext context)
        {
            if (context.Exception is SystemException)
            {
                Log.Fatal(String.Format("[{0} {1}] : {2}", context.Request.Method, context.Request.RequestUri, context.Exception.Message));
                context.Response = new HttpResponseMessage(HttpStatusCode.ServiceUnavailable);
            }
        }
    }
}