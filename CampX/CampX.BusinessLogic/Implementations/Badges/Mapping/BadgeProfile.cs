using AutoMapper;
using CampX.BusinessLogic.Implementations.Badges.Models;
using CampX.BusinessLogic.Implementations.Campers.Models;
using CampX.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampX.BusinessLogic.Implementations.Badges.Mapping
{
    internal class BadgeProfile : Profile
    {
        public BadgeProfile()
        {
            CreateMap<BadgeModel, Badge>();
            CreateMap<CamperBadgeModel, CamperBadge>();
        }
    }

}
