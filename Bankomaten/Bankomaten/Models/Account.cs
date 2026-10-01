using System;
using System.Collections.Generic;
using System.Text;

namespace Bankomaten.Models  {
    /*
        Reminder: This class defines the Account model used by Service,
        keep account data here and make sure balance changes can be controlled from here.
    */
    public class Account  {
        public string AccountName { get; set; }
        public decimal Balance { get; set; }

        /*
            public decimal Balance { get; set; }
            ^ This should change from public, as Balance should not be editable from outside of this class.
            So you can replace the setter with a control method,
            (think that deposit, withdrawls etc, need to go call that setter.)
        */

        /*
            In Account(), remember that it must support different starting balances
            and that balances should use decimal.
        */

        public Account(string accountName, decimal balance)
        {
            AccountName = accountName;
            Balance = balance;
        }

        /*
            ShowAccounts() Should be in AccountService.cs
            Think of it like this: Account.cs should represent account data,
            while AccountService handles operations and managing the data.

            So you can leave this as is and we'll tell Martin to move it or remove it.
        */
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
