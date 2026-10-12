using Bankomaten.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Bankomaten.Utils;

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
            if (amount > 0)
            { 
                return true;
            }
            return false;
        }


        // Call upon the method from account to deposit the ammount and return for BankServices
        public TransactionResult Deposit(User user, int accountIndex, decimal amount)
        {
            if (!TryGetAccount(user, accountIndex, out Account? accountDeposit))
            {
                return TransactionResult.InvalidAccountIndex;
            }

            if (!IsValidAmount(amount))
            {
                return TransactionResult.InvalidAmount;
            }
            accountDeposit.Deposit(amount);
            return TransactionResult.SuccessfulTransaction;
        }


        // Call upon the method from account to withdraw the ammount and return for BankServices
        public TransactionResult Withdraw(User user, int accountIndex, decimal amount)
        {

            if (!TryGetAccount(user, accountIndex, out Account? accountWithdraw))
            {
                return TransactionResult.InvalidAccountIndex;
            }

            if (!IsValidAmount(amount))
            {
                return TransactionResult.InvalidAmount;
            }

            if (!IsValidAmount(amount))
            {
                return TransactionResult.InvalidAmount;
            }

            //will return InsufficientFunds if Withdraw is returned false from Account.cs
            if (!accountWithdraw.Withdraw(amount))
            {
                return TransactionResult.InsufficientFunds;
            }

            return TransactionResult.SuccessfulTransaction;
        }



        public TransactionResult Transfer(User user, int fromIndex, int toIndex, decimal amount)
        {
            // Checks that the user does not withdraw and deposits to the same account
            if (fromIndex == toIndex)
            {
                return TransactionResult.SameAccount;
            }

            if (!IsValidAmount(amount))
            {
                return TransactionResult.InvalidAmount;
            }

            if (!TryGetAccount(user, fromIndex, out Account? accountWithdraw))
            {
                return TransactionResult.InvalidAccountIndex;
            }

            if (!TryGetAccount(user, toIndex, out Account? accountDeposit))
            {
                return TransactionResult.InvalidAccountIndex;
            }

            if (!accountWithdraw.Withdraw(amount))
            {
                return TransactionResult.InsufficientFunds;
            }

            accountDeposit.Deposit(amount);
            return TransactionResult.SuccessfulTransaction;
        }


        public TransactionResult GetBalance(User user, int accountIndex, out decimal balance)
        {
            if (!TryGetAccount(user, accountIndex, out Account? account))
            {
                balance = 0;
                return TransactionResult.InvalidAccountIndex;
            }
            balance = account.Balance;
            return TransactionResult.SuccessfulTransaction;
        }
    }
}
