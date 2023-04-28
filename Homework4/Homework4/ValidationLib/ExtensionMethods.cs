using FluentValidation;
using System.Reflection;

namespace ValidationLib
{
    public static class ExtensionMethods
    {
        public static void Validate<T>(this T objectToValidate)
        {
            var modelType = typeof(T);
            var assembly = Assembly.GetAssembly(modelType);
            var validatorType = assembly.GetTypes().FirstOrDefault(x => x.Name == String.Concat(modelType.Name, "Validator"));
            var validator = (IValidator<T>)Activator.CreateInstance(validatorType);
            var validation = validator.Validate(objectToValidate);
            if (!validation.IsValid)
            {
                foreach(var error in validation.Errors)
                {
                    Console.WriteLine($"There is an error: {error}");
                }
            }
            else
            {
                Console.WriteLine($"Validation of the object with {objectToValidate} type is successful");
            }
        }
    }
}