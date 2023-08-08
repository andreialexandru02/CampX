using AutoMapper;
using CampX.BusinessLogic.Implementations.Trips.Models;
using CampX.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampX.BusinessLogic.Implementations.Trips.Mapping
{
    public class TripCamperProfile : Profile
    {
        public TripCamperProfile()
        {
            CreateMap<TripCamperModel, TripCamper>();
                //.ForMember(t => t.TripId, t => t.MapFrom(t => t.));
        }
    }
}
