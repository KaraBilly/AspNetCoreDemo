using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using AspNetCoreDemo.Dtos;
using AspNetCoreDemo.Dtos.GroupShopping.Responses;
using AspNetCoreDemo.Dtos.Values.ChildDtos;
using AspNetCoreDemo.Dtos.Values.Requests;
using AspNetCoreDemo.Dtos.Values.Responses;
using AspNetCoreDemo.Framework.Errors;
using AspNetCoreDemo.Framework.Repositories.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;

namespace AspNetCoreDemo.Controllers
{
    //[Route("group-shopping")]
    [ApiController]
    public class GroupShoppingController : ControllerBase
    {
        private readonly IGroupShoppingRepository _groupShoppingRepository;
        private readonly IMapper _mapper;

        public GroupShoppingController(IGroupShoppingRepository groupShoppingRepository,
            IMapper mapper)
        {
            _groupShoppingRepository = groupShoppingRepository;
            _mapper = mapper;
        }

        /// <summary>
        ///   get test 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(NotFoundResult), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.ExpectationFailed)]
        [ProducesResponseType(typeof(GetFoundResponse), (int)HttpStatusCode.OK)]
        [HttpGet("group-shopping/{id}")]
        public Task<ObjectResult> GetAsync([FromRoute(Name = "id")]int id)
        {
            return DoAsync(async () =>
            {
                var found = await _groupShoppingRepository.GetDataById(1944);
                return _mapper.Map<GetFoundResponse>(found);
            });
        }
    }
}
