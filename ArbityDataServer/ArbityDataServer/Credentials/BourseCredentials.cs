using ArbityDataServer.Credentials.Entities;
using ArbityDataServer.Entities.Enums;
using ArbityDataServer.LoggingService;
using System.Text.Json;

namespace ArbityDataServer.Credentials
{
    class BourseCredentials
    {
        private const string _fileName = @"D:\Credentials.json";

        private List<CredentialsEntry> credentialsEntries = new List<CredentialsEntry>();

        public BourseCredentials()
        {
            LoadCredentials();
        }

        public CredentialsEntry GetCredentials(Bourse bourse)
        {
            foreach(CredentialsEntry credentialsEntry in credentialsEntries) 
            {
                if (credentialsEntry.Exchanger == bourse)
                {
                    return credentialsEntry;
                }
            }
            Logger.Error($"Credentials: {bourse} is missing from the credentials list");
            return null;
        }

        private void LoadCredentials()
        {
            try
            {
                if (!File.Exists(_fileName))
                {
                    Logger.Error("Credentials: File not found!");
                    return;
                }

                string jsonString = File.ReadAllText(_fileName);
                credentialsEntries = JsonSerializer.Deserialize<List<CredentialsEntry>>(jsonString);

                if (credentialsEntries == null || credentialsEntries.Count == 0)
                {
                    Logger.Error("Credentials: Failed to deserialize credentials.");
                    return;
                }

                Logger.Success("Credentials: File read successfully!");
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
            }
        }
    }
}
