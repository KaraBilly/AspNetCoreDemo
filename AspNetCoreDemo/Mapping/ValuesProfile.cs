using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ND.ManagementSvcs.Dtos.GroupShopping.Responses;
using ND.ManagementSvcs.Dtos.Values.ChildDtos;
using ND.ManagementSvcs.Framework.Repositories.Entities;
using ND.ManagementSvcs.Framework.Repositories.Entities.GroupShopping;
using AutoMapper;

namespace ND.ManagementSvcs.Mapping
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
