using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORMPractice.Models
{
    public class Continent
    {
        public long ContinentId { get; set; }

        public string Name { get; set; }
    }
}
