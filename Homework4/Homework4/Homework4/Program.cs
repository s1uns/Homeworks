using Homework4.Models;
using MapperLib;
using ValidationLib;

namespace Homework4
{
    public class Program
    {
        static void Main(string[] args)
        {   ;
            var normalUser = new User("IlliaLoh", "MinecraftCreeper2004");
            var uimodel = new UIModel();
            normalUser.MapTo(uimodel);
            Console.WriteLine(uimodel.UserName);

            normalUser.Validate();
            var wrongUser = new User("Illasdqwqda?", "MinecraftCreeper2004");
            wrongUser.Validate();
        }
    }
}