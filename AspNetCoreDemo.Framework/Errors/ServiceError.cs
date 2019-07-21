using System.Net;

namespace AspNetCoreDemo.Framework.Errors
{
    public class ServiceError
    {
        public HttpStatusCode StatusCode { get; set; }

        public string Code { get; set; }

        public string Description { get; set; }

        public string Message { get; set; }

        public object Data { get; set; }
    }
}
