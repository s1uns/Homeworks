using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyOwnVaidationLib
{
    public class ValidationResult
    {
        public ValidationResult()
        {
            Errors = new Dictionary<string, List<string>>();
        }

        public bool IsValid => !this.Errors.Any();
        public IDictionary<string, List<string>> Errors { get; }

        public void AddError(string name, string message)
        {
            if(!Errors.ContainsKey(name)) 
            {
                Errors[name] = new List<string>();
            }

            this.Errors[name].Add(message);
        }
    }
}
