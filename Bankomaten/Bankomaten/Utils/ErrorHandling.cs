using System;
using System.Collections.Generic;
using System.Text;

namespace Bankomaten.Utils
{
    enum LoginResult
    {
        NoUser,
        InvalidPinFormat,
        UserLocked,
        WrongPin,
        SuccessfulLogin
    }
    enum TransactionResult
    {
        NonLoggedIn,
        InvalidAccountIndex,
        InsufficientFunds,
        InvalidAmount,
        WrongPin,
        InvalidPinFormat,
        UserLocked,
        SameAccount,
        SuccessfulTransaction
    }

    internal class ErrorHandling 
    {
        
    }
}
