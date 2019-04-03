using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc.ViewFeatures;


namespace Frontend.Helpers.Alert
{
    public class AlertResult : IActionResult
    {
        public IActionResult Result { get; }
        public string Type { get; }
        public string Title { get; }
        public string Body { get; }
        public ITempDataDictionaryFactory tempDataDictionaryFactory { get; set; }

        public AlertResult(IActionResult result, string type, string title, string body)
        {
            Result = result;
            Type = type;
            Title = title;
            Body = body;
        }

        public AlertResult()
        {
        }

        public async Task ExecuteResultAsync(ActionContext context)
        {
            if (Result is StatusCodeResult || Result is OkObjectResult)
            {
                AddAlertMessageToApiResult(context);
            }
            else
            {
                AddAlertMessageToMvcResult(context);
            }

            await Result.ExecuteResultAsync(context);
        }

        private void AddAlertMessageToApiResult(ActionContext context)
        {
            context.HttpContext.Response.Headers.Add("x-alert-type", Type);
            context.HttpContext.Response.Headers.Add("x-alert-title", Title);
            context.HttpContext.Response.Headers.Add("x-alert-body", Body);
        }

        private void AddAlertMessageToMvcResult(ActionContext context)
        {
            var factory = context.HttpContext.RequestServices.GetService<ITempDataDictionaryFactory>();

            var tempData = factory.GetTempData(context.HttpContext);

            tempData["_alert.type"] = Type;
            tempData["_alert.title"] = Title;
            tempData["_alert.body"] = Body;
        }
    }
}
