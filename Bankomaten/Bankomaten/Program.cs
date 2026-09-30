using Bankomaten.Models;

namespace Bankomaten
{
    internal class Program
    {
        static void Main(string[] args) 
        {
            User[] users =
            {
                new User("Linus", 1111),
                new User("Kristin", 2222),
                new User("William", 3333),
                new User("Martin", 4444),
                new User("Admin", 5555)
            };
        }
    }
}
