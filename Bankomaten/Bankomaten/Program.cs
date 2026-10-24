using Bankomaten.Models;
using Bankomaten.Service;
using Bankomaten.UI;
using Bankomaten.Utils;

namespace Bankomaten {
    internal class Program {
        private static void Main(string[] args) {
            DataHandling dataHandling = new DataHandling();

            // loading in users from JSON and if JSON is empty, creates users
            List<User> users = dataHandling.Load();
            if (users.Count == 0) {
                users = createInitialUsers();
                dataHandling.Save(users);
            }

            // references to services
            ValidationHelper validationHelper = new ValidationHelper();
            AccountService accountService = new AccountService();
            BankService bankService = new BankService(users, validationHelper, accountService);

            LoginMenu loginMenu = new LoginMenu();
            MainMenu mainMenu = new MainMenu(bankService, users, dataHandling);

            // program loop (Login -> Main Menu -> back to login if logged out)
            while (true) {
                bool loggedIn = loginMenu.ShowLoginMenu(bankService);
                if (!loggedIn) {
                    return;
                }
                Console.Clear();
                mainMenu.showMenu();
            }
        }
        // method for creating the users
        private static List<User> createInitialUsers() {
            return new List<User> {
                new User("Linus", 1111, Account.AccountFactory.generateAccounts()),
                new User("Kristin", 2222, Account.AccountFactory.generateAccounts()),
                new User("Martin", 3333, Account.AccountFactory.generateAccounts()),
                new User("Anas", 4444, Account.AccountFactory.generateAccounts()),
                new User("Osama", 5555, Account.AccountFactory.generateAccounts())
            };
        }
    }
}