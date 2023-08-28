using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORMPractice.Models
{
    public class Country_Language
    {
        [Key]
        public long CountryLanguageId { get; set; }

        public long CountryId { get; set; }

        public long LanguageId { get; set; }

        public bool Official { get; set; }
        public Language Language { get; set; }

        public Country Country { get; set; }
    }
}
