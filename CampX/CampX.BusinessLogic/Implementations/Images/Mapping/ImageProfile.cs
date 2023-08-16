using AutoMapper;
using CampX.BusinessLogic.Implementations.Images.Models;
using CampX.BusinessLogic.Implementations.Map.Models;
using CampX.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampX.BusinessLogic.Implementations.Images.Mapping
{
    public class ImageProfile : Profile
    {
        public ImageProfile()
        {

            CreateMap<ImageModel, Image>();

        }
    }
}
