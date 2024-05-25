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
                .NotEmpty().WithMessage("Câmp obligatoriu!")
                .Must(NameTooLong)
                .WithMessage("Numele este prea lung");
            RuleFor(c => c.Description)
                .Must(DescriptionTooLong)
                .WithMessage("Descrierea este prea lunga!");
            RuleFor(c => c.Difficulty)
                .NotEmpty().WithMessage("Câmp obligatoriu!");
            RuleFor(c => c.Latitude)
                .NotEmpty().WithMessage("Câmp obligatoriu!");
            RuleFor(c => c.Longitude)
                .NotEmpty().WithMessage("Câmp obligatoriu!");

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