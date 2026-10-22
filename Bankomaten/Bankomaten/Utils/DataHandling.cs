using Bankomaten.Models;
using System.Text.Json;

namespace Bankomaten.Utils
{
    internal class DataHandling
    {
        // saves the file in the data folder / sets paths
        private static readonly string FilePath = Path.Combine(AppContext.BaseDirectory, "Data", "accounts.json");
        public List<User> Load() {
            // checks if file exists / if not create one.
            if (!File.Exists(FilePath))
                return new List<User>();

            /*
            try / catch for checking if there are any users in the file if not, creates an empty list of users.
            If empty, throws an exception error message.
            */
            try {
                string json = File.ReadAllText(FilePath);
                List<User>? users = JsonSerializer.Deserialize<List<User>>(json);

                if (users == null) {
                    return new List<User>();
                }
                return users;
            }
            catch (Exception) {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("Fel: ");
                Console.ResetColor();
                Console.Write("Kunde inte läsa kontofilen. Startar med en tom användarlista.");
                return new List<User>();
            }
        }

        public void Save(List<User> users) {
            // checks / creates the data folder if it doesn't exists
            Directory.CreateDirectory(Path.GetDirectoryName(FilePath)!);

            // serializes / saves the file back to Json with indented options for the Json format
            var options = new JsonSerializerOptions { WriteIndented = true};

            string json = JsonSerializer.Serialize(users, options);
            File.WriteAllText(FilePath, json);
        }
    }
}
