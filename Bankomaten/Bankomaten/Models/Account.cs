using System;
using System.Collections.Generic;
using System.Text;

namespace Bankomaten.Models  {
    /*
        Reminder: This class defines the Account model used by Service,
        keep account data here and make sure balance changes can be controlled from here.
    */
    public class Account  {
        public string AccountName { get; set; } // This only needs a "get;", since 'set' is done in the constructor.
        public decimal Balance { get; private set; }

        public Account(string accountName, decimal balance)
        {
            AccountName = accountName;
            Balance = balance;
        }
         // For this class to be "done", you need to make a method for "deposit" and for "withdraw", both should only return a bool and change the balance, no prints.     
    }
    // You also need to make another class inside here. The class will be used to create the actual bank accounts, set the number of bank accounts to a random (1, 7).
    // same with set a random balance (100, 10000). The hint for this class is the one i sent on Discord. (I recommend swapping names of the classes to keep the names more organised).
    // A tip here too, when you set the names for the different bank accounts, declare an array with the names, (this fullfills our requirement for 2 data structures).
}
