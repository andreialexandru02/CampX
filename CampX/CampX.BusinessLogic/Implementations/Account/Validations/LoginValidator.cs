using CampX.BusinessLogic.Implementations.Account.Models;
using CampX.DataAccess;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampX.BusinessLogic.Implementations.Account.Validations
{
    public class LoginValidator : AbstractValidator<RegisterModel>
    {

        public LoginValidator(UnitOfWork unitOfWork)
        {
            RuleFor(r => r.Email)
                .NotEmpty().WithMessage("Camp obligatoriu!")
                .EmailAddress(FluentValidation.Validators.EmailValidationMode.AspNetCoreCompatible);
            RuleFor(r => r.Password)
                .NotEmpty().WithMessage("Camp obligatoriu!");
        }    
    }
}
