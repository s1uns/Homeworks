using MyOwnORM.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyOwnORM.Models
{
    public class NewEntity<T>
    {
        public T data { get; set; }

        public State State { get; set; }
    }
}