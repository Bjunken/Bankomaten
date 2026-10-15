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
            User loggedInUser = null;
            while (true)
            {
                Console.Write("Username: ");
                string username = Console.ReadLine();

                loggedInUser = users.Find(u => u.UserName == username);
                if (loggedInUser == null)
                {
                    Console.WriteLine("Unknown user.");
                    continue; //returns to the beginning of the loop to ask for username again.
                }
                //Pin-validation

                if (loggedInUser.IsLocked)
                {
                    Console.WriteLine("Your account is locked. Please try again later.");
                    continue; //returns to the beginning of the loop to ask for username again.
                }
                Console.Write("Enter PIN: ");
                if (!int.TryParse(Console.ReadLine(), out int enteredPin))
                {
                    Console.WriteLine("Invalid PIN format. Please enter a numeric PIN.");
                    continue;
                }
                var validator = new ValidationHelper();
                bool isValid = validator.VerifyPin(loggedInUser, enteredPin);

                if (!isValid)
                {
                    continue;
                }
                break; // Exit the loop after successful login and account display                          


            }
            //Logged in successfully

            LoginMenu loginMenu = new LoginMenu();
            loginMenu.showloginmenu();

            Console.Clear();

            MainMenu mainMenu = new MainMenu();
            mainMenu.showMenu();

    


        }
    }
}
