using FluentValidation;
using Homework4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework4.Validators
{
    public class UserValidator : ValidationLib.GenericValidator<User>
    {
        public UserValidator()
        {
            RuleFor(x => x.UserName)
                    .NotEmpty()
                    .MinimumLength(5)
                    .MaximumLength(15)
                    .Must(RestrictedSymbols)
                    .WithMessage("Username is required. It shouldn't be less than 5 or more than 15 symbols. It also shouldn't have restricted symbols.");
            RuleFor(x => x.Password)
                    .NotEmpty()
                    .MinimumLength(8)
                    .MaximumLength(25)
                    .WithMessage("Password is required. It shouldn't be less than 8 or more than 25 symbols.");


        }

        private bool RestrictedSymbols(string username)
        {
            var restrictedSymbols = new char[] { '_', '*', '!', '?', '$' };
            bool res = true;
            foreach (var symbol in username)
            {
                if (restrictedSymbols.Contains(symbol))
                {
                    res = false;
                }
            }
            return res;
        }
    }
}
