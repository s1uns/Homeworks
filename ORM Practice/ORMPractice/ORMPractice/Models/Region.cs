using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORMPractice.Models
{
    public class Region
    {
        public long RegionId { get; set; }

        public string Name { get; set; }

        public long ContinentId { get; set; }

        public Continent Continent { get; set; }
    }
}
