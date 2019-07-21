using System;
using System.Collections.Generic;
using System.Text;
using AspNetCoreDemo.Dtos.Values.ChildDtos;

namespace AspNetCoreDemo.Dtos.Values.Responses
{
    public class GetValuesResponse : ResponseBase
    {
        public string Result { get; set; }
        public List<string> LstResult { get; set; }
        public DetailDto Details { get; set; }
    }
}
