using Bankomaten.Service;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bankomaten.Models  {
    /*
        Reminder: This class defines the user model USED by Service and UI,
        keep this class focused on user data and the methods required to access it.
        (aka no write/readLines)
    */
    public class User  {
        public string UserName { get; set; } // This only needs a get, so can remove "set;"
        public int PinCode { get; set; } // This doesn't need "{ get; set;}" since it's set inside of the constructor. Should also be private, so it can't be changed from outside the class.
        public List<Account> Accounts { get; set; } // doesn't need "set;" just a "get;" since it's set inside of creation
        public User(string userName, int pinCode, List<Account> accounts)
        {
            UserName = userName;
            PinCode = pinCode;
            Accounts = accounts;
        }
        // Login method to find a user by username from a list of users.      
        public static User Login(List<User> users, string username)
        {
            Console.Write("Username: ");
          
            foreach (User user in users)
            {
                if (user.UserName == username)
                {
                    return user;
                }
            }

            return null;
        }
        // The whole of "user Login()", is in the wrong place, also not part of your intended assignment for this task.
        // The only thing you have left in "User.cs" to do is; a public method that checks if the pin is correct or not and return said value.
    }
}
