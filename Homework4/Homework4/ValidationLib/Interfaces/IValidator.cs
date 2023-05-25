using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ValidationLib.Interfaces
{
    public interface IValidator
    {
        public bool Validate(object value, out Queue<string> errors);

        public bool IsMinLength(string value, int minLength);
        public bool IsMaxLength(string value, int maxLength);

        public bool IsLengthBetweeen(string value, int minLength, int maxLength);
        public bool RestrictedSymbols(string value);
    }
}
