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
        public Account[] Accounts { get; set; }
        /*
            public Account[] Accounts { get; set;}
            ^ This needs to be changed to it's own List class, rather than being an array,
            since each user must be able to have a different number of Accounts.

            You can put it inside of Account.cs
        */

        public User(string userName, int pinCode, Account[] accounts)
        {
            UserName = userName;
            PinCode = pinCode;
            Accounts = accounts;
        }

        /*
            Login() method should only find and return the User matching the username.
            The actual login flow and PIN validation will be handled by other parts of the application
        */
        public static User Login(User[] users)
        {
            Console.Write("Username: ");
            string username = Console.ReadLine(); William i UI

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
