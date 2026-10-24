using Bankomaten.Models;
using Bankomaten.Utils;
using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Text;

namespace Bankomaten.Service {
    internal class AccountService {
        public bool tryGetAccount(User user, int accountIndex, out Account? account) {
            if (accountIndex < 0 || accountIndex >= user.Accounts.Count) {
                account = null;
                return false;
            }
            account = user.Accounts[accountIndex];
            return true;
        }
        public bool isValidAmount(decimal amount) {
            return amount > 0;
        }
        // call the deposit method from Account.cs and passes the amount, then returns to BankServices
        public TransactionResult deposit(User user, int accountIndex, decimal amount) {
            if (!tryGetAccount(user, accountIndex, out Account? account)) {
                return TransactionResult.InvalidAccountIndex;
            }
            if (!isValidAmount(amount)) {
                return TransactionResult.InvalidAmount;
            }
            account!.deposit(amount);
            return TransactionResult.SuccessfulTransaction;
        }
        // call the withdraw method from Account.cs and passes the amount, then returns to BankServices
        public TransactionResult withdraw(User user, int accountIndex, decimal amount) {
            if (!tryGetAccount(user, accountIndex, out Account? account)) {
                return TransactionResult.InvalidAccountIndex;
            }
            if (!isValidAmount(amount)) {
                return TransactionResult.InvalidAmount;
            }
            if (account!.Balance < amount) {
                return TransactionResult.InsufficientFunds;
            }
            account.withdraw(amount);
            return TransactionResult.SuccessfulTransaction;
        }
        // Call the transfer method from Account.cs to withdraw and deposit the amount, returns to BankServices
        public TransactionResult transfer(User user, int fromIndex, int toIndex, decimal amount) {
            if (!tryGetAccount(user, fromIndex, out Account? fromAccount)) {
                return TransactionResult.InvalidAccountIndex;
            }
            if (!tryGetAccount(user, toIndex, out Account? toAccount)) {
                return TransactionResult.InvalidAccountIndex;
            }
            // Checks that the user does not withdraw and deposits to the same account
            if (fromIndex == toIndex) { 
                return TransactionResult.SameAccount;
            } 
            if (!isValidAmount(amount)) {
                return TransactionResult.InvalidAmount;
            } 
            if (fromAccount!.Balance < amount) {
                return TransactionResult.InsufficientFunds;
            }
            fromAccount!.withdraw(amount);
            toAccount!.deposit(amount);
            return TransactionResult.SuccessfulTransaction;
        }
    }
}