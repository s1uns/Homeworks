using Homework4.Models;
using ValidationLib;

namespace Homework4
{
    public class Program
    {
        static void Main(string[] args)
        {   var mapper = new MapperLib.Mapper();
            var normalUser = new User("IlliaLoh", "MinecraftCreeper2004");
            var uimodel = new UIModel();
            mapper.MapTo(normalUser, uimodel);
            Console.WriteLine(uimodel.UserName);

            normalUser.Validate();
            var wrongUser = new User("Illasdqwqda?", "MinecraftCreeper2004");
            wrongUser.Validate();
        }
    }
}