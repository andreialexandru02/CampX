using AutoMapper;
using CampX.BusinessLogic.Implementations.Requests.Models;
using CampX.BusinessLogic.Implementations.Trips.Models;
using CampX.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CampX.BusinessLogic.Implementations.Requests.Mapping
{
    public class RequestProfile : Profile
    {
        public RequestProfile()
        {
            CreateMap<AddRequestModel, Request>();
            CreateMap<ShowTripsModel, Request>();
            CreateMap<RequestTripModel, Trip>();
        }
    }
}
