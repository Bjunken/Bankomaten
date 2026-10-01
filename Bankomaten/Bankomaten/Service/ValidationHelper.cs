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
    
    internal class ValidationHelper 
    {

        /*
            The method "VerifyPin" needs to change a little bit.
            Remember that UI-logic should only be handled in the UI methods, while this
            method should only validate the PIN and then another UI method will call it and display
            the result / error message.

            Sidenote:

            Make sure invalid inputs also counts as an attempt,
            and this method should not handle the rest of the login flow.
        */

        // Validates the PIN
        public static bool VerifyPin(User user)
        {
            return VerifyPin(user, 3);
        }

        public static bool VerifyPin(User user, int maxAttempts)
        {
            int attempts = 0;

            while (attempts < maxAttempts)
            {
                Console.Write("Enter code: ");

                if (!int.TryParse(Console.ReadLine(), out int enteredPin))
                {
                    attempts++;
                    continue;
                }

                if (enteredPin == user.PinCode)
                {
                    return true;
                }

                attempts++;
                Console.WriteLine($"Wrong code. Tries left: {maxAttempts - attempts}");
            }

            return false;
        }


    }
}
