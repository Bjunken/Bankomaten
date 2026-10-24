using System;
using System.Collections.Generic;
using System.Text;

namespace Bankomaten.Models  {
    public class Account  {
        public string AccountName { get; } // <- only have a get because it should not be able to be set outside of a method
        public decimal Balance { get; private set; } // <- public get so balance can be shown everywhere, while private set to ensure balance can only be set within the class
        public Account(string accountName, decimal balance) {
            AccountName = accountName;
            Balance = balance;
        }
        // deposit method that first checks if amount is less or equal to 0, if check passes, adds amounts
        public bool deposit(decimal amount) {
            if (amount <= 0) {
                return false; 
            }
            Balance += amount;
            return true;
        }
        // withdraw method that first runs a check if amount is less or equal to 0 OR if balance is less than amount, if the check passes, removes the amount
        public bool withdraw(decimal amount) {
            if (amount <= 0 || Balance < amount) {
                return false;
            }
            Balance -= amount;
            return true;
        }
        // class that works as a helper for creating accounts and holds the names in an array
        public static class AccountFactory {
            private static readonly string[] _accountNames = {
            "Lönekonto",
            "Sparkonto",
            "Semesterkonto",
            "Investeringskonto",
            "Buffertkonto",
            "Räkningskonto"
            };
            // method that generates the list of accounts with random amounts of accounts and balance.
            public static List<Account> generateAccounts() {
                Random random = new Random();
                int numberOfAccounts = random.Next(1, 7);
                List<Account> accounts = new List<Account>();

                for (int i = 0; i < numberOfAccounts; i++) {
                    string name = _accountNames[i];
                    decimal balance = random.Next(100, 10000); // Random balance between 100 and 10 000
                    accounts.Add(new Account(name, balance));
                }
                return accounts;
            }
        }
    }
}