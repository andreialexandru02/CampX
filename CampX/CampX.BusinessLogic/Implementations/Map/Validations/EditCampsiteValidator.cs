using CampX.BusinessLogic.Implementations.Map.Models;
using CampX.DataAccess;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampX.BusinessLogic.Implementations.Map.Validations
{


    public class EditCampsiteValidator : AbstractValidator<EditCampsiteModel>
    {
        public EditCampsiteValidator()
        {
            
            RuleFor(c => c.Name)
                .NotEmpty().WithMessage("Câmp obligatoriu!")
                .Must(NameTooLong)
                .WithMessage("Numele este prea lung");
            RuleFor(c => c.Description)
                .Must(DescriptionTooLong)
                .WithMessage("Descrierea este prea lungă!");
            RuleFor(c => c.Difficulty)
                .NotEmpty().WithMessage("Câmp obligatoriu!");
            RuleFor(c => c.Latitude)
                .NotEmpty().WithMessage("Câmp obligatoriu!");
            RuleFor(c => c.Longitude)
                .NotEmpty().WithMessage("Câmp obligatoriu!");
            RuleFor(c => c.Images)
                .Must(IsImageExtensionCorrect)
                .WithMessage("Format invalid, selectează poze!");



        }
            public bool NameTooLong(string name)
            {
                return name.Length <= 100;
            }
            public bool DescriptionTooLong(string description)
            {
                if (description == null)
                    return true;
                return description.Length <= 500;
            }
        private bool IsImageExtensionCorrect(List<IFormFile> images)
        {
            var ok = true;

            if (images == null)
            {
                ok = false;
            }
            foreach (var image in images)
            {

                if (image == null)
                {
                    ok = false;
                    break;
                }
                var acceptedContentTypes = new List<string>()
                    {
                        "image/gif",
                        "image/jpeg",
                        "image/jpg",
                        "image/png",
                        "image/jfif",
                        "image/GIF",
                        "image/JPEG",
                        "image/JPG",
                        "image/PNG",
                        "image/JFIF"
                    };
                ok = ok && acceptedContentTypes.Contains(image.ContentType) ;

            }
            return ok;

        }
    }
}
