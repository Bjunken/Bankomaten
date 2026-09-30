using Bankomaten.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bankomaten.Service {
    internal class ValidationHelper 
    {
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
