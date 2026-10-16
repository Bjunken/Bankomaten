using Bankomaten.Service;
using Bankomaten.Utils;

namespace Bankomaten.UI
{
    internal class LoginMenu
    {
        public bool ShowLoginMenu(BankService bank)
        {
            Console.Title = "Syntax Banken";

            while (true)
            {
                Console.WriteLine("=================================");
                Console.WriteLine("  VÄLKOMMEN TILL SYNTAX BANKEN");
                Console.WriteLine("=================================");
                Console.Write("Användarnamn: ");
                string? userName = Console.ReadLine();
                if (userName == null)
                    return false;

                Console.Write("Ange PIN: ");
                string? pinText = Console.ReadLine();
                if (pinText == null)
                    return false;

                // Pass the text to BankService so login validation stays outside the UI.
                LoginResult result = bank.Login(userName, pinText);
                Console.WriteLine(ErrorHandling.GetLoginMessage(result));
                // Only a successful login leaves this menu and opens the main menu.
                if (result == LoginResult.SuccessfulLogin)
                    return true;

                Console.WriteLine("Press Enter to try again.");
                Console.ReadLine();
                Console.Clear();
            }
        }
    }
}
