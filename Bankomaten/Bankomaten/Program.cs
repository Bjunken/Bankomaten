using Bankomaten.Models;
using Bankomaten.Service;
<<<<<<< HEAD
using Bankomaten.UI;
using Bankomaten.Utils;
=======
using Bankomaten.Utils;
using System;
using System.Collections.Generic;
>>>>>>> a3e59978d3dc86e706fc7dfa3a6265f950508f0e

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
<<<<<<< HEAD
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
=======
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
                    new Account("Salaryaccount", 800)
                })
            };

            // Same wiring order as the real program
            ValidationHelper validationHelper = new ValidationHelper();
            AccountService accountService = new AccountService();
            BankService bank = new BankService(users, validationHelper, accountService);

            }
            //Logged in successfully

            LoginMenu loginMenu = new LoginMenu();
            loginMenu.showloginmenu();

            Console.Clear();

            MainMenu mainMenu = new MainMenu();
            mainMenu.showMenu();

    

            Console.WriteLine("\nDone. Press Enter to exit.");
            Console.ReadLine();
        }

        // Helper so the balance checks are one line each
        static void PrintBalance(BankService bank, int index) {
            TransactionResult result = bank.GetBalance(index, out decimal balance);
            Console.WriteLine($"Balance [{index}]:      {result} / {balance}");
>>>>>>> a3e59978d3dc86e706fc7dfa3a6265f950508f0e
        }
    }
}