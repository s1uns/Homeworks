using Homework4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework4.Validators
{
    public class UserValidator : MyOwnVaidationLib.ObjectValidator<User>
    { 
        public UserValidator(User user) {
            Validate(user);
        }
    }
}
