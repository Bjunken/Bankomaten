using Bankomaten.Models;
using Bankomaten.Service;
using Bankomaten.Utils;
using System.Threading;
namespace Bankomaten.UI
{
   
    internal class LoginMenu
    {
        private LoginResult result;
        public bool ShowLoginMenu(BankService bank)
        {
            Console.Title = "SyntaxSquad Banken";
            // Continues until a successful login or the user cancels the login process
            while (true) {
                Console.WriteLine("===========================================");
                Console.WriteLine("    VÄLKOMMEN TILL SYNTAXSQUAD BANKEN");
                Console.WriteLine("===========================================");
                Console.Write("Användarnamn: ");
                string? userName = Console.ReadLine();
                if (userName == null) { 
                return false;
                }
                Console.Write("Ange PIN: ");
                string? pinText = Console.ReadLine();
                if (pinText == null) {
                    
                   return false;
                }
                // Pass the input to BankService for login validation
                LoginResult result = bank.Login(userName, pinText);
                // If login is correct, display a green message during 2 seconds then enter mainmenu
                if (result == LoginResult.SuccessfulLogin) {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine(ErrorHandling.GetLoginMessage(result));
                    Thread.Sleep(2000);
                    Console.ResetColor();
                    return true;                    
                }
                // If login fails, display a red message during 2 seconds then loop to the beginning of the while loop
                else {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(ErrorHandling.GetLoginMessage(result));
                    Thread.Sleep(2000);
                    Console.ResetColor();
                }                
                Console.WriteLine("Tryck Enter för att försöka igen");
                Console.ReadLine();
                Console.Clear();
            }
        }       
    }
}
