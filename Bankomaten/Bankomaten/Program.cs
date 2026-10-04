using Bankomaten.Models;
using Bankomaten.Service;
using Bankomaten.UI;
using System.Reflection.Metadata;
using static Bankomaten.Models.Account;

namespace Bankomaten
{
    internal class Program
    {
        static void Main(string[] args) 
        {
            //Creates a list of users with their usernames, PINs, and accounts
            List<User> users = new List<User>();

            users.Add(new User("Linus", 1111, AccountFactory.GenerateAccounts()));
            users.Add(new User("Kristin", 2222, AccountFactory.GenerateAccounts()));
            users.Add(new User("William", 3333, AccountFactory.GenerateAccounts()));
            users.Add(new User("Martin", 4444, AccountFactory.GenerateAccounts()));
            users.Add(new User("Anas", 5555, AccountFactory.GenerateAccounts()));
            //Login process
            Console.Write("Username: ");
            string username = Console.ReadLine();

            User loggedInUser = users.Find(u => u.UserName == username);
            if (loggedInUser == null)
            {
                Console.WriteLine("Unknown user.");
                return;
            }
            //Pin-validation
            while (true)
            {
                if (loggedInUser.IsLocked)
                {
                    Console.WriteLine("Your account is locked. Please try again later.");
                    return;
                }
                Console.Write("Enter PIN: ");
                if(!int.TryParse(Console.ReadLine(), out int enteredPin))
                {
                    Console.WriteLine("Invalid PIN format. Please enter a numeric PIN.");
                    continue;
                }
                bool isValid = ValidationHelper.VerifyPin(loggedInUser, enteredPin);
                
                if (!isValid)
                {
                    Console.WriteLine("Invalid PIN. Please try again.");
                    continue;
                }
                break;

            }
            //Logged in successfully        
            Console.WriteLine($"Welcome lovely {loggedInUser.UserName}");
            AccountService.ShowAccounts(loggedInUser);








        }
    }
}
