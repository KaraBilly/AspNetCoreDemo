using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using ND.ManagementSvcs.Dtos;
using ND.ManagementSvcs.Dtos.GroupShopping.Responses;
using ND.ManagementSvcs.Dtos.Values.ChildDtos;
using ND.ManagementSvcs.Dtos.Values.Requests;
using ND.ManagementSvcs.Dtos.Values.Responses;
using ND.ManagementSvcs.Framework.Errors;
using ND.ManagementSvcs.Framework.Repositories.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;

namespace ND.ManagementSvcs.Controllers
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
