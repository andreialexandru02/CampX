using AutoMapper;
using CampX.BusinessLogic.Implementations.Trips.Models;
using CampX.DataAccess;
using CampX.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampX.BusinessLogic.Implementations.Trips.Mapping
{
    public class TripProfile : Profile
    {

        public TripProfile()
        {
            CreateMap<AddTripModel, Trip>()
                .ForMember(t => t.Campsites, t => t.Ignore())
                .ForMember(t => t.TripCampers, t => t.Ignore());

            CreateMap<TripCamperModel, TripCamper>();
            CreateMap<TripCamperIdModel, TripCamper>();
            CreateMap<TripNoFKModel, Trip>();
          
        }
    }
}
