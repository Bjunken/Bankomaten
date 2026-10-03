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
        //Only allow getting the balance from outside, not setting it directly. Only account methods should be able to change the balance.
        public decimal Balance { get; private set; } 


        public Account(string accountName, decimal balance)
        {
            AccountName = accountName;
            Balance = balance;
        }
              
    }
}
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