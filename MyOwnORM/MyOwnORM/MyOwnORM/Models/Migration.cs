using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyOwnORM.Models
{
    public class Migration : BaseEntity
    {
        public string AppVersion { get; set; }
    }
}