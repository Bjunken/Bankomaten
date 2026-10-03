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
        // In the field for this class, we need also need to prepare for the "freeze program", so we're going to need two more private fields,
        // (one int and one DateTime, (DateTime needs to accept Null).
        // We need a third public field which is a bool, it only needs a "larger" 'get;' that returns true and false based on if-checks.
        // After the field is done, you only need to write one method that accepts user and an int in it's parameter, then check if-locked
        // then check if-guess was true or not, then ++ attempts and return true or false, (hint: if PinTries >= 3, LockedAt = DateTime.Now;)
        
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
