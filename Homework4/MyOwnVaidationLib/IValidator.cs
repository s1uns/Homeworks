using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyOwnVaidationLib
{
    public interface IValidator<T>
    {
        public ValidationResult Validate<T>(T objectToValidate);
    }
}
