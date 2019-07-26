using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreDemo.Dtos.GroupShopping.Responses;
using AspNetCoreDemo.Framework.Repositories.Entities.GroupShopping;
using AutoMapper;

namespace AspNetCoreDemo.Mapping
{
    public class GroupShoppingProfile : Profile
    {
        public GroupShoppingProfile()
        {
            CreateMap<GroupShoppingFound, GetFoundResponse>();
        }
    }
}
