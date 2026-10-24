using Bankomaten.Models;
using System;
using System.Collections.Generic;
using Bankomaten.Utils;

namespace Bankomaten.Service {
    internal class BankService {
        private AccountService _accountService;
        private ValidationHelper _validationHelper;
        private List<User> _users;
        private User? _currentUser;
        // the menu, (UI), reads the signed-in user's accounts through the bank service
        public User? CurrentUser => _currentUser;
        public BankService(List<User> Users, ValidationHelper validationHelper, AccountService accountService) {
            _users = Users;
            _validationHelper = validationHelper;
            _accountService = accountService;
        }
        public User? locateUser(string userName) {
            foreach (User user in _users) {
                // Compare two strings without taking into account if they are upper or lowercase to deal with case sensitivity.
                bool areEqual = string.Equals(user.UserName, userName, StringComparison.OrdinalIgnoreCase);
                if (areEqual) {
                    return user;
                }
            }
            return null;
        }
        public LoginResult login(string userName, string pinText) {
            User? user = locateUser(userName);
            if (user == null) {
                return LoginResult.NoUser;
            }
            if (!int.TryParse(pinText, out int pinCode)) {
                return LoginResult.InvalidPinFormat;
            }
            if (user!.IsLocked) {
                return LoginResult.UserLocked;
            }
            if (!_validationHelper.verifyPin(user, pinCode)) {
                // the third incorrect attempt locks the user and returns a distinct result
                return user.IsLocked ? LoginResult.UserLocked : LoginResult.WrongPin;
            }
            _currentUser = user;
            return LoginResult.SuccessfulLogin;
        }
        public bool isLoggedIn() {
            return _currentUser != null;
        }
        public void logout() {
            _currentUser = null;
        }
        // will pass _currentUser!, accountIndex and amount to AccountService.cs and then return the result from AccountService.cs
        public TransactionResult deposit(decimal amount, int accountIndex) {
            if (!isLoggedIn()) {
                return TransactionResult.NonLoggedIn;
            }
            return _accountService.deposit(_currentUser!, accountIndex, amount);
        }
        // runs a if-check and if passing, calls _accountService.withdraw()
        public TransactionResult withdraw(decimal amount, int accountIndex, string pinText) {
            if (!isLoggedIn()) {
                return TransactionResult.NonLoggedIn;
            }
            if (!int.TryParse(pinText, out int pinCode)) {
                return TransactionResult.InvalidPinFormat;
            }
            if (_currentUser!.IsLocked) {
                return TransactionResult.UserLocked;
            } 
            if (!_validationHelper.verifyPin(_currentUser, pinCode)) {
                return _currentUser.IsLocked ? TransactionResult.UserLocked : TransactionResult.WrongPin;
            }
            // if PIN valid, calls _accountService.withdraw() and checks the amount and updates the balance.
            return _accountService.withdraw(_currentUser, accountIndex, amount);
        }
        // Will pass _currentUser!, fromIndex, toIndex and amount to AccountService.cs and then return the result from AccountService.cs
        public TransactionResult transfer(int fromIndex, int toIndex, decimal amount) {
            if (!isLoggedIn()) {
                return TransactionResult.NonLoggedIn;
            }
            return _accountService.transfer(_currentUser!, fromIndex, toIndex, amount);
        }
    }
}