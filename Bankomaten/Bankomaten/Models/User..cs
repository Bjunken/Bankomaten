using Bankomaten.Service;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

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
        [JsonInclude]private int PinCode { get; }
        public List<Account> Accounts { get; }
        
        private int PinTries;
        private DateTime? LockedAt;

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
        private void ResetPinTries()
        {
            PinTries = 0;
            LockedAt = null;

        }
    }
}
