using Bankomaten.Service;
using Bankomaten.Utils;

namespace Bankomaten.UI
{
    internal class LoginMenu
    {
        public bool ShowLoginMenu(BankService bank)
        {
            // Name of the console window
            Console.Title = "Syntax Banken";


            // Login menu loop, continues until a successful login or the user cancels the login process.
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

                // If login fails, prompt the user to try again.
                Console.WriteLine("tryck Enter för att försöka igen");
                Console.ReadLine();
                Console.Clear();
            }
        }
    }
}
