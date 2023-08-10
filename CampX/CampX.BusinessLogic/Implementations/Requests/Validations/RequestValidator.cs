using CampX.BusinessLogic.Implementations.Requests.Models;
using CampX.BusinessLogic.Implementations.Trips.Models;
using CampX.DataAccess;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampX.BusinessLogic.Implementations.Requests.Validations
{
    public class RequestValidator : AbstractValidator<AddRequestModel>
    {
        public RequestValidator()
        {

            RuleFor(c => c.TripId)
                .NotEmpty().WithMessage("Camp obligatoriu!");
            RuleFor(c => c.CamperId)
               .NotEmpty().WithMessage("Camp obligatoriu!");
        }
    }
}
