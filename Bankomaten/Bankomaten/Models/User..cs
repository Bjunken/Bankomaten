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
        public string UserName { get; set; }
        public int PinCode { get; set; }
        public List<Account> Accounts { get; set; }
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


    }
}
