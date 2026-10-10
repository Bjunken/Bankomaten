using Bankomaten.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Bankomaten.Utils;

namespace Bankomaten.Service {
    internal class BankService {
        private AccountService _accountService;
        private ValidationHelper _validationHelper;
        private List<User> _users;
        private User? _currentUser;

        public BankService(List<User> Users, ValidationHelper validationHelper, AccountService accountService) {
            _users = Users;
            _validationHelper = validationHelper; 
            _accountService = accountService;
        }
        public User? LocateUser(string userName) {       
            foreach(User user in _users) {
                bool areEqual = string.Equals(user.UserName, userName, StringComparison.OrdinalIgnoreCase);
                if (areEqual) {
                    return user;
                }
            }
            return null;
        }
        public LoginResult Login(string userName, string pinText) {
            User? user = LocateUser(userName);
            if (user == null) { // <- You already have a method to check this, (IsLoggedIn), so you can just do (!IsLoggedIn()) and it will do the same.
                return LoginResult.NoUser;
            }
            if (!int.TryParse(pinText, out int pinCode)) {
                return LoginResult.InvalidPinFormat;
            }
            if (user.IsLocked) {
                return LoginResult.UserLocked;
            }
            if (!_validationHelper.VerifyPin(user, pinCode)) {
                return LoginResult.WrongPin;
            }
            _currentUser = user;
            return LoginResult.SuccessfulLogin;     
        }
        public bool IsLoggedIn() {
            return _currentUser != null;
        }
        public void Logout() {
            _currentUser = null;
        }
        public TransactionResult Deposit(decimal amount, int accountIndex) {
            if (_currentUser == null) { // <- You already have a method to check this, (IsLoggedIn), so you can just do (!IsLoggedIn()) and it will do the same.
                return TransactionResult.NonLoggedIn;
            }
            return _accountService.Deposit(_currentUser, accountIndex, amount);
        }
        public TransactionResult Withdraw(decimal amount, int accountIndex, string pinText) {
            if (_currentUser == null) { // <- You already have a method to check this, (IsLoggedIn), so you can just do (!IsLoggedIn()) and it will do the same.
                return TransactionResult.NonLoggedIn;
            }
            if (!int.TryParse(pinText, out int pinCode)) {
                return TransactionResult.InvalidPinFormat;
            }
            /*
            You need to add a check here if _currentUser is locked.
            */
            
            if (!_validationHelper.VerifyPin(_currentUser, pinCode)) {
                return TransactionResult.WrongPin;
            }
            return _accountService.Withdraw(_currentUser, accountIndex, amount);
        }
        public TransactionResult GetBalance(int accountIndex, out decimal balance) {
            if(_currentUser == null) { // <- You already have a method to check this, (IsLoggedIn), so you can just do (!IsLoggedIn()) and it will do the same.
                balance = 0;
                return TransactionResult.NonLoggedIn;
            }
            return _accountService.GetBalance(_currentUser, accountIndex, out balance);
        }
        /*
        TODO:
        The Withdraw() Method is missing a 'account is locked' check.
        write a Transfer() method, 3 parameters, (to and from index) + amount. It needs to check if logged in, else return accountService.Transfer with parameters.
        */ 
    }
}
