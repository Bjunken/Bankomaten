using Bankomaten.Service;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization; // allows the use of [JasonIgnore/Include] within this class

namespace Bankomaten.Models {
    public class User {
        public string UserName { get; } // <- only have a get because it should not be able to be set outside of a method
        [JsonInclude]
        private int PinCode { get; } // <- is [JsonInclude] to ensure that the pin gets saved into the JSON-file
        public List<Account> Accounts { get; } // <- only have a get because it should not be able to be set outside of a method
        private int _PinTries; // <- doesn't have a get/set because it should not be able to be set outside of a method
        private DateTime? _LockedAt; // <- doesn't have a get/set because it should not be able to be set outside of a method
        [JsonIgnore]
        public bool IsLocked { // <- is [JsonIgnore] to ensure it does not get saved into the JSON-file.
            get {
                if (_LockedAt == null)
                    return false;
                // else returns the lock time remaining
                return (DateTime.Now - _LockedAt.Value).TotalSeconds < 180;
            }
        }
        public User(string userName, int pinCode, List<Account> accounts) {
            UserName = userName;
            PinCode = pinCode;
            Accounts = accounts;
        }
        // this method checks if the entered pin is correct and handles the logic for locking the account after 3 failed attempts
        public bool checkPin(int enteredPin) {
            if (IsLocked)
                return false;
            if (_LockedAt !=null) {
                _resetPinTries(); }
            if (enteredPin == PinCode) {
                _resetPinTries(); // reset tries on successful login
                return true;
            }

            _PinTries++;
            if (_PinTries >= 3) {
                _LockedAt = DateTime.Now;
            }
            return false;
        }
        // this method resets the pin tries and unlocks the account
        private void _resetPinTries() {
            _PinTries = 0;
            _LockedAt = null;
        }
    }
}