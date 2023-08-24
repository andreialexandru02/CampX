using CampX.BusinessLogic.Implementations.Account.Models;
using CampX.Common.ViewModels;
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
    public class EditCamperValidator : AbstractValidator<EditCamperModel>
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly CurrentCamperDTO _currentCamper;

        public EditCamperValidator(UnitOfWork unitOfWork, CurrentCamperDTO currentCamper)
        {
            _unitOfWork = unitOfWork;
            _currentCamper = currentCamper;
            RuleFor(r => r.Email)
                .NotEmpty().WithMessage("Camp obligatoriu!")
                .Must(NotAlreadyExist)
                .WithMessage("Exista deja un utilizator cu acest email!")
                .EmailAddress(FluentValidation.Validators.EmailValidationMode.AspNetCoreCompatible);          
            RuleFor(r => r.FirstName)
                .NotEmpty().WithMessage("Camp obligatoriu!");            
            RuleFor(r => r.LastName)
                .NotEmpty().WithMessage("Camp obligatoriu!");
            RuleFor(r => r.BirthDay)
                .NotEmpty().WithMessage("Camp obligatoriu!")
                .Must(BeValidBirthDate).WithMessage("Camperii trebuie sa aiba minim 13 ani!");

        }

        public bool NotAlreadyExist(string email)
        {
            var camper = _unitOfWork.Campers.Get()
                .AsNoTracking()
                .SingleOrDefault(u => u.Email == email);
           
            if (camper == null)
            {
                return true;
            }
            if(camper.Id == _currentCamper.Id)
            {
                return true;
            }
            return false;

        }
        private bool BeValidBirthDate(DateTime birthDate)
        {
            return birthDate <= DateTime.Today.AddYears(-13);
        }
    }

}
