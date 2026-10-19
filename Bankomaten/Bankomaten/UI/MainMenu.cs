using Bankomaten.Models;
using Bankomaten.Service;
using Bankomaten.Utils;

namespace Bankomaten.UI
{
    internal class MainMenu
    {
        private readonly BankService _bank;
        private readonly List<User> _users;
        private readonly DataHandling _dataHandling;

        public MainMenu(BankService bank, List<User> users, DataHandling dataHandling)
        {
            _bank = bank;
            _users = users;
            _dataHandling = dataHandling;
        }

        public void ShowMenu()
        {
            // Main menu shows after a successful login.
            while (_bank.IsLoggedIn())
            {
                Console.WriteLine("=================================");
                Console.WriteLine("        SYNTAX BANKEN MENY");
                Console.WriteLine("=================================");
                Console.WriteLine();
                Console.WriteLine("Konton och saldo:");
                ShowAccounts();
                Console.WriteLine();
                Console.WriteLine("Välj ett alternativ:");
                Console.WriteLine("[1] Sätta in pengar");
                Console.WriteLine("[2] Ta ut pengar");
                Console.WriteLine("[3] Överföra pengar");
                Console.WriteLine("[4] Logga ut");

                if (!ReadInt("Välj alternativ: ", out int choice))
                {
                    _bank.Logout();
                    return;
                }

                Console.Clear();

                ShowAccounts();
                switch (choice)
                {
                    case 1:
                        Deposit();
                        break;
                    case 2:
                        if (!Withdraw())
                            return;
                        break;
                    case 3:
                        Transfer();
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

        private void ShowAccounts()
        {
            if (_bank.CurrentUser is not User user)
                return;

            // Show account numbers starting at 1; internal list indexes start at 0.
            for (int i = 0; i < user.Accounts.Count; i++)
            {
                var account = user.Accounts[i];
                Console.WriteLine($"{i + 1}. {account.AccountName}: {account.Balance:C}");
            }
        }

        private void CheckBalance()
        {
            if (!ReadAccountIndex("Ange kontonummer: ", out int accountIndex))
                return;

            TransactionResult result = _bank.GetBalance(accountIndex, out decimal balance);
            Console.WriteLine(ErrorHandling.GetTransactionMessage(result));
            // Use the balance only when the service confirms that the account was found.
            if (result == TransactionResult.SuccessfulTransaction)
                Console.WriteLine($"Saldo: {balance:C}");
        }

        private void Deposit()
        {
            if (!ReadAccountIndex("Ange kontonummer: ", out int accountIndex) ||
                !ReadDecimal("Ange belopp: ", out decimal amount))
                return;

            TransactionResult result = _bank.Deposit(amount, accountIndex);
            Console.WriteLine(ErrorHandling.GetTransactionMessage(result));
            SaveAfterSuccess(result);
        }

        private bool Withdraw()
        {
            if (!ReadAccountIndex("Ange kontonummer: ", out int accountIndex) ||
                !ReadDecimal("Ange belopp: ", out decimal amount))
                return true;

            Console.Write("Ange PIN: ");
            string? pinText = Console.ReadLine();
            if (pinText == null)
            {
                _bank.Logout();
                return false;
            }

            TransactionResult result = _bank.Withdraw(amount, accountIndex, pinText);
            Console.WriteLine(ErrorHandling.GetTransactionMessage(result));
            SaveAfterSuccess(result);
            // A locked account ends the session and returns to login in Program.
            if (result == TransactionResult.UserLocked)
            {
                _bank.Logout();
                return false;
            }
            return true;
        }

        private void Transfer()
        {
            if (!ReadAccountIndex("Från konto: ", out int fromIndex) ||
                !ReadAccountIndex("Till konto: ", out int toIndex) ||
                !ReadDecimal("Ange belopp: ", out decimal amount))
                return;

            TransactionResult result = _bank.Transfer(fromIndex, toIndex, amount);
            Console.WriteLine(ErrorHandling.GetTransactionMessage(result));
            SaveAfterSuccess(result);
        }

        private bool ReadAccountIndex(string prompt, out int accountIndex)
        {
            if (!ReadInt(prompt, out int displayedNumber))
            {
                accountIndex = -1;
                return false;
            }

            // Convert the displayed account number to the service's zero-based index.
            accountIndex = displayedNumber - 1;
            return true;
        }

        private bool ReadInt(string prompt, out int value)
        {
            while (true)
            {
                Console.Write(prompt);
                string? input = Console.ReadLine();
                if (input == null)
                {
                    _bank.Logout();
                    value = 0;
                    return false;
                }
                if (int.TryParse(input, out value))
                    return true;

                Console.WriteLine("Ange ett giltigt heltal.");
            }
        }

        private bool ReadDecimal(string prompt, out decimal value)
        {
            while (true)
            {
                Console.Write(prompt);
                string? input = Console.ReadLine();
                if (input == null)
                {
                    _bank.Logout();
                    value = 0;
                    return false;
                }
                if (decimal.TryParse(input, out value))
                    return true;

                Console.WriteLine("Ange ett giltigt belopp.");
            }
        }

        private void SaveAfterSuccess(TransactionResult result)
        {
            // Press Enter to continue after a successful transaction, and save the user data.
            if (result == TransactionResult.SuccessfulTransaction)
                _dataHandling.Save(_users);
            Console.WriteLine("tryck Enter till att fortsätta");
            Console.ReadLine();
            Console.Clear();
        }
    }
}
