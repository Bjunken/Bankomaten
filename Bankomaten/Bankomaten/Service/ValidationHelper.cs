using Bankomaten.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bankomaten.Service {
    /*
        Reminder: This class handles validation needed by the UI flow,
        it should only RETURN useful results to the classes using it instead
        of handling the complete UI flow.

        I've, (Linus), done some research on DateTime, and in order to create the pin-fail lock, we need to redo this class.
        This is on me, as I underrestimated the task, so I will try and comment-guide as much as I can in here.
    */
    
    internal class ValidationHelper {
        // In the field for this class, we need two private fields, (one int and one DateTime, (DateTime needs to accept Null).
        // We also need a third public field which is a bool, it only needs a "larger" get that returns true and false based on if-checks.
        // After the field is done, you only need to write one method that accepts user and an int in it's parameter, then check if-locked
        // then check if-guess was true or not, then ++ attempts and return true or false, (hint: if PinTries >= 3, LockedAt = DateTime.Now;)
        public static bool VerifyPin(User user) {
            return VerifyPin(user, 3);
        }
        public static bool VerifyPin(User user, int maxAttempts) {
            int attempts = 0;
            while (attempts < maxAttempts) {
                Console.Write("Enter code: "); 
                if (!int.TryParse(Console.ReadLine(), out int enteredPin)) {
                    attempts++;
                    continue;
                }
                if (enteredPin == user.PinCode) {
                    return true;
                }
                attempts++;
                //Console.WriteLine($"Wrong code. Tries left: {maxAttempts - attempts}"); -William
            }
            return false;
        }
    }
}
