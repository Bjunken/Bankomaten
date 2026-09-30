using System;
using System.Collections.Generic;
using System.Text;

namespace Bankomaten.Models 
{
    public class User 
    {
        public string UserName { get; set; }
        public int PinCode { get; set; }

        public User(string userName, int pinCode)
        {
            UserName = userName;
            PinCode = pinCode;
        }

        public static User Login(User[] users)
        {
            int attempts = 0;

            while (attempts < 3)
            {
                Console.Write("Username: ");
                string username = Console.ReadLine();
                Console.Write("Enter code: ");
                int pin;

                if (!int.TryParse(Console.ReadLine(), out pin))
                {
                    Console.WriteLine("Wrong code entered.");
                    attempts++;
                    continue;
                }                

                foreach (User user in users)
                {
                    if (user.UserName == username && user.PinCode == pin)
                    {
                        return user;
                    }
                }
                attempts++;
                Console.WriteLine($"Wrong User or code. Tries left: {3 - attempts}");
            }
            return null;

        }


    }
}
