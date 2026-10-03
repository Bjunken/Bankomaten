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
    
    internal class ValidationHelper {
        // In this class, we only need a public bool method that works as the "engine", aka it just verifies that account isn't locked and that attempts get reset or locks the program.
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
