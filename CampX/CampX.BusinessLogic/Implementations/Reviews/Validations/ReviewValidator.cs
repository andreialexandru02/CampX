using CampX.BusinessLogic.Implementations.Map.Models;
using CampX.BusinessLogic.Implementations.Reviews.Models;
using CampX.DataAccess;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampX.BusinessLogic.Implementations.Reviews.Validations
{
    public class ReviewValidator : AbstractValidator<AddReviewModel>
    {
        private readonly UnitOfWork _unitOfWork;
        public ReviewValidator(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            RuleFor(r => r.Rating)
                .NotEmpty().WithMessage("Camp obligatoriu!");
            RuleFor(r => r.CampsiteId)
                .NotEmpty().WithMessage("Camp obligatoriu!");
            RuleFor(r => r.Content)
                .Must(DescriptionTooLong)
                .WithMessage("Continutul este prea lung!");
        }

        public bool DescriptionTooLong(string description)
        {
            if (description == null)
                return true;
            return description.Length <= 500;
        }
    }
}
