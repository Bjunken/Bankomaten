using System;
using System.Collections.Generic;
using System.Text;
using static Bankomaten.Models.Account;

namespace Bankomaten.UI
{
    internal class MainMenu
    {
        // =====================================================================
        // Menu to the banking system after the user has logged in successfully.
        // =====================================================================
        public void showMenu()
        {

            Console.WriteLine("What would you like to do?");
            Console.WriteLine();
            Console.WriteLine("[1] View balance");
            Console.WriteLine("[2] Transfer money");
            Console.WriteLine("[3] Withdraw money");
            Console.WriteLine("[4] Log out");
            Console.WriteLine();

            Console.Write("Choose an option: ");

            string choice = Console.ReadLine();

            Console.WriteLine();
            Console.WriteLine("---------------------------------");

            switch (choice)
            {
                case "1":
                    Console.WriteLine($"Your balance is:{loggedInUser.Accounts[1-6].Balance} kr");
                    break;

                case "2":
                    Console.WriteLine("Transfer");
                    Console.WriteLine("Enter amount:");
                    break;

                case "3":
                    Console.WriteLine("Withdrawal");
                    Console.WriteLine("Enter amount:");
                    break;

                case "4":
                    Console.WriteLine("You have been logged out.");
                    break;

                default:
                    Console.WriteLine("Invalid choice!");
                    break;
            }

            Console.WriteLine("---------------------------------");
            Console.WriteLine();
            Console.WriteLine("Press ENTER to continue.");
            Console.ReadLine();
        }
    }
}