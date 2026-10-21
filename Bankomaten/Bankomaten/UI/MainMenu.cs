using Bankomaten.Models;
using Bankomaten.Service;
using Bankomaten.Utils;

namespace Bankomaten.UI {
    internal class MainMenu {
        private readonly BankService _bank;
        private readonly List<User> _users;
        private readonly DataHandling _dataHandling;
        private int _menuChoice;
        public MainMenu(BankService bank, List<User> users, DataHandling dataHandling) {
            _bank = bank;
            _users = users;
            _dataHandling = dataHandling;
        }
        public void ShowMenu() {
            // Main menu shows after a successful login.
            while (_bank.IsLoggedIn()) {
                header();
                Console.WriteLine("Konto Översikt");
                showAccounts();
                Console.WriteLine();
                Console.WriteLine("Välj ett alternativ:");
                Console.WriteLine("[1] Sätta in pengar");
                Console.WriteLine("[2] Ta ut pengar");
                Console.WriteLine("[3] Överföra pengar");
                Console.WriteLine("[4] Logga ut");

                string userInput = Console.ReadLine();
                menuInputCheck(userInput);
                Console.Clear();

                switch (_menuChoice) {
                    case 1:
                        deposit();
                        Console.Clear();
                        break;
                    case 2:
                        //if (!Withdraw())
                            return;
                        break;
                    case 3:
                        //Transfer();
                        break;
                    case 4:
                        _bank.Logout();
                        return;
                    default:
                        Console.WriteLine("Ogiltigt menyval.");
                        break;
                }
            }
        }
        private void showAccounts() {
            if (_bank.CurrentUser is not User user)
                return;
            for (int i = 0; i < user.Accounts.Count; i++) {
                var account = user.Accounts[i];
                Console.WriteLine($"{i + 1}. {account.AccountName}: {account.Balance:C}");
            }
        }
        private void header() {
            Console.WriteLine("=================================");
            Console.WriteLine("        SYNTAX BANKEN MENY");
            Console.WriteLine("=================================");
            Console.WriteLine();
        }
        private void menuInputCheck(string input) {
            if (!int.TryParse(input, out int userInput)) {
                _menuChoice = 0;
            }
            else {
                _menuChoice = userInput;
            }
        }
        private decimal? parseDecimalCheck(string input) {
            if (decimal.TryParse(input, out decimal result)) {
                return result;
            }
            return null;
        }
        private Account? accountInputCheck(string input) {
            // Parsing
            int? accountNumber = parseCheck(input);
            if (accountNumber == null) {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("\nFel: ");
                Console.ResetColor();
                Console.Write("Inmatningen måste vara en siffra.");
                return null;
            }
            // convert to list index
            int listIndex = accountNumber.Value - 1;

            // checking if the index actually exsists
            if (listIndex < 0 || listIndex >= _bank.CurrentUser.Accounts.Count) {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("\nFel: ");
                Console.ResetColor();
                Console.Write("Kontot existerar inte i listan.");
                return null;
            }
            return _bank.CurrentUser.Accounts[listIndex];
        }
        private int? parseCheck(string input) {
            if (int.TryParse(input, out int result)) {
                return result;
            } else {return null;}
        }
        private void deposit() {
            bool transactionFinished = false;

            while (!transactionFinished) {
                Console.Clear();
                header();
                Console.WriteLine("=== INSÄTTNING ===");
                Console.WriteLine("Dina konton:");
                showAccounts();
                Console.WriteLine();
                Console.WriteLine("(Skriv '0' för att avbryta)");
                Console.Write("Ange Konto (siffra): ");

                string accountInput = Console.ReadLine();
                if (accountInput == "0") return; // <- Will cancel / go back to main menu

                // converting input to account indext
                Account? chosenAccount = accountInputCheck(accountInput);

                // if it was null, return a error message
                if (chosenAccount == null) {
                    Console.ReadKey();
                    continue; // just continues the loop
                }

                // collecting the index from bankservice
                int accountIndex = _bank.CurrentUser.Accounts.IndexOf(chosenAccount);

                Console.WriteLine($"Valt konto: {chosenAccount.AccountName}");
                Console.Write("Belopp att sätta in: ");

                string amountInput = Console.ReadLine();
                if (amountInput == "0") return; // <- will cancel / go back to the main menu

                // parsing the amount into decimal
                decimal? amount = parseDecimalCheck(amountInput);
                if (amount == null || amount <= 0) {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("\nFel: ");
                    Console.ResetColor();
                    Console.Write("Beloppet måste vara ett giltigt nummer över 0 kr.");
                    Console.ReadKey();
                    continue;
                }
                // send it off to bankservice for the actual deposit
                TransactionResult result = _bank.Deposit(amount.Value, accountIndex);

                // then we check back on the result of the validation
                switch (result) {
                    case TransactionResult.SuccessfulTransaction:
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($"\nInsättning lyckades! {amount.Value:C} har satts in på {chosenAccount.AccountName}.");
                        Console.ResetColor();
                        transactionFinished = true;
                        break;

                    case TransactionResult.NonLoggedIn:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("\nFel: ");
                        Console.ResetColor();
                        Console.Write("Du är inte inloggad.");
                        transactionFinished = true;
                        break;

                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\nFel: ");
                        Console.ResetColor();
                        Console.Write("Transaktionen misslyckades.");
                        Console.ReadKey();
                        break;
                }
            }
            Console.WriteLine("\nTryck på valfri tangent för att återgå till huvudmenyn...");
            Console.ReadKey();
        }
        private void withdraw() {
            // Kristin
        }
        private void transfer() {
            // Martin
        }
    }
}