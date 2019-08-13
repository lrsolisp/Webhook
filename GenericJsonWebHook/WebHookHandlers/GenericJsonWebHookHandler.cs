





using log4net;
using Microsoft.AspNet.WebHooks;
using Newtonsoft.Json.Linq;
using System.Linq;
using System.Threading.Tasks;

namespace GenericJsonWebHook.WebHookHandlers
{
    public class GenericJsonWebHookHandler : WebHookHandler
    {
        public GenericJsonWebHookHandler()
        {
            this.Receiver = "genericjson";
        }
        private static ILog log = LogManager.GetLogger("MainIn");

        public override Task ExecuteAsync(string receiver, WebHookHandlerContext context)
        {
            JObject data = context.GetDataOrDefault<JObject>();

            string action = context.Actions.First();

            log.Debug(action);
            System.IO.File.WriteAllText(@"C:\WebhookLog.txt", action);

            return Task.FromResult(true);
        }
    }
}
