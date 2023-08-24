using CampX.BusinessLogic.Implementations.Account.Models;
using CampX.DataAccess;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampX.BusinessLogic.Implementations.Account.Validations
{
    public class ChangePasswordValidator : AbstractValidator<ChangePasswordModel>
    {
        private readonly UnitOfWork _unitOfWork;
        public ChangePasswordValidator(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
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
            RuleFor(r => new {r.Id, r.OldPassword})
                .Must(r => CheckOldPassword(r.Id, r.OldPassword))
                                       .WithMessage("Parola gresita!");

        }
      
        public bool PasswordTooShort(string password)
        {
            if (password.Length < 10)
            {
                return false;
            }
            return true; ;
        }
        public bool CheckOldPassword(int id, string password)
        {
            var camper = _unitOfWork.Campers.Get()
                
                .SingleOrDefault(c => c.Id == id);

            var corectPassword = BCrypt.Net.BCrypt.EnhancedVerify(password, camper.Password);
            if (camper == null || !corectPassword)
            {
                return false;
            }
            return true;
        }

    }

}
