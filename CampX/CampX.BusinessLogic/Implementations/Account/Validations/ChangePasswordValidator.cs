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
    public class ChangePasswordValidator : AbstractValidator<RegisterModel>
    {
        public ChangePasswordValidator(UnitOfWork unitOfWork)
        {

            RuleFor(r => r.Password)
                .NotEmpty().WithMessage("Camp obligatoriu!")
                .Must(PasswordTooShort)
                .WithMessage("Parola trebuie sa aiba mai mult de 10 caractere");
            RuleFor(r => r.Password)
                .NotEmpty().WithMessage("Camp obligatoriu!")
                .Must(PasswordTooShort)
                .WithMessage("Parola trebuie sa aiba mai mult de 10 caractere");     
            RuleFor(r => r.ConfirmPassword)
                .NotEmpty().WithMessage("Camp obligatoriu!")
                .Equal(r => r.Password)
                .WithMessage("Cele 2 parole nu coincid!");
           

        }
      
        public bool PasswordTooShort(string password)
        {
            if (password.Length < 10)
            {
                return false;
            }
            return true; ;
      }
      
    }

}
