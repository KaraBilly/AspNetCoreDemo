using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AspNetCoreDemo.Dtos.Values.Requests
{
    public class GetValuesRequest //: RequestBase
    {
        /// <summary>
        ///  value  Id
        /// </summary>
        [FromQuery(Name = "val_id"),Required]
        public long ValueId { get; set; }
        /// <summary>
        ///   Value
        /// </summary>
        [FromQuery(Name = "value")]
        public long Value { get; set; }
    }
}
