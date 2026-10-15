using Bankomaten.Models;
using Bankomaten.Service;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Reflection.Metadata;
using static Bankomaten.Models.Account;

namespace Bankomaten.UI 
{
    internal class LoginMenu 
    {
        /* This method is from User.cs. I will remove the Console.Write and string username from my code
          when you say that you are done here.
         */
        public void showloginmenu()
        {

            Console.WriteLine("=================================");
            Console.WriteLine("       WELCOME TO SYNTAX BANK");
            Console.WriteLine("=================================");
            Console.WriteLine();

            //Console.Write("Username: ");
            //string username = Console.ReadLine();

            //loggedInUser = users.Find(u => u.UserName == username);
            //if (loggedInUser == null)
            //{
            //    Console.WriteLine("Unknown user.");
            //    continue; //returns to the beginning of the loop to ask for username again.
            //}
            ////Pin-validation

            //if (loggedInUser.IsLocked)
            //{
            //    Console.WriteLine("Your account is locked. Please try again later.");
            //    continue; //returns to the beginning of the loop to ask for username again.
            //}
            //Console.Write("Enter PIN: ");
            //if (!int.TryParse(Console.ReadLine(), out int enteredPin))
            //{
            //    Console.WriteLine("Invalid PIN format. Please enter a numeric PIN.");
            //    continue;
            //}
            //var validator = new ValidationHelper();
            //bool isValid = validator.VerifyPin(loggedInUser, enteredPin);

            //if (!isValid)
            //{
            //    continue;
            //}
            //break; // Exit the loop after successful login and account display                          


        }
        //Logged in successfully
    }

    //public static User Login(User[] users)
    //{
    //    Console.Write("Username: ");
    //    string username = Console.ReadLine(); William i UI

    //    foreach (User user in users)
    //    {
    //        if (user.UserName == username)
    //        {
    //            return user;
    //        }
    //    }

    //    return null;
}

