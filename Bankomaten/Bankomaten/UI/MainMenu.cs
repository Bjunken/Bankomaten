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
                        withdraw();
                        Console.Clear();
                        break;
                    case 3:
                        transfer();
                        Console.Clear();
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
        // This method shows the users all their accounts with the balance
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
            Console.WriteLine("       SYNTAXSQUAD BANKEN");
            Console.WriteLine("=================================");
            Console.WriteLine();
        }
        // This method checks if the input is valid and sets the variable accordingly
        private void menuInputCheck(string input) {
            if (!int.TryParse(input, out int userInput)) {
                _menuChoice = 0;
            }
            else {
                _menuChoice = userInput;
            }
        }
        // This method parse string to decimal and returns the result or null if it fails
        private decimal? parseDecimalCheck(string input) {
            if (decimal.TryParse(input, out decimal result)) {
                return result;
            }
            return null;
        }
        // This method checks if the input is a valid account number 
        private Account? accountInputCheck(string input) {            
            int? accountNumber = parseCheck(input);
            if (accountNumber == null) {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("\nFel: ");
                Console.ResetColor();
                Console.Write("Inmatningen måste vara en siffra.");
                return null;
            }
            // Convert to list index
            int listIndex = accountNumber.Value - 1;

            // Checking if the index actually exists
            if (listIndex < 0 || listIndex >= _bank.CurrentUser.Accounts.Count) {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("\nFel: ");
                Console.ResetColor();
                Console.Write("Kontot existerar inte i listan.");
                return null;
            }
            return _bank.CurrentUser.Accounts[listIndex];
        }
        // This method checks if the input is a valid integer
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
                if (accountInput == "0") return; 
                // Converting input to account index
                Account? chosenAccount = accountInputCheck(accountInput);
                // If it is null, return an error message
                if (chosenAccount == null) {
                    Console.ReadKey();
                    continue; 
                }
                // Fetch the index from bankservice
                int accountIndex = _bank.CurrentUser.Accounts.IndexOf(chosenAccount);

                Console.WriteLine($"Valt konto: {chosenAccount.AccountName}");
                Console.Write("Belopp att sätta in: ");

                string amountInput = Console.ReadLine();
                if (amountInput == "0") return; 
                // Parsing the amount into decimal
                decimal? amount = parseDecimalCheck(amountInput);
                if (amount == null || amount <= 0) {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("\nFel: ");
                    Console.ResetColor();
                    Console.Write("Beloppet måste vara över 0 kr.");
                    Console.ReadKey();
                    continue;
                }
                // Pass to bankservice for deposit
                TransactionResult result = _bank.Deposit(amount.Value, accountIndex);
                // Check the result of the validation
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
        // Only difference between deposit and withdraw is that withdraw requires a pin code, so we will ask for it here
        private void withdraw() {
            bool transactionFinished = false;

            while (!transactionFinished)
            {
                Console.Clear();
                header();
                Console.WriteLine("=== UTTAG ===");
                Console.WriteLine("Dina konton:");
                showAccounts();
                Console.WriteLine();
                Console.WriteLine("(Skriv '0' för att avbryta)");
                Console.Write("Ange Konto (siffra): ");

                string accountInput = Console.ReadLine();
                if (accountInput == "0") return; 
                
                Account? chosenAccount = accountInputCheck(accountInput); 
                
                if (chosenAccount == null)
                {
                    Console.ReadKey();
                    continue; 
                }
                
                int accountIndex = _bank.CurrentUser.Accounts.IndexOf(chosenAccount);

                Console.WriteLine($"Valt konto: {chosenAccount.AccountName}");
                Console.Write("Belopp att ta ut: ");

                string amountWithdrawal = Console.ReadLine();
                if (amountWithdrawal == "0") return; 
                
                decimal? amount = parseDecimalCheck(amountWithdrawal);
                if (amount == null || amount <= 0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("\nFel: ");
                    Console.ResetColor();
                    Console.Write("Beloppet måste vara över 0 kr.");
                    Console.ReadKey();
                    continue;
                }
                // Pass to BankService for validation of the Pin
                string pinText = null; 
                Console.Write("Ange PIN kod: ");
                pinText = Console.ReadLine();
                TransactionResult result = _bank.Withdraw(amount.Value, accountIndex, pinText);                           
                                
                switch (result)
                {
                    case TransactionResult.SuccessfulTransaction:
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($"\nUttaget lyckades! {amount.Value:C} har tagits ut från {chosenAccount.AccountName}.");
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
        // Only difference between deposit and transfer is that we move balance between two accounts
        private void transfer() {
            bool transactionFinished = false;

            while (!transactionFinished)
            {
                Console.Clear();
                header();
                Console.WriteLine("=== Överföring ===");
                Console.WriteLine("Dina konton:");
                showAccounts();
                Console.WriteLine();
                Console.WriteLine("(Skriv '0' för att avbryta)");
                Console.Write("Ange kontot du vill överföra från: ");
                // Move balance from Account 1 to Account 2 starts here
                string _fromAccount = Console.ReadLine();
                if (_fromAccount == "0")
                {
                    return;
                }

                Account? fromAccount = accountInputCheck(_fromAccount);

                if (fromAccount == null)
                {
                    Console.ReadKey();
                    continue;
                }

                int fromIndex = _bank.CurrentUser.Accounts.IndexOf(fromAccount);
                Console.WriteLine($"Valt konto: {fromAccount.AccountName}");
                Console.Write("Ange konto du vill överföra till: ");
                // Move balance from Account 1 to Account 2 ends here
                // This is the start of the receiving Account
                string _toAccount = Console.ReadLine();
                if (_toAccount == "0")
                {
                    return;
                }

                Account? toAccount = accountInputCheck(_toAccount);

                if (toAccount == null)
                {
                    Console.ReadKey();
                    continue;
                }
                
                int toIndex = _bank.CurrentUser.Accounts.IndexOf(toAccount);
                Console.WriteLine($"Valt konto: {toAccount.AccountName}");
                Console.Clear();
                header();
                Console.WriteLine("=== Överföring ===");
                Console.WriteLine("Dina konton:");
                showAccounts();
                Console.WriteLine();
                Console.WriteLine($"Överför från {fromAccount.AccountName} till {toAccount.AccountName} ");
                
                // Asking the user for the amount of balance to transfer
                Console.Write("Ange vilket belopp du vill överföra: ");
                string amountInput = Console.ReadLine();
                if (amountInput == "0")
                {
                    return;
                }

                decimal? amount = parseDecimalCheck(amountInput);
                if (amount == null || amount <= 0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("\nFel: ");
                    Console.ResetColor();
                    Console.Write("Beloppet måste vara över 0 kr.");
                    Console.ReadKey();
                    continue;
                }
                // Pass to BankService for validation of the transfer
                TransactionResult result = _bank.Transfer(fromIndex, toIndex, amount.Value);

                switch (result)
                {
                    case TransactionResult.SuccessfulTransaction:
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($"\nÖverföring lyckades! {amount.Value:C} har tagits ut från {fromAccount.AccountName} överförts till {toAccount.AccountName}.");
                        Console.ResetColor();
                        Console.ReadKey();
                        transactionFinished = true;
                        break;

                    case TransactionResult.NonLoggedIn:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("\nFel: ");
                        Console.ResetColor();
                        Console.Write("Du är inte inloggad.");
                        Console.ReadKey();
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
        }
    }
}