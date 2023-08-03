using CampX.BusinessLogic.Implementations.Account.Models;
using CampX.BusinessLogic.Implementations.Map.Models;
using CampX.DataAccess;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampX.BusinessLogic.Implementations.Map.Validations
{
    public class CampsiteValidator : AbstractValidator<AddCampsiteModel>
    {
        private readonly UnitOfWork _unitOfWork;
        public CampsiteValidator(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            RuleFor(c => c.Name)
                .NotEmpty().WithMessage("Camp obligatoriu!");            
                
            RuleFor(c => c.Difficulty)
                .NotEmpty().WithMessage("Camp obligatoriu!");
            RuleFor(c => c.Latitude)
                .NotEmpty().WithMessage("Camp obligatoriu!");
            RuleFor(c => c.Longitude)
                .NotEmpty().WithMessage("Camp obligatoriu!");

        }
    }
}
