using Bankomaten.Models;
using Bankomaten.Utils;
using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Text;

namespace Bankomaten.Service
{
    internal class AccountService
    {
        public bool TryGetAccount(User user, int accountIndex, out Account? account)
        {
            if (accountIndex < 0 || accountIndex >= user.Accounts.Count)
            {
                account = null;
                return false;
            }
            account = user.Accounts[accountIndex];
            return true;
        }

        public bool IsValidAmount(decimal amount)
        { 
            return amount > 0;
        }


        // Call upon the method from account to deposit the ammount and return for BankServices
        public TransactionResult Deposit(User user, int accountIndex, decimal amount)
        {
            if (!TryGetAccount(user, accountIndex, out Account? account))
            {
                return TransactionResult.InvalidAccountIndex;
            }

            if (!IsValidAmount(amount))
            {
                return TransactionResult.InvalidAmount;
            }
            account!.Deposit(amount);
            return TransactionResult.SuccessfulTransaction;
        }


        // Call upon the method from account to withdraw the ammount and return for BankServices
        public TransactionResult Withdraw(User user, int accountIndex, decimal amount)
        {

            if (!TryGetAccount(user, accountIndex, out Account? account))
            {
                return TransactionResult.InvalidAccountIndex;
            }

            if (!IsValidAmount(amount))
            {
                return TransactionResult.InvalidAmount;
            }

            if (account!.Balance < amount)
            {
                return TransactionResult.InsufficientFunds;
            }

            account.Withdraw(amount);
            return TransactionResult.SuccessfulTransaction;
        }



        public TransactionResult Transfer(User user, int fromIndex, int toIndex, decimal amount)
        {
            // Checks that the user does not withdraw and deposits to the same account
            if (!TryGetAccount(user, fromIndex, out Account? fromAccount))
            {
                return TransactionResult.InvalidAccountIndex;
            }

            if (!TryGetAccount(user, toIndex, out Account? toAccount))
            {
                return TransactionResult.InvalidAccountIndex;
            }

            if (fromIndex == toIndex)
            {
                return TransactionResult.SameAccount;
            }

            if (!IsValidAmount(amount))
            {
                return TransactionResult.InvalidAmount;
            }

            if (fromAccount!.Balance < amount)
            {
                return TransactionResult.InsufficientFunds;
            }

            fromAccount!.Withdraw(amount);
            toAccount!.Deposit(amount);
            return TransactionResult.SuccessfulTransaction;
        }


        public TransactionResult GetBalance(User user, int accountIndex, out decimal balance)
        {
            if (!TryGetAccount(user, accountIndex, out Account? account))
            {
                balance = 0;
                return TransactionResult.InvalidAccountIndex;
            }
            balance = account!.Balance;
            return TransactionResult.SuccessfulTransaction;
        }
    }
}
