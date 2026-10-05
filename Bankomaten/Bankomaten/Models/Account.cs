using System;
using System.Collections.Generic;
using System.Text;

namespace Bankomaten.Models  {
    /*
        Reminder: This class defines the Account model used by Service,
        keep account data here and make sure balance changes can be controlled from here.
    */
    public class Account  
    {
        public string AccountName { get; }
        //Only allow getting the balance from outside, not setting it directly. Only account methods should be able to change the balance.
        public decimal Balance { get; private set; }
        public Account(string accountName, decimal balance)
        {
            AccountName = accountName;
            Balance = balance;
        }
        public bool Deposit(decimal amount)
        {
            if (amount <= 0)
            {
                return false; 
            }
            Balance += amount;
            return true;
        }
        public bool Withdraw(decimal amount)
        {
            if (amount <= 0 || Balance < amount)
            {
                return false;
            }
            Balance -= amount;
            return true;
        }

        public static class AccountFactory
        {
            private static readonly string[] AccountNames =
            {
            "Salaryaccount",
            "Savingsaccount",
            "Vacationaccount",
            "InvestmentAccount",
            "EmergencyFund",
            "BillsAccount"            
            };

            public static List<Account> GenerateAccounts()
            {
                Random random = new Random();
                int numberOfAccounts = random.Next(1, 7);
                List<Account> accounts = new List<Account>();

                for (int i = 0; i < numberOfAccounts; i++)
                {
                    string name = AccountNames[i];
                    decimal balance = random.Next(100, 10000); // Random balance between 100 and 10 000
                    accounts.Add(new Account(name, balance));
                }
                return accounts;
            }
        }

    }
}
