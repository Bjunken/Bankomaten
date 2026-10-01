using Bankomaten.Models;
using Bankomaten.Service;
using Bankomaten.UI;

namespace Bankomaten
{
    internal class Program
    {
        static void Main(string[] args) 
        {
            User[] users =
            {
                new User("Linus", 1111, new Account []
                {
                    new Account("Salaryaccount", 12500.50m),
                    new Account("Savingsaccount", 5000.75m)
                }),
                new User("Kristin", 2222, new Account []
                {
                    new Account("Salaryaccount", 1500.50m),
                    new Account("Savingsaccount", 4586.75m),
                    new Account("Vacationaccount", 8563.75m)
                }),
                new User("William", 3333, new Account[] 
                {   new Account("Salaryaccount", 12547.50m), 
                    new Account("Savingsaccount", 5001.75m)
                }),
                new User("Martin", 4444, new Account[] 
                {   new Account("Salaryaccount", 4598.50m), 
                    new Account("Savingsaccount", 50.75m) 
                }),
                new User("Admin", 5555, new Account[] 
                {   new Account("Salaryaccount", 8963.50m),                      
                })
            };
            Console.WriteLine("Welcome to SyntaxSquad Bank");
            User loggedInUser = User.Login (users);
            
            if (loggedInUser == null)
            {
                Console.WriteLine("Unknown user.");
                return;
            }

            if (!ValidationHelper.VerifyPin(loggedInUser))
            {
                Console.WriteLine("To many failed login attempts.");
                return;
            }
            Console.WriteLine($"Welcome lovely {loggedInUser.UserName}");
            Account.ShowAccounts(loggedInUser);

        }
    }
}
