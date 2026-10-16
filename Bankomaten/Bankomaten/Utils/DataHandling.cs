using Bankomaten.Models;
using System.Text.Json;

namespace Bankomaten.Utils
{
    internal class DataHandling
    {
        // Load and Save use the same file path.
        private static readonly string FilePath = Path.Combine("Data", "accounts.json");
        private static readonly JsonSerializerOptions JsonOptions = new() { WriteIndented = true };

        public List<User> Load()
        {
            // Start with an empty list on the first run instead of failing.
            if (!File.Exists(FilePath))
                return new List<User>();

            try
            {
                string json = File.ReadAllText(FilePath);
                return JsonSerializer.Deserialize<List<User>>(json, JsonOptions) ?? new List<User>();
            }
            // A broken file or read error should not stop the program.
            catch (JsonException)
            {
                Console.WriteLine("Kontofilen innehåller ogiltig JSON. Startar med en tom användarlista.");
                return new List<User>();
            }
            catch (IOException)
            {
                Console.WriteLine("Kontofilen kunde inte läsas. Startar med en tom användarlista.");
                return new List<User>();
            }
        }

        public void Save(List<User> users)
        {
            // Create the Data folder automatically before writing accounts.
            string? directory = Path.GetDirectoryName(FilePath);
            if (!string.IsNullOrEmpty(directory))
                Directory.CreateDirectory(directory);

            // Serialize the existing User and Account models as readable JSON.
            string json = JsonSerializer.Serialize(users, JsonOptions);
            File.WriteAllText(FilePath, json);
        }
    }
}
