using AutoMapper;
using CampX.BusinessLogic.Implementations.Reviews.Models;
using CampX.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampX.BusinessLogic.Implementations.Reviews.Mapping
{
    public class ReviewEditProfile : Profile
    {
        public ReviewEditProfile()
        {
            CreateMap<EditReviewModel, Review>();

        }
    }
}
