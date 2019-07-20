using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Remotion.Linq.Clauses.ResultOperators;

namespace AspNetCoreDemo.Filters
{
    public class GlobalExceptionFilterAttribute : ExceptionFilterAttribute
    {
        private const string TextContentType = "text/json;charset=utf-8;";
        private readonly ILogger _logger;
        public GlobalExceptionFilterAttribute(ILogger<GlobalExceptionFilterAttribute> logger)
        {
            _logger = logger;
        }

        public override void OnException(ExceptionContext context)
        {
            var request = context.HttpContext.Request;
            var exception = context.Exception;

            var customFields = new Dictionary<string, string>
            {
                {"Url", request.Path},
                {"Method", request.Method.ToUpper()}
            };
            if (request.QueryString.HasValue)
            {
                customFields.Add("QueryString", request.QueryString.Value);
            }
            if (request.Method == "POST" || request.Method == "PUT" || request.Method == "DELETE")
            {
                using (StreamReader reader = new StreamReader(request.Body))
                {
                    var body = reader.ReadToEnd();
                    customFields.Add("Body", body);
                }
            }

            var error = $"{JsonConvert.SerializeObject(customFields)} |";
            void ReadException(Exception ex)
            {
                while (true)
                {
                    error += $"{ex.Message} | {ex.StackTrace} | {ex.InnerException}";
                    if (ex.InnerException != null)
                    {
                        ex = ex.InnerException;
                        continue;
                    }

                    break;
                }
            }

            ReadException(context.Exception);
            _logger.LogError(error);

            var result = new ContentResult
            {
                StatusCode = (int)HttpStatusCode.InternalServerError,
                ContentType = TextContentType
            };

            var json = new { exception.Message, Detail = error};
            result.Content = JsonConvert.SerializeObject(json);
           
            context.Result = result;
            context.ExceptionHandled = true;
        }
    }
}
