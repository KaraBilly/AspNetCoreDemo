﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using ND.ManagementSvcs.Dtos;
using ND.ManagementSvcs.Dtos.Values.ChildDtos;
using ND.ManagementSvcs.Dtos.Values.Requests;
using ND.ManagementSvcs.Dtos.Values.Responses;
using ND.ManagementSvcs.Framework.Errors;
using ND.ManagementSvcs.Framework.Repositories.Interfaces;
using ND.ManagementSvcs.Objects;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using NLog;

namespace ND.ManagementSvcs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly ILogger _log;
        private readonly IValuesRepository _valuesRepository;
        private readonly IDistributedCache _distributedCache;
        private readonly IMapper _mapper;
        public ValuesController(IValuesRepository valuesRepository, IDistributedCache distributedCache,IMapper mapper)
        {
            _log = LogManager.GetCurrentClassLogger();
            _valuesRepository = valuesRepository;
            _distributedCache = distributedCache;
            _mapper = mapper;
        }

        /// <summary>
        ///   get test 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(NotFoundResult), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.ExpectationFailed)]
        [ProducesResponseType(typeof(GetValuesResponse), (int)HttpStatusCode.OK)]
        [HttpGet("{id}")]
        public Task<ObjectResult> GetAsync([FromRoute(Name = "id")]int id,[FromQuery]GetValuesRequest value)
        {
            return DoAsync(async () =>
            {
                _log.Info("info");
                _log.Debug("Debug");
                _log.Error("Error");
                _log.Warn("Warn");

                throw new ServiceException(AllServiceErrors.TestError.WithMessageParameters("Billy"));
                //throw new Exception("test Exception");

                await _distributedCache.SetAsync("hello", Encoding.UTF8.GetBytes("world22222"),
                    new DistributedCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromMinutes(5)));
                var _ = await _distributedCache.GetAsync("hello");
                return new GetValuesResponse
                {
                    Details = _mapper.Map<DetailDto>(_valuesRepository.GetDetail()),
                        //new DetailDto {DetailInt = 1, DetailStr = "dsf"},
                    Result = _valuesRepository.GetValues(id)
                };
            });

            //return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value,[FromQuery]int request)
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
