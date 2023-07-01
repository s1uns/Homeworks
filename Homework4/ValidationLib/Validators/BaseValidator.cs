using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ValidationLib.Interfaces;

namespace ValidationLib.Validators
{
    public class BaseValidator : IValidator
    {
        public virtual bool Validate(object value, out Queue<string> errors)
        {
            errors = null;
            return true;
        }

        public bool IsLengthBetweeen(string value, int minLength, int maxLength)
        {
            return value.Length <= minLength && value.Length > maxLength;
        }

        public bool IsMaxLength(string value, int maxLength)
        {
            return value.Length > maxLength;
        }

        public bool IsMinLength(string value, int minLength)
        {
            return value.Length <= minLength;
        }

        public bool RestrictedSymbols(string value)
        {
            var restrictedSymbols = new char[] { '_', '*', '!', '?', '$' };
            bool res = false;
            foreach (var symbol in value)
            {
                if (restrictedSymbols.Contains(symbol))
                {
                    res = true;
                }
            }
            return res;
        }
    }
}
