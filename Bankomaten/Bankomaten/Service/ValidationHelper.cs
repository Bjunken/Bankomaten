using Bankomaten.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bankomaten.Service {
    /*
        Reminder: This class handles validation needed by the UI flow,
        it should only RETURN useful results to the classes using it instead
        of handling the complete UI flow.

        I've, (Linus), done some research on DateTime and OOP structure, and in order to create the pin-fail lock, we need to redo this class.
        This is on me, as I underrestimated the task, so I will try and comment-guide as much as I can in here.
    */

    public static class ValidationHelper
    {
        public static bool VerifyPin(User user, int enteredPin)
        {
            return user.CheckPin(enteredPin);
        }
    }

}

