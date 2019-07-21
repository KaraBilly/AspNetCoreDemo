using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace AspNetCoreDemo.Framework.Errors
{
    public static class AllServiceErrors
    {
        public static ServiceError InvalidDto => new ServiceError
        {
            StatusCode = HttpStatusCode.BadRequest,
            Code = nameof(InvalidDto),
            Message = "The request dto is invalid."
        };
        public static ServiceError TestError => new ServiceError
        {
            StatusCode = HttpStatusCode.BadRequest,
            Code = nameof(TestError),
            Message = "{0} for test"
        };
    }
}
