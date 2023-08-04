using CampX.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentValidation;
using System.Threading.Tasks;
using CampX.BusinessLogic.Implementations.Trip.Models;

namespace CampX.BusinessLogic.Implementations.Trip.Validations
{
    public class TripValidator : AbstractValidator<AddTripModel>
    {
        private readonly UnitOfWork _unitOfWork;
        public TripValidator(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            RuleFor(c => c.Name)
                .NotEmpty().WithMessage("Camp obligatoriu!");
            RuleFor(c => c.IsPublic)
                .NotEmpty().WithMessage("Camp obligatoriu!");
        }
    }
}
