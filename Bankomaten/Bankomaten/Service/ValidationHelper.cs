using Bankomaten.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bankomaten.Service {
    // verification pin helper method that returns true/false, where the pin check goes through here into User.cs for verification
    public class ValidationHelper {
        public bool verifyPin(User user, int enteredPin) {
            return user.checkPin(enteredPin);
        }
    }
}