using Bankomaten.Models;
using Bankomaten.Service;
using Bankomaten.Utils;
using System;
using System.Collections.Generic;

namespace Bankomaten
{
    internal class Program
    {
        static void Main(string[] args) {
            // Test data: two users with known PINs and balances
            List<User> users = new List<User>
            {
                new User("Linus", 1234, new List<Account>
                {
                    new Account("Salaryaccount", 1000),
                    new Account("Savingsaccount", 500)
                }),
                new User("Anna", 4321, new List<Account>
                {
                    new Account("Salaryaccount", 800)
                })
            };

            // Same wiring order as the real program
            ValidationHelper validationHelper = new ValidationHelper();
            AccountService accountService = new AccountService();
            BankService bank = new BankService(users, validationHelper, accountService);

            Console.WriteLine("--- LOGIN ---");
            Console.WriteLine("Unknown user:      " + bank.Login("Nobody", "1234"));      // NoUser
            Console.WriteLine("PIN not a number:  " + bank.Login("Linus", "abc"));        // InvalidPinFormat
            Console.WriteLine("Wrong PIN:         " + bank.Login("Linus", "9999"));       // WrongPin
            Console.WriteLine("Correct (lowercase name): " + bank.Login("linus", "1234")); // SuccessfulLogin
            Console.WriteLine("Logged in as:     " + bank.CurrentUser?.UserName);

            Console.WriteLine("\n--- BALANCE / DEPOSIT ---");
            PrintBalance(bank, 0);                                                         // 1000
            Console.WriteLine("Deposit 500:       " + bank.Deposit(500, 0));              // Successful
            PrintBalance(bank, 0);                                                         // 1500
            Console.WriteLine("Deposit -5:        " + bank.Deposit(-5, 0));               // InvalidAmount
            Console.WriteLine("Deposit bad index: " + bank.Deposit(100, 99));             // InvalidAccountIndex

            Console.WriteLine("\n--- WITHDRAW ---");
            Console.WriteLine("Withdraw 200:      " + bank.Withdraw(200, 0, "1234"));     // Successful
            PrintBalance(bank, 0);                                                         // 1300
            Console.WriteLine("Withdraw 99999:    " + bank.Withdraw(99999, 0, "1234"));   // InsufficientFunds
            PrintBalance(bank, 0);                                                         // still 1300
            Console.WriteLine("Wrong PIN:         " + bank.Withdraw(50, 0, "0000"));      // WrongPin
            Console.WriteLine("PIN not a number:  " + bank.Withdraw(50, 0, "abc"));       // InvalidPinFormat

            Console.WriteLine("\n--- TRANSFER ---");
            Console.WriteLine("Transfer 300 0->1: " + bank.Transfer(0, 1, 300));          // Successful
            PrintBalance(bank, 0);                                                         // 1000
            PrintBalance(bank, 1);                                                         // 800
            Console.WriteLine("Same account:      " + bank.Transfer(0, 0, 100));          // SameAccount
            Console.WriteLine("Too much:          " + bank.Transfer(0, 1, 99999));        // InsufficientFunds

            Console.WriteLine("\n--- LOGOUT ---");
            bank.Logout();
            Console.WriteLine("Logged in as:      " + (bank.CurrentUser?.UserName ?? "nobody"));
            Console.WriteLine("Deposit logged out: " + bank.Deposit(100, 0));             // NonLoggedIn
            PrintBalance(bank, 0);                                                         // NonLoggedIn

            Console.WriteLine("\n--- LOCKOUT (Anna) ---");
            Console.WriteLine("Wrong PIN 1:       " + bank.Login("Anna", "1111"));        // WrongPin
            Console.WriteLine("Wrong PIN 2:       " + bank.Login("Anna", "2222"));        // WrongPin
            Console.WriteLine("Wrong PIN 3:       " + bank.Login("Anna", "3333"));        // WrongPin (+ lock message)
            Console.WriteLine("Correct PIN now:   " + bank.Login("Anna", "4321"));        // UserLocked
            Console.WriteLine("Waiting 31 seconds for the lock to expire...");
            System.Threading.Thread.Sleep(31000);
            Console.WriteLine("After 31 seconds:  " + bank.Login("Anna", "4321"));        // SuccessfulLogin

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