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
    }
}
