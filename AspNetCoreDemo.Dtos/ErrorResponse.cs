using System;
using System.Collections.Generic;
using System.Text;

namespace ND.ManagementSvcs.Dtos
{
    public class ErrorResponse : ResponseBase
    {
        public string ErrorCode { get; set; }

        public string ErrorMessage { get; set; }

        public object ErrorData { get; set; }
    }
}
