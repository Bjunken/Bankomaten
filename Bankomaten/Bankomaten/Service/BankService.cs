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

        public BankService(List<User> Users)
        {
            users = Users;
            validationHelper = new ValidationHelper();
        }


        public User? LocateUser() //add into parameter later string username (UI will handle all readlines later on)
        {
            string username = Console.ReadLine();
            currentUser = users.Find(u => u.UserName == username);
            return currentUser; //If it returns null then the 'Login' method will handle it
        }


        public LoginResult Login(string pinText)
        {
            if (currentUser == null)
            {
                return LoginResult.NoUser;
            }

            if (currentUser.IsLocked) 
            {
                return LoginResult.UserLocked;
            }

            if (!int.TryParse(pinText, out int pinCode))
            {
                return LoginResult.InvalidPinFormat;
            }

            if (!validationHelper.VerifyPin(currentUser, pinCode))
            {
                return LoginResult.WrongPin;
            }

            return LoginResult.SuccessfulLogin;     
        }


        public void Logout()
        {
            currentUser = null;
        }
        //Todo: Remove all readlines as those will be handled in the ui and instead utilise parameters. use pin logic for transfers.
    }
}
