using Bankomaten.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Bankomaten.Utils;

namespace Bankomaten.Service {
    /*
        Reminder: This class handles the account features such as showing accounts and displaying their balance. 
        Transfering money between accounts and withdrawing money.
    */
    internal class AccountService 
    {
        // check if account index is less than 0 OR account index is higher or equal to user.accounts.count, if true set account to null and return false.
        // otherwise just set account to user.accounts[accountIndex] and return true
        public bool TryGetAccount(User user, int accountIndex, out Account account)
        {
            if (accountIndex < 0 || accountIndex >= user.Accounts.Count)
            {
                account = null;
                return false;
            }
            account = user.Accounts[accountIndex];
            return true;
        }


        //Will be removed after Transfer is finished
        public static Account AccountSelection(User user)
        {
            while (true)
            {
                string selection = Console.ReadLine();
                Account selectedAccount = user.Accounts.Find(account => account.AccountName == selection);
                if (selectedAccount == null)
                {
                    Console.WriteLine("Unknown account");
                    continue;
                }
                else
                {
                    return selectedAccount;
                }
            }
        }


        // write a short bool method for "IsValidAmount" just take parameter decimal and check if the input is higher than 0
        public bool IsValidAmount(decimal input)
        {
            if (input > 0)
            {
                return true;
            }
            return false;
        }

        public TransactionResult TransferMoney(User user, int fromindex, int toindex, decimal amount) // should have parameters for user, fromindex, to index and deimal amount. So you can make similar checks as you did in BankService.
        {
            Console.WriteLine("Please enter the account you would like to transfer money from: "); //Remove when done in UI
            Account accountWithdraw = AccountSelection(user);

            Console.WriteLine("Please enter the account you would like to transfer money to: "); //Remove when done in UI
            Account accountDeposit = AccountSelection(user);

            //If the user wrote same account when AccountSelection is called let them know they cant transfer to same account
            if (accountWithdraw == accountDeposit)
            {
                return TransactionResult.SameAccount;
            }

            Console.WriteLine("Please enter how much money you would like to transfer: "); 

            if (!decimal.TryParse(Console.ReadLine(), out decimal transferAmount))
            {
                return TransactionResult.InvalidAmount;
            }

            if (!IsValidAmount(amount))
            {
                return TransactionResult.InvalidAmount;
            }

            if (transferAmount > accountWithdraw.Balance)
            {
                return TransactionResult.InsufficientFunds;
            }

            accountWithdraw.Withdraw(transferAmount);
            accountDeposit.Deposit(transferAmount);
            return TransactionResult.SuccessfulTransaction;
        }


        //Call upon the method from account to withdraw the ammount and return for BankServices
        public TransactionResult Withdraw(User user, int  accountIndex, decimal amount)
        {
            if (!TryGetAccount(user, accountIndex, out Account account))
            {
                return TransactionResult.InvalidAccountIndex;
            }

            if(!IsValidAmount(amount))
            {
                return TransactionResult.InvalidAmount;
            }
            account.Withdraw(amount);
            return TransactionResult.SuccessfulTransaction;
        }


        //Call upon the method from account to deposit the ammount and return for BankServices
        public TransactionResult Deposit(User user, int accountIndex, decimal amount)
        {
            if (!TryGetAccount(user, accountIndex, out Account account))
            {
                return TransactionResult.InvalidAccountIndex;
            }

            if (!IsValidAmount(amount))
            {
                return TransactionResult.InvalidAmount;
            }
            account.Deposit(amount);
            return TransactionResult.SuccessfulTransaction;
        }


        public TransactionResult GetBalance(User user, int accountIndex,out decimal balance)
        {
            if(!TryGetAccount(user, accountIndex, out Account account))
            {
                balance = 0;
                return TransactionResult.InvalidAccountIndex;
            }

            balance = account.Balance;
            return TransactionResult.SuccessfulTransaction;
        }
    }
}
