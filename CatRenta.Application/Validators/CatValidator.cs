using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace CatRenta.Application.Validators
{
   public class CatValidator : AbstractValidator<CatVM>
    {
        public CatValidator()
        {
            RuleFor(cat => cat.Name)
                .NotEmpty()
                .WithMessage("Please Specify a Name.");

            RuleFor(cat => cat.Price)
                .NotEmpty()
                .WithMessage("Please write price");

            RuleFor(cat => cat.Details)
                .NotEmpty()
                .WithMessage("Please write details.");

           

        }

        //private static bool BeAValidZip(string zip)
        //{
        //    if (!string.IsNullOrEmpty(zip))
        //    {
        //        var regex = new Regex(@"\d{5}");
        //        return regex.IsMatch(zip);
        //    }
        //    return false;
        //}
    }
}
