using Bankomaten.Service;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization; //Activate saving and loading of private properties in the User class, like PinCode.

namespace Bankomaten.Models
{
    /*
        Reminder: This class defines the user model USED by Service and UI,
        keep this class focused on user data and the methods required to access it.
        (aka no write/readLines)
    */
    public class User
    {
        public string UserName { get; }
        [JsonInclude]private int PinCode { get; } //This property is private and can only be accessed within the User class. It is used to store the user's PIN code for authentication purposes.
        public List<Account> Accounts { get; }
        
        private int PinTries;
        private DateTime? LockedAt;
        //If the user has failed to enter the correct pin 3 times, the account will be locked for 30 seconds. This property checks if the account is currently locked.
        public bool IsLocked
        {
            get
            {
                if (LockedAt == null)
                    return false;

                return (DateTime.Now - LockedAt.Value).TotalSeconds < 30;
            }
        }
        public User(string userName, int pinCode, List<Account> accounts)
        {
            UserName = userName;
            PinCode = pinCode;
            Accounts = accounts;
        }
        // This method checks if the entered pin is correct and handles the logic for locking the account after 3 failed attempts.
        public bool CheckPin(int enteredPin)
        {
            if (IsLocked)
                return false;
            if (LockedAt !=null)
            { ResetPinTries(); }
            if (enteredPin == PinCode)
            {
                ResetPinTries(); // Reset tries on successful login
                return true;
            }

            PinTries++;
            if (PinTries >= 3)
            {
                LockedAt = DateTime.Now;
               
                Console.WriteLine("Too many failed attempts. Your account is locked for 30 seconds."); // Remove when all done in UI.
            }
            return false;
        }
        // This method resets the pin tries and unlocks the account.
        private void ResetPinTries()
        {
            PinTries = 0;
            LockedAt = null;

        }
    }
}
