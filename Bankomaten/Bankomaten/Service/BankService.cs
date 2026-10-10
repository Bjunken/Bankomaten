using Bankomaten.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Bankomaten.Utils;

namespace Bankomaten.Service {
    /*
        Reminder: This class locates current user and checks if the user is locked. It also handles pin verification both when trying to
        login and when the user is about to make a transfer between different accounts.
    */
    internal class BankService {
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

        // Add a for loop that loops through "User user in _users" then check if parameter name is in users.
        public User? LocateUser(string userName) //add into parameter later string username (UI will handle all readlines later on)
        {       
            foreach(User user in _users)
            {
                bool areEqual = string.Equals(user.UserName, userName, StringComparison.OrdinalIgnoreCase);
                if (areEqual)
                {
                    return user;
                }
            }
            return null;
        } // make sure you return null if there isn't a match.


        public LoginResult Login(string userName, string pinText) // add parameter for the username too, then run locateUser()
        {
            User? user = LocateUser(userName);
            
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

            if (user == null)// <- should be set last if there is a user
            {
                return LoginResult.NoUser;
            }
            // Need to set current user here.
            _currentUser = user;
            return LoginResult.SuccessfulLogin;     
        }

        // make another method bool method here called "IsLoggedIn() that just returns currentuser != null.
        public bool IsLoggedIn()
        {
            return _currentUser != null;
        }

        public void Logout()
        {
            _currentUser = null;
        }

        // Write a deposit method that checks if not logged in if so return nonloggedin, else return call accountservice.deposit with parameters.
        public TransactionResult Deposit(decimal amount, int accountIndex)
        {
            if (_currentUser == null)
            {
                return TransactionResult.NonLoggedIn;
            }
            return _accountService.Deposit(_currentUser, accountIndex, amount);
        }


        public TransactionResult Withdraw(decimal amount, int accountIndex, string pinText)
        {
            if (_currentUser == null)
            {
                return TransactionResult.NonLoggedIn;
            }

            if (!int.TryParse(pinText, out int pinCode))
            {
                return TransactionResult.InvalidPinFormat;
            }

            if (!_validationHelper.VerifyPin(_currentUser, pinCode))
            {
                return TransactionResult.WrongPin;
            }
            return _accountService.Withdraw(_currentUser, accountIndex, amount);
        }


        public TransactionResult GetBalance(int accountIndex, out decimal balance)
        {
            if(_currentUser == null)
            {
                balance = 0;
                return TransactionResult.NonLoggedIn;
            }
            return _accountService.GetBalance(_currentUser, accountIndex, out balance);
        }
        //Todo: Remove all readlines as those will be handled in the ui and instead utilise parameters. use pin logic for transfers.


        // Write a withdraw method with 3 parameters, it needs to first check if logged in, then check if pin can be parsed, then check if account is locked,
        // then verify pin, then return success.

        // write a Transfer method, 3 parameters, (to and from index) + amount. It needs to check if logged in, else return accountService.Transfer with parameters.

        // write a GetBalance method, two parameters with accountIndex and out decimal balance, it needs to check if not logged in and if not set balance to 0 before returning
        // else return calling accountService.Getbalance with parameters.
    }
}
