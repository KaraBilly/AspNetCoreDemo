using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreDemo.Dtos.GroupShopping.Responses;
using AspNetCoreDemo.Dtos.Values.ChildDtos;
using AspNetCoreDemo.Framework.Repositories.Entities;
using AspNetCoreDemo.Framework.Repositories.Entities.GroupShopping;
using AutoMapper;

namespace AspNetCoreDemo.Mapping
{
    public class ValuesProfile : Profile
    {
        public ValuesProfile()
        {
            CreateMap<Detail, DetailDto>()
                .ForMember(x => x.DetailStr,
                    x => x.MapFrom(t => t.DetailStrTest))
                .ForMember(x => x.DetailInt,
                    x => x.MapFrom(t => t.DetailIntTest));
        }

    }
}
