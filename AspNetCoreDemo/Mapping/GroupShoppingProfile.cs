using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ND.ManagementSvcs.Dtos.GroupShopping.Responses;
using ND.ManagementSvcs.Framework.Repositories.Entities.GroupShopping;
using AutoMapper;

namespace ND.ManagementSvcs.Mapping
{
    public class GroupShoppingProfile : Profile
    {
        public GroupShoppingProfile()
        {
            CreateMap<GroupShoppingFound, GetFoundResponse>();
        }
    }
}
