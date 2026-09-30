using Bankomaten.Models;
using Bankomaten.UI;

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
            Console.WriteLine("Welcome to SyntaxSquad Bank");
            User loggedInUser = User.Login (users);
            
            if (loggedInUser == null)
            {
                Console.WriteLine("Too many failed login attempts.");
                return;
            }
            Console.WriteLine($"Welcome lovely {loggedInUser.UserName}");

        }
    }
}
