using Bankomaten.Service;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bankomaten.Models 
{
    public class User 
    {
        public string UserName { get; set; }
        public int PinCode { get; set; }
        public Account[] Accounts { get; set; } 

        public User(string userName, int pinCode, Account[] accounts)
        {
            UserName = userName;
            PinCode = pinCode;
            Accounts = accounts;
        }

        public static User Login(User[] users)
        {
            Console.Write("Username: ");
            string username = Console.ReadLine();

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
