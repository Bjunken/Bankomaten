using Bankomaten.Models;
using Bankomaten.Service;
using Bankomaten.UI;

namespace Bankomaten
{
    internal class Program
    {
        static void Main(string[] args) 
        {
            List<User> users = new List<User>
            {
                new User("Linus", 1111, new List<Account>
                {
                    new Account("Salaryaccount", 12500.50m),
                    new Account("Savingsaccount", 5000.75m)
                }),
                new User("Kristin", 2222, new List<Account>
                {
                    new Account("Salaryaccount", 1500.50m),
                    new Account("Savingsaccount", 4586.75m),
                    new Account("Vacationaccount", 8563.75m)
                }),
                new User("William", 3333, new List<Account>
                {   new Account("Salaryaccount", 12547.50m), 
                    new Account("Savingsaccount", 5001.75m)
                }),
                new User("Martin", 4444, new List<Account>
                {   new Account("Salaryaccount", 4598.50m), 
                    new Account("Savingsaccount", 50.75m) 
                }),
                new User("Admin", 5555, new List<Account>
                {   new Account("Salaryaccount", 8963.50m),                      
                })
            };
            Console.WriteLine("Welcome to SyntaxSquad Bank");

            // below is to be added to the UI-Class. I put it here to make it work for now. -Kristin
            string username = Console.ReadLine();
            User loggedInUser = User.Login(users, username);

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
            AccountService.ShowAccounts(loggedInUser);

        }
    }
}
