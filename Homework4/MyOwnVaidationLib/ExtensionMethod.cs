using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.ComponentModel.DataAnnotations;
using MyOwnVaidationLib;
using System.Xml;

namespace ValidationLib
{
    public static class ExtensionMethods
    {
        public static void Validate<T>(this T objectToValidate)
        {
            var modelType = typeof(T);
            var assembly = Assembly.GetAssembly(modelType);
            var validatorType = assembly.GetTypes().FirstOrDefault(x => x.Name == String.Concat(modelType.Name, "Validator"));
            ConstructorInfo constructor = validatorType.GetConstructor(new[] { typeof(T) });
            var validator = (IValidator<T>)constructor.Invoke(new object[] { objectToValidate });
            var validationResult = validator.Validate(objectToValidate);
            if (!validationResult.IsValid)
            {   
                foreach (var errorName in validationResult.Errors.Keys)
                {
                    Console.WriteLine($"An error occured while validating a field of the object: ");
                    Console.WriteLine($"{errorName}: ");
                    foreach(var error in validationResult.Errors[errorName])
                    {
                        Console.WriteLine(error);
                    }
                    Console.WriteLine("-----------------------------------------");
                }
            }
            else
            {
                Console.WriteLine($"Validation of the object is successful");
                Console.WriteLine("-----------------------------------------");
            }
        }
    }
}