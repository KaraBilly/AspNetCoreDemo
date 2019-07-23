using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreDemo.Request
{
    public class ExpectationFailedObjectResult : ObjectResult
    {
        public ExpectationFailedObjectResult(object error)
            : base(error)
        {
            this.StatusCode = new int?(400);
        }
    }
}
