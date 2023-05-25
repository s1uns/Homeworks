
using System.Reflection;
using ValidationLib.Interfaces;

namespace ValidationLib
{
    public static class ExtensionMethods
    {
        public static void Validate<T>(this T objectToValidate)
        {
            var modelType = typeof(T);
            var assembly = Assembly.GetAssembly(modelType);
            var validatorType = assembly.GetTypes().FirstOrDefault(x => x.Name == String.Concat(modelType.Name, "Validator"));
            ConstructorInfo constructor = validatorType.GetConstructor(new[] { modelType });
            var validator = (IValidator)constructor.Invoke(new object[] { objectToValidate} );
            var validation = validator.Validate(objectToValidate, out Queue<string> errors);
            if (!validation)
            {
                foreach(var error in errors)
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