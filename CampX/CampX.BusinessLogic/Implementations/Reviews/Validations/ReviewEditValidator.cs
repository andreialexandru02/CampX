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
    public class ReviewEditValidator : AbstractValidator<EditReviewModel>
    {
        private readonly UnitOfWork _unitOfWork;
        public ReviewEditValidator(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            RuleFor(r => r.Rating)
                .NotEmpty().WithMessage("Câmp obligatoriu!");
            RuleFor(r => r.CampsiteId)
                .NotEmpty().WithMessage("Câmp obligatoriu!");
            RuleFor(r => r.Content)
                .Must(DescriptionTooLong)
                .WithMessage("Conținutul este prea lung!");
        }
        public bool DescriptionTooLong(string description)
        {
            if (description == null)
                return true;
            return description.Length <= 500;
        }
    }
}
