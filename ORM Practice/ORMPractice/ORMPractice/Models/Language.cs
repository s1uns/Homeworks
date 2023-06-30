using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORMPractice.Models
{
    public class Language
    {
        public long LanguageId { get; set; }

        public string Name { get; set; }
    }
}
