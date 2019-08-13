using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;

namespace MambuWebHook.Filters
{
    public class BasicAuthenticationAttribute : ActionFilterAttribute
    {

        public string ValidUsername { get; protected set; }
        public string ValidPassword { get; protected set; }
        public BasicAuthenticationAttribute()
        {
            ValidUsername = WebConfigurationManager.AppSettings["WebHookUsername"];
            ValidPassword = WebConfigurationManager.AppSettings["WebHookPassword"];
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var req = filterContext.HttpContext.Request;
            var auth = req.Headers["Authorization"];
            if (!String.IsNullOrEmpty(auth))
            {
                var cred = System.Text.ASCIIEncoding.ASCII.GetString(Convert.FromBase64String(auth.Substring(6))).Split(':');
                var user = new { Name = cred[0], Pass = cred[1] };
                if (user.Name == ValidUsername && user.Pass == ValidPassword) return;
            }
            filterContext.HttpContext.Response.AddHeader("WWW-Authenticate", "Basic Scheme 'Data' location = 'http://localhost:");
            /// thanks to eismanpat for this line: http://www.ryadel.com/en/http-basic-authentication-asp-net-mvc-using-custom-actionfilter/#comment-2507605761
            filterContext.Result = new HttpUnauthorizedResult();
        }
    }
}