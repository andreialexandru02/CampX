using CampX.BusinessLogic.Implementations.Map.Models;
using CampX.BusinessLogic.Implementations.Notes.Models;
using CampX.DataAccess;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampX.BusinessLogic.Implementations.Notes.Validations
{
    public class NoteValidator : AbstractValidator<AddNoteModel>
    {
        
 
        public NoteValidator()
        {
        
            RuleFor(c => c.Content)
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
