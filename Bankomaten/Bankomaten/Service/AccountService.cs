using Bankomaten.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Bankomaten.Utils;

namespace Bankomaten.Service {
    internal class AccountService {
        public bool TryGetAccount(User user, int accountIndex, out Account account) { // <- add a '?' to the out Account datatype, since it can be null.
            if (accountIndex < 0 || accountIndex >= user.Accounts.Count) {
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
        public bool IsValidAmount(decimal input) { // <-- change paramter name to 'amount' instead to keep with the naming scheme.
            if (input > 0) { // <- you will get the same effect with just "return input > 0;" as it will only give true or false.
                return true;
            }
            return false;
        }
        public TransactionResult TransferMoney(User user, int fromindex, int toindex, decimal amount) // <-- can change name to just 'transfer' to keep within the naming scheme, same with the paramters, use camelcasing, (fromIndex, toIndex).
        {
            Console.WriteLine("Please enter the account you would like to transfer money from: "); //Remove when done in UI
            Account accountWithdraw = AccountSelection(user);

            Console.WriteLine("Please enter the account you would like to transfer money to: "); //Remove when done in UI
            Account accountDeposit = AccountSelection(user);
            // this should just be an if check, (!TryGetAccount) twice, one with from Index and one for toIndex.
            
            //If the user wrote same account when AccountSelection is called let them know they cant transfer to same account
            if (accountWithdraw == accountDeposit) { // <- you can just compare the input Index's.
                return TransactionResult.SameAccount;
            }
            if (!IsValidAmount(amount)) {
                return TransactionResult.InvalidAmount;
            }
            Console.WriteLine("Please enter how much money you would like to transfer: "); // <- remove

            if (!decimal.TryParse(Console.ReadLine(), out decimal transferAmount))
            {
                return TransactionResult.InvalidAmount;
            }
            // Remove the whole tryparse here ^.
            if (transferAmount > accountWithdraw.Balance) { // (fromAccount!.AccountBalance < amount) (since you're removing the readlines)
                return TransactionResult.InsufficientFunds;
            }
            accountWithdraw.Withdraw(transferAmount); // <- fromAccount.Withdraw(amount)
            accountDeposit.Deposit(transferAmount); // <- toAccount!.Deposit(amount) since you're removing the readlines.
            return TransactionResult.SuccessfulTransaction;
        }
        public TransactionResult Withdraw(User user, int  accountIndex, decimal amount) {
            if (!TryGetAccount(user, accountIndex, out Account account)) { // <- add a '?' to Account datatype since it can be null.
                return TransactionResult.InvalidAccountIndex;
            }
            if(!IsValidAmount(amount)) {
                return TransactionResult.InvalidAmount;
            }
            /*
            Need to add a balance check here.
            */
            account.Withdraw(amount);
            return TransactionResult.SuccessfulTransaction;
        }
        public TransactionResult Deposit(User user, int accountIndex, decimal amount) {
            if (!TryGetAccount(user, accountIndex, out Account account)) {// <- add a '?' to Account datatype since it can be null.
                return TransactionResult.InvalidAccountIndex;
            }
            if (!IsValidAmount(amount)) {
                return TransactionResult.InvalidAmount;
            }
            account.Deposit(amount);
            return TransactionResult.SuccessfulTransaction;
        }
        public TransactionResult GetBalance(User user, int accountIndex, out decimal balance) {
            if(!TryGetAccount(user, accountIndex, out Account account)) { // <- add a '?' to Account datatype since it can be null.
                balance = 0;
                return TransactionResult.InvalidAccountIndex;
            }
            balance = account.Balance;
            return TransactionResult.SuccessfulTransaction;
        }
    }
}
