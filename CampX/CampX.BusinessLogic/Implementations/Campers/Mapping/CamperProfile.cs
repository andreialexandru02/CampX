using AutoMapper;
using CampX.BusinessLogic.Implementations.Campers.Models;
using CampX.BusinessLogic.Implementations.Requests.Models;
using CampX.BusinessLogic.Implementations.Trips.Models;
using CampX.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampX.BusinessLogic.Implementations.Campers.Mapping
{
    internal class CamperProfile : Profile
    {
        public CamperProfile()
        {
            CreateMap<CamperModel, Camper>();          
        }
    }
}
