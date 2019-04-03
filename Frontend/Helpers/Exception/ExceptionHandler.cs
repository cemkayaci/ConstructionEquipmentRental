
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace Frontend.Helpers.Exception
{
    public  class ExceptionHandler : ExceptionFilterAttribute
    {
        private readonly ILogger _logger;

        public ExceptionHandler(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<ExceptionHandler>();
        }
        public override void OnException(ExceptionContext context)
        {
            var exception = context.Exception;
            
            _logger.LogError(exception.Message);
            
        }        
    }
}
