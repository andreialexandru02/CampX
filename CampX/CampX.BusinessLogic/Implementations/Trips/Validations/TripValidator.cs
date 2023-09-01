using CampX.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentValidation;
using System.Threading.Tasks;
using CampX.BusinessLogic.Implementations.Trips .Models;

namespace CampX.BusinessLogic.Implementations.Trips.Validations
{
    public class TripValidator : AbstractValidator<AddTripModel>
    {
        private readonly UnitOfWork _unitOfWork;
        public TripValidator(UnitOfWork unitOfWork)
        {
            RuleFor(c => c.Name)
                .NotEmpty().WithMessage("Camp obligatoriu!")
                .Must(NameTooLong)
                .WithMessage("Numele este prea lung");
            RuleFor(c => c.Description)
                .Must(DescriptionTooLong)
                .WithMessage("Descrierea este prea lunga!");
            RuleFor(c => c.IsPublic)
                .NotEmpty().WithMessage("Camp obligatoriu!");
        }
        public bool NameTooLong(string name)
        {
            return name.Length <= 100;
        }
        public bool DescriptionTooLong(string description)
        {
            if (description == null)
                return true;
            return description.Length <= 500;
        }
    }
}
