using Bankomaten.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bankomaten.Service {
    
    /*
        Reminder: This class handles validation needed by the UI flow,
        it should only RETURN useful results to the classes using it instead
        of handling the complete UI flow.
    */
    
    public static class ValidationHelper 
    {
        public static bool VerifyPin (User user, int enteredPin)
        {
            return user.CheckPin(enteredPin);
        }     


    }
}
