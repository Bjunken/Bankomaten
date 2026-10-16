using Bankomaten.Models;
using System;
using System.Collections.Generic;
using Bankomaten.Utils;

namespace Bankomaten.Service
{
    internal class BankService
    {
        private AccountService _accountService;
        private ValidationHelper _validationHelper;
        private List<User> _users;
        private User? _currentUser;
        public User? CurrentUser => _currentUser;

        public BankService(List<User> Users, ValidationHelper validationHelper, AccountService accountService)
        {
            _users = Users;
            _validationHelper = validationHelper;
            _accountService = accountService;
        }

        // The menu reads the signed-in user's accounts through the bank service.
        public User? CurrentUser => _currentUser;


        public User? LocateUser(string userName)
        {
            foreach (User user in _users)
            {
                bool areEqual = string.Equals(user.UserName, userName, StringComparison.OrdinalIgnoreCase); // Compare two strings without taking into account if they are upper or lowercase to deal with case sensitivity.
                if (areEqual)
                {
                    return user;
                }
            }
            return null;
        }


        // Find and verify the user here so the login menu only handles input and display.
        public LoginResult Login(string userName, string pinText)
        {
            User? user = LocateUser(userName);
            if (user == null)
            {
                return LoginResult.NoUser;
            }
            if (!int.TryParse(pinText, out int pinCode))
            {
                return LoginResult.InvalidPinFormat;
            }
            if (user!.IsLocked)
            {
                return LoginResult.UserLocked;
            }
            if (!_validationHelper.VerifyPin(user, pinCode))
            {
                // The third incorrect attempt locks the user and returns a distinct result.
                return user.IsLocked ? LoginResult.UserLocked : LoginResult.WrongPin;
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


        // Will pass _currentUser!, accountIndex and amount to AccountService.cs and then return the result from AccountService.cs
        public TransactionResult Deposit(decimal amount, int accountIndex)
        {
            if (!IsLoggedIn())
            {
                return TransactionResult.NonLoggedIn;
            }
            return _accountService.Deposit(_currentUser!, accountIndex, amount);
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

            if (_currentUser!.IsLocked)
            {
                return TransactionResult.UserLocked;
            }

            if (!_validationHelper.VerifyPin(_currentUser, pinCode))
            {
                // A withdrawal PIN counts as an attempt; the menu logs out a locked user.
                return _currentUser.IsLocked ? TransactionResult.UserLocked : TransactionResult.WrongPin;
            }
<<<<<<< HEAD
            // The PIN is valid; AccountService checks the amount and updates the balance.
            return _accountService.Withdraw(_currentUser, accountIndex, amount);
=======
            return _accountService.Withdraw(_currentUser!, accountIndex, amount);
>>>>>>> a3e59978d3dc86e706fc7dfa3a6265f950508f0e
        }


        // Will pass _currentUser!, fromIndex, toIndex and amount to AccountService.cs and then return the result from AccountService.cs
        public TransactionResult Transfer(int fromIndex, int toIndex, decimal amount)
        {
            if (!IsLoggedIn())
            {
                return TransactionResult.NonLoggedIn;
            }

            return _accountService.Transfer(_currentUser!, fromIndex, toIndex, amount);
        }


        public TransactionResult GetBalance(int accountIndex, out decimal balance)
        {
            if (!IsLoggedIn())
            {
                balance = 0;
                return TransactionResult.NonLoggedIn;
            }
            return _accountService.GetBalance(_currentUser!, accountIndex, out balance);
        }
    }
}
