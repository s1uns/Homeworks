using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORMPractice.Models
{
    public class Country
    {
        public long CountryId { get; set; }

        public string Name { get; set; }

        public decimal Area { get; set; }

        public DateTime NationalDay { get; set; }

        public long CountryCode2 { get; set; }

        public long CountryCode3 { get; set; }

        public long RegionId { get; set; }

        public Region Region { get; set; }
    }
}
