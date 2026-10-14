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
        // Turns a LoginResult into a message the UI can show.
        public static string GetLoginMessage(LoginResult result) {
            switch (result) {
                case LoginResult.NoUser:
                    return "Användaren hittades inte. Kontrollera användarnamnet och försök igen.";
                case LoginResult.InvalidPinFormat:
                    return "PIN-koden får bara innehålla siffror.";
                case LoginResult.UserLocked:
                    return "För många misslyckade försök. Ditt konto är låst i 30 sekunder.";
                case LoginResult.WrongPin:
                    return "Fel PIN-kod. Försök igen.";
                case LoginResult.SuccessfulLogin:
                    return "Inloggningen lyckades.";
                default:
                    return "Något gick fel vid inloggningen.";
            }
        }
        // Turns a TransactionResult into a message the UI can show.
        public static string GetTransactionMessage(TransactionResult result) {
            switch (result) {
                case TransactionResult.NonLoggedIn:
                    return "Du är inte inloggad.";
                case TransactionResult.InvalidAccountIndex:
                    return "Kontot finns inte. Välj ett konto från listan.";
                case TransactionResult.InsufficientFunds:
                    return "Otillräckligt saldo på kontot.";
                case TransactionResult.InvalidAmount:
                    return "Beloppet måste vara större än 0.";
                case TransactionResult.WrongPin:
                    return "Fel PIN-kod. Transaktionen avbröts.";
                case TransactionResult.InvalidPinFormat:
                    return "PIN-koden får bara innehålla siffror.";
                case TransactionResult.UserLocked:
                    return "För många misslyckade försök. Ditt konto är låst i 30 sekunder.";
                case TransactionResult.SameAccount:
                    return "Du kan inte överföra pengar till samma konto.";
                case TransactionResult.SuccessfulTransaction:
                    return "Transaktionen lyckades.";
                default:
                    return "Något gick fel med transaktionen.";
            }
        }
    }
}
