using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace MyOwnVaidationLib
{
    public class ObjectValidator<T> : IValidator<T>
    {

        public ValidationResult Validate<T>(T objToValidate)
        {
            if (objToValidate == null)
            {
                var result = new ValidationResult();
                result.AddError("Object", "Object is null");

                return result;
            }

            var objectType = objToValidate.GetType();

            var properties = objectType.GetProperties();

            var validationResult = new ValidationResult();
            foreach ( var property in properties )
            {
                var propertyName = property.Name;
                var propertyValue = property.GetValue(objToValidate);

                var attributes = property.GetCustomAttributes<ValidationAttribute>();

                foreach ( var attribute in attributes )
                {
                    var isValid = attribute.IsValid(propertyValue);

                    if(!isValid )
                    {
                        var errorMessage = attribute.FormatErrorMessage(propertyName);

                        validationResult.AddError(propertyName, errorMessage);
                    }
                }
            }
            return validationResult;
        }
    }
}