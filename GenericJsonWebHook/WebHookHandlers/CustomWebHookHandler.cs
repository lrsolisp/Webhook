using Microsoft.AspNet.WebHooks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace GenericJsonWebHook.WebHookHandlers
{
    public class CustomWebHookHandler : WebHookHandler
    {
        public override Task ExecuteAsync(string receiver, WebHookHandlerContext context)
        {
            JObject data = context.GetDataOrDefault<JObject>();
            System.IO.File.WriteAllText(@"C:\WebhookLog.txt", data.ToString());

            return Task.FromResult(true);
        }
    }
}