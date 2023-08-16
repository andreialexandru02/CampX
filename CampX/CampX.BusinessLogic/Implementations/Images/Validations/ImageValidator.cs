using CampX.BusinessLogic.Implementations.Images.Models;
using CampX.BusinessLogic.Implementations.Trips.Models;
using CampX.DataAccess;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampX.BusinessLogic.Implementations.Images.NewFolder
{
    public class ImageValidator : AbstractValidator<ImageModel>
    {
        private readonly UnitOfWork _unitOfWork;
        public ImageValidator(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            RuleFor(i => i.ImageData)
                .NotEmpty().WithMessage("Camp obligatoriu!");

        }
    }
}
