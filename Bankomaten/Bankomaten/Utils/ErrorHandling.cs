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

    internal static class ErrorHandling
    {
        // Keep login result messages here instead of in LoginMenu.
        public static string GetLoginMessage(LoginResult result)
        {
            return result switch
            {
                LoginResult.NoUser => "Användaren kunde inte hittas.",
                LoginResult.InvalidPinFormat => "PIN-koden måste bestå av siffror.",
                LoginResult.UserLocked => "Kontot är låst. Försök igen senare.",
                LoginResult.WrongPin => "Fel PIN-kod.",
                LoginResult.SuccessfulLogin => "Inloggningen lyckades.",
                _ => "Ett okänt inloggningsresultat inträffade."
            };
        }

        // Use the same approach for balance and transaction results from BankService.
        public static string GetTransactionMessage(TransactionResult result)
        {
            return result switch
            {
                TransactionResult.NonLoggedIn => "Du är inte inloggad.",
                TransactionResult.InvalidAccountIndex => "Det valda kontot finns inte.",
                TransactionResult.InsufficientFunds => "Det finns inte tillräckligt med pengar på kontot.",
                TransactionResult.InvalidAmount => "Beloppet är ogiltigt.",
                TransactionResult.WrongPin => "Fel PIN-kod.",
                TransactionResult.InvalidPinFormat => "PIN-koden måste bestå av siffror.",
                TransactionResult.UserLocked => "Kontot är låst. Du loggas ut.",
                TransactionResult.SameAccount => "Du kan inte överföra pengar till samma konto.",
                TransactionResult.SuccessfulTransaction => "Åtgärden genomfördes.",
                _ => "Ett okänt transaktionsresultat inträffade."
            };
        }
    }
}
