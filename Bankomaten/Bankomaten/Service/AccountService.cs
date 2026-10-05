using Bankomaten.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bankomaten.Service {
    internal class AccountService 
    {
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
