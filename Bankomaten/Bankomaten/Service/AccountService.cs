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
        // Can remove ShowAccounts completely.

        // This method can be redone and name change to fit the structure a bit better: TryGetAccount, with parameter for user, account index and out bankaccount account.
        // check if account index is less than 0 OR account index is higher or equal to user.accounts.count, if true set account to null and return false.
        // otherwise just set account to user.accounts[accountIndex] and return true
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


        public TransactionResult TransferMoney(User user) // should have parameters for user, fromindex, to index and deimal amount. So you can make similar checks as you did in BankService.
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

            if (transferAmount <= 0)
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


            //Todo: Add withdraw <- should have parameter for user, accountIndex and decimal amount for checks.
            // write deposit(), should have same paramaters as Withdraw.
            // write a GetBalance() should have same parameters as deposit and withdraw.
        }
    }
}
