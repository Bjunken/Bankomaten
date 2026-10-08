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
        private AccountService accountService;
        private ValidationHelper validationHelper;
        private List<User> users;
        private User? currentUser;

        public BankService(List<User> Users) // <- Add parameter for ValidationHelper and Accountservice and set them in the constructor
        {
            users = Users;
            validationHelper = new ValidationHelper(); // <- this should be a setter for paramters so field = parameter, not new.
        }

        // Add a for loop that loops through "User user in _users" then check if parameter name is in users.
        public User? LocateUser() //add into parameter later string username (UI will handle all readlines later on)
        {
            string username = Console.ReadLine();
            currentUser = users.Find(u => u.UserName == username);
            return currentUser; //If it returns null then the 'Login' method will handle it
        } // make sure you return null if there isn't a match.


        public LoginResult Login(string pinText) // add parameter for the username too, then run locateUser()
        {
            if (currentUser == null) // <- should be set last if there is a user
            {
                return LoginResult.NoUser;
            }

            if (currentUser.IsLocked) 
            {
                return LoginResult.UserLocked;
            }

            if (!int.TryParse(pinText, out int pinCode)) // <- make sure you check this before you check if account is locked.
            {
                return LoginResult.InvalidPinFormat;
            }

            if (!validationHelper.VerifyPin(currentUser, pinCode))
            {
                return LoginResult.WrongPin;
            }
            // Need to set current user here.
            return LoginResult.SuccessfulLogin;     
        }
        // make another method bool method here called "IsLoggedIn() that just returns currentuser != null.

        public void Logout()
        {
            currentUser = null;
        }
        //Todo: Remove all readlines as those will be handled in the ui and instead utilise parameters. use pin logic for transfers.
        // Write a deposit method that checks if not logged in if so return nonloggedin, else return call accountservice.deposit with parameters.

        // Write a withdraw method with 3 parameters, it needs to first check if logged in, then check if pin can be parsed, then check if account is locked,
        // then verify pin, then return success.

        // write a Transfer method, 3 parameters, (to and from index) + amount. It needs to check if logged in, else return accountService.Transfer with parameters.

        // write a GetBalance method, two parameters with accountIndex and out decimal balance, it needs to check if not logged in and if not set balance to 0 before returning
        // else return calling accountService.Getbalance with parameters.
    }
}
