using AutoMapper;
using CampX.BusinessLogic.Implementations.Badges.Models;
using CampX.BusinessLogic.Implementations.Campers.Models;
using CampX.BusinessLogic.Implementations.Nights.Models;
using CampX.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampX.BusinessLogic.Implementations.Nights.Mapping
{
    public class NightProfile : Profile
    {
        public NightProfile()
        {
            CreateMap<AddNightsModel, Night>();

        }
    }


}
