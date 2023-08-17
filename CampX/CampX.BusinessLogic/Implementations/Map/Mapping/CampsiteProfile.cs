using AutoMapper;
using CampX.BusinessLogic.Implementations.Account.Models;
using CampX.BusinessLogic.Implementations.Map.Models;
using CampX.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampX.BusinessLogic.Implementations.Map.Mapping
{
    public class CampsiteProfile : Profile
    {
        public CampsiteProfile()
        {

            CreateMap<AddCampsiteModel, Campsite>()
                .ForMember(t => t.Images, t => t.Ignore());
            CreateMap<CampsiteDetailsModel, Campsite>();
            CreateMap<EditCampsiteModel, Campsite>()
                .ForMember(t => t.Images, t => t.Ignore());
            CreateMap<CampsiteDetailsModel, EditCampsiteModel>()
                 .ForMember(e => e.Images, e => e.Ignore()); 
                // .ForMember(e => e.stringImageIds, e => e.MapFrom(d => string.Join(",", d.ImageIds.Select(x => x.ToString()).ToArray())));

        }
    }
}
