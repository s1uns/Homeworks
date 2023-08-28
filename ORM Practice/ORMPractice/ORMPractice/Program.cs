using ORMPractice.Models;
using ORMPractice;

using (var context = new ORMPractice.AppContext())
{
    var repository1 = new Repository<Country>(context);
    var repository2 = new Repository<Language>(context);
    var repository3 = new Repository<Continent>(context);

    /*    var lang1 = new Language
        {
            Name = "ua"

        };
        repository2.Add(lang1);
        repository2.SaveChanges();*/
    /*    var country1 = new Country
        {
            Name = "Ukraine",
            Area = 100000.99M,
            NationalDay = DateTime.Now,
            CountryCode2 = 123,
            CountryCode3 = 456,
            RegionId = 1
        };

        var country2 = new Country
        {
            Name = "USA",
            Area = 100000.99M,
            NationalDay = DateTime.Now,
            CountryCode2 = 123,
            CountryCode3 = 456,
            RegionId = 10
        };
        repository1.Add(country1);
        repository1.SaveChanges();
        repository1.Add(country2);
        repository1.SaveChanges();*/

    /*    var allLanguages = repository1.GetAll();
        Console.WriteLine("All countries: ");
        foreach (var continent in allLanguages)
        {
            Console.WriteLine(continent.Name + " " + continent.CountryId);
        }*/

    /*    repository2.Delete();*/
    /*    repository2.SaveChanges();*/
    /* var allLanguages = repository2.GetAll();
     Console.WriteLine("All languages: ");
     foreach (var continent in allLanguages)
     {
         Console.WriteLine(continent.Name + " " + continent.LanguageId);
     }
     var lang = new Language();
     lang.LanguageId = 2;
     lang.Name = "ukr";
     repository2.Update(2, lang);
     context.SaveChanges();
     allLanguages = repository2.GetAll();
     Console.WriteLine("All languages: ");
     foreach (var continent in allLanguages)
     {
         Console.WriteLine(continent.Name + " " + continent.LanguageId);
     }*/

    var sortedContinents = repository3.Sort($"{nameof(Continent)}Id", false);
    foreach (var continent in sortedContinents)
    {
        Console.WriteLine(continent.Name + " " + continent.ContinentId);
    }
}