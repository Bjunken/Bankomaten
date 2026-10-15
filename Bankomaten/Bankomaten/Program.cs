using Bankomaten.Models;
using Bankomaten.Service;
using Bankomaten.Utils;
using System;
using System.Collections.Generic;

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
        }
    }
}