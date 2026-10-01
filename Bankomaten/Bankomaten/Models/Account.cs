using System;
using System.Collections.Generic;
using System.Text;

namespace Bankomaten.Models 
{
    public class Account 
    {
        public string AccountName { get; set; }
        public decimal Balance { get; set; }

        public Account(string accountName, decimal balance)
        {
            AccountName = accountName;
            Balance = balance;
        }
        public static void ShowAccounts(User user)
        {
            Console.WriteLine();
            Console.WriteLine($"{user.UserName}s accounts:");

            foreach (Account account in user.Accounts)
            {
                Console.WriteLine(
                $"{account.AccountName}: {account.Balance:C}");
            }
        }
    }
}
