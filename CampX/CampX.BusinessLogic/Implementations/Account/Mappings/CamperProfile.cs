using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CampX.BusinessLogic.Implementations.Account.Models;
using CampX.Entities;

namespace CampX.BusinessLogic.Implementations.Account.Mapping
{
    public class CamperProfile : Profile
    {
        public CamperProfile() {

            CreateMap<RegisterModel, Camper>()
                            .ForMember(a => a.Password, a => a.MapFrom(s => BCrypt.Net.BCrypt.EnhancedHashPassword(s.Password, 13)))
                            .ForMember(a => a.BirthDate, a => a.MapFrom(s => s.BirthDay));
                            //.ForMember(a => a.IdImage, a => a.MapFrom("2"));
        }
    }
}
