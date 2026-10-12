using Bankomaten.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Bankomaten.Utils;

namespace Bankomaten.Service
{
    internal class BankService
    {
        private AccountService _accountService;
        private ValidationHelper _validationHelper;
        private List<User> _users;
        private User? _currentUser;

        public BankService(List<User> Users, ValidationHelper validationHelper, AccountService accountService)
        {
            _users = Users;
            _validationHelper = validationHelper;
            _accountService = accountService;
        }
        public User? LocateUser(string userName)
        {
            foreach (User user in _users)
            {
                bool areEqual = string.Equals(user.UserName, userName, StringComparison.OrdinalIgnoreCase);
                if (areEqual)
                {
                    return user;
                }
            }
            return null;
        }
        public LoginResult Login(string userName, string pinText)
        {
            User? user = LocateUser(userName);
            if (!IsLoggedIn())
            {
                return LoginResult.NoUser;
            }
            if (!int.TryParse(pinText, out int pinCode))
            {
                return LoginResult.InvalidPinFormat;
            }
            if (user.IsLocked)
            {
                return LoginResult.UserLocked;
            }
            if (!_validationHelper.VerifyPin(user, pinCode))
            {
                return LoginResult.WrongPin;
            }
            _currentUser = user;
            return LoginResult.SuccessfulLogin;
        }


        public bool IsLoggedIn()
        {
            return _currentUser != null;
        }


        public void Logout()
        {
            _currentUser = null;
        }


        public TransactionResult Deposit(decimal amount, int accountIndex)
        {
            if (!IsLoggedIn())
            {
                return TransactionResult.NonLoggedIn;
            }
            return _accountService.Deposit(_currentUser, accountIndex, amount);
        }


        public TransactionResult Withdraw(decimal amount, int accountIndex, string pinText)
        {
            if (!IsLoggedIn())
            {
                return TransactionResult.NonLoggedIn;
            }
            if (!int.TryParse(pinText, out int pinCode))
            {
                return TransactionResult.InvalidPinFormat;
            }

            if (_currentUser.IsLocked)
            {
                return TransactionResult.UserLocked;
            }

            if (!_validationHelper.VerifyPin(_currentUser, pinCode))
            {
                return TransactionResult.WrongPin;
            }
            return _accountService.Withdraw(_currentUser, accountIndex, amount);
        }


        public TransactionResult Transfer(int fromindex, int toindex, decimal amount)
        {
            if (!IsLoggedIn())
            {
                return TransactionResult.NonLoggedIn;
            }

            return _accountService.Transfer(_currentUser, fromindex, toindex, amount);
        }


        public TransactionResult GetBalance(int accountIndex, out decimal balance)
        {
            if (!IsLoggedIn())
            {
                balance = 0;
                return TransactionResult.NonLoggedIn;
            }
            return _accountService.GetBalance(_currentUser, accountIndex, out balance);
        }
    }
}
