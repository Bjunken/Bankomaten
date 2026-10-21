using Bankomaten.Models;
using Bankomaten.Service;
using Bankomaten.UI;
using Bankomaten.Utils;

namespace Bankomaten
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            // Load saved users first; create the starter accounts if no saved data exists.
            var dataHandling = new DataHandling();
            List<User> users = dataHandling.Load();

            if (users.Count == 0)
            {
                users = CreateInitialUsers();
                dataHandling.Save(users);
            }

            var validator = new ValidationHelper();
            var bank = new BankService(users, validator, new AccountService());
            var loginMenu = new LoginMenu();
            var mainMenu = new MainMenu(bank, users, dataHandling);

            // Return to the login menu after the user logs out.
            while (true)
            {
                if (!loginMenu.ShowLoginMenu(bank))
                    return;
                Console.Clear();
                mainMenu.ShowMenu();
            }
        }

        private static List<User> CreateInitialUsers()
        {
            //Creates a list of users with their usernames, PINs, and accounts
            return new List<User>
            {
                new User("Linus", 1111, Account.AccountFactory.GenerateAccounts()),
                new User("Kristin", 2222, Account.AccountFactory.GenerateAccounts()),
                new User("William", 3333, Account.AccountFactory.GenerateAccounts()),
                new User("Martin", 4444, Account.AccountFactory.GenerateAccounts()),
                new User("Anas", 5555, Account.AccountFactory.GenerateAccounts())
            };
        }
    }
}