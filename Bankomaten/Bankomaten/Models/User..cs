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
        // Save the PIN in JSON; failed attempts and lock duration only exist for this run.
        [JsonInclude]private int PinCode { get; }
        public List<Account> Accounts { get; }
        
        private int PinTries;
        private DateTime? LockedAt;

<<<<<<< HEAD
        // Do not save the lock status; users are unlocked after a restart.
        [JsonIgnore]
        public bool IsLocked
=======
        [JsonIgnore]public bool IsLocked
>>>>>>> a3e59978d3dc86e706fc7dfa3a6265f950508f0e
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
