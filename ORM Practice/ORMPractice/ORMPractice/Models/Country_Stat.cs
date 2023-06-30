using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORMPractice.Models
{
    public class Country_Stat
    {
        [Key]
        public long CountryYearId { get; set; }

        public long CountryId { get; set; }

        public long Year { get; set; }

        public int Population { get; set; }

        public decimal GDP { get; set; }

        public Country Country { get; set; }
    }
}
