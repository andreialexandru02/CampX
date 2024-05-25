using CampX.BusinessLogic.Base;
using CampX.BusinessLogic.Implementations.Account;
using CampX.BusinessLogic.Implementations.Account.Models;
using CampX.DataAccess;
using FluentValidation;
using System.Text.RegularExpressions;

namespace CampX.BusinessLogic.Implementations.Account
{
    public class RegisterCamperValidator : AbstractValidator<RegisterModel>
    {
        private readonly UnitOfWork _unitOfWork;

        public RegisterCamperValidator(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            RuleFor(r => r.Email)
                .NotEmpty().WithMessage("Câmp obligatoriu!")
                .Must(NotAlreadyExist)
                .WithMessage("Există deja un utilizator cu acest email!")
                //.EmailAddress(FluentValidation.Validators.EmailValidationMode.AspNetCoreCompatible)
                .Must(IsValidEmail)
                .WithMessage("Formatul email-ului nu este corect!")
                .Must(InputTooLong)
                .WithMessage("Email-ul este prea lung");
            RuleFor(r => r.Password)
                .NotEmpty().WithMessage("Câmp obligatoriu!")
                .Must(PasswordTooShort)
                .WithMessage("Parola trebuie să aibă mai mult de 10 caractere");
            RuleFor(r => r.FirstName)
                .NotEmpty().WithMessage("Câmp obligatoriu!")
                .Must(InputTooLong)
                .WithMessage("Prenumele este prea lung");
            RuleFor(r => r.ConfirmPassword)
                .NotEmpty().WithMessage("Câmp obligatoriu!")
                .Equal(r => r.Password)
                .WithMessage("Cele 2 parole nu coincid!");
            RuleFor(r => r.LastName)
                .NotEmpty().WithMessage("Câmp obligatoriu!")
                .Must(InputTooLong)
                .WithMessage("Numele de familie este prea lung");
            RuleFor(r => r.BirthDay)
                .NotEmpty().WithMessage("Câmp obligatoriu!")
                .Must(BeValidBirthDate).WithMessage("Camperii trebuie să aibă minim 13 ani!");
            
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
        public bool CheckPassword(string password, string confirmPassword)
        {
            return password == confirmPassword;
        }
        private bool BeValidBirthDate(DateTime birthDate)
        {
            return birthDate <= DateTime.Today.AddYears(-13);
        }
        public bool InputTooLong(string name)
        {
            return name.Length <= 100;
        }
        public static bool IsValidEmail(string email)
        {
           
            string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";

            return Regex.IsMatch(email, pattern);
        }
    }
}
