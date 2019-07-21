using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AspNetCoreDemo.Dtos;
using AspNetCoreDemo.Dtos.Values.ChildDtos;
using AspNetCoreDemo.Dtos.Values.Requests;
using AspNetCoreDemo.Dtos.Values.Responses;
using AspNetCoreDemo.Framework.Errors;
using Microsoft.AspNetCore.Mvc;
using NLog;

namespace AspNetCoreDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly ILogger _log;
        public ValuesController()
        {
            _log = LogManager.GetCurrentClassLogger();
        }
        // GET api/values
        /// <summary>
        /// get
        /// </summary>
        /// <param name="id"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(NotFoundResult), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(GetValuesResponse), (int)HttpStatusCode.OK)]
        [HttpGet("~/test/{id}")]
        public Task<ObjectResult> GetAsync([FromRoute(Name = "id")]int id,[FromQuery]GetValuesRequest value)
        {
            return DoAsync(async () =>
            {
                //_log.Info("Info");
                //_log.Debug("Debug");
                //_log.Error("Error");
                //_log.Warn("Warn");
                throw new ServiceException(AllServiceErrors.TestError.WithMessageParameters("Billy"));
                throw new Exception("test Exception");
                return new OkObjectResult(new GetValuesResponse
                {
                    Details = new DetailDto {DetailInt = 1, DetailStr = "dsf"},
                    Result = "ds"
                });
            });

            //return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        //[HttpGet("{id}")]
        //public ActionResult<string> Get(int id)
        //{
        //    return "value";
        //}

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
