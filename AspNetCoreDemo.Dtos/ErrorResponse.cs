using System;
using System.Collections.Generic;
using System.Text;

namespace AspNetCoreDemo.Dtos
{
    public class ErrorResponse : ResponseBase
    {
        public string ErrorCode { get; set; }

        public string ErrorMessage { get; set; }

        public object ErrorData { get; set; }
    }
}
