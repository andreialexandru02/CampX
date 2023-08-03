using CampX.BusinessLogic.Base;
using CampX.BusinessLogic.Implementations.Account;
using CampX.BusinessLogic.Implementations.Account.Models;
using CampX.DataAccess;
using FluentValidation;


namespace CampX.BusinessLogic.Implementations.Account
{
    public class RegisterCamperValidator : AbstractValidator<RegisterModel>
    {
        private readonly UnitOfWork _unitOfWork;

        public RegisterCamperValidator(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            RuleFor(r => r.Email)
                .NotEmpty().WithMessage("Camp obligatoriu!")
                .Must(NotAlreadyExist).WithMessage("Exista deja un utilizator cu acest email!")
                .EmailAddress(FluentValidation.Validators.EmailValidationMode.AspNetCoreCompatible);
            RuleFor(r => r.Password)
                .NotEmpty().WithMessage("Camp obligatoriu!")
                .Must(PasswordTooShort).WithMessage("Parola trebuie sa aiba mai mult de 10 caractere");
            RuleFor(r => r.FirstName)
                .NotEmpty().WithMessage("Camp obligatoriu!");
            RuleFor(r => r.LastName)
                .NotEmpty().WithMessage("Camp obligatoriu!");
            RuleFor(r => r.BirthDay)
                .NotEmpty().WithMessage("Camp obligatoriu!");
            
        }

        public bool NotAlreadyExist(string email)
        {
            var camper = _unitOfWork.Campers.Get()
               .SingleOrDefault(u => u.Email == email);

            if (camper == null)
            {
                return true;
            }
            return false;

        }
        public bool PasswordTooShort(string password)
        {
            if(password.Length < 10)
            {
                return false;
            }
            return true; ;
        }
    }
}
