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
                .NotEmpty().WithMessage("Camp obligatoriu!");
            RuleFor(r => r.CampsiteId)
                .NotEmpty().WithMessage("Camp obligatoriu!");
        }
    }
}
