using Microsoft.EntityFrameworkCore;
using ORMPractice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORMPractice
{
    public class AppContext : DbContext
    {
        public DbSet<Language> Languages { get; set; }
        public DbSet<Continent> Continents { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Country_Stat> Country_Stats { get; set; }
        public DbSet<Country_Language> Country_Languages { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"data source=(localdb)\MSSQLLocalDB;initial catalog=ORMPractice;trusted_connection=true");
        }
    }
}
