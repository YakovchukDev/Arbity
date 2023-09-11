using ArbityDataServer.Credentials.Entities;
using ArbityDataServer.Entities.Enums;
using ArbityDataServer.LoggingService;
using System;
using System.IO;
using System.Reflection;
using System.Text.Json;

namespace ArbityDataServer.Credentials
{
    class ExchangerCredentials
    {
        private const string _fileName = @"D:\Credentials.json";

        public Status Status { get; private set; }

        private List<CredentialsEntry> credentialsEntries = new List<CredentialsEntry>();

        public ExchangerCredentials()
        {
            LoadCredentials();
        }

        public CredentialsEntry GetCredentials(Exchanger exchanger)
        {
            foreach(CredentialsEntry credentialsEntry in credentialsEntries) 
            {
                if (credentialsEntry.Exchanger == exchanger)
                {
                    return credentialsEntry;
                }
            }
            Logger.Failure($"Credentials: {exchanger} is missing from the credentials list", MethodBase.GetCurrentMethod());
            return null;
        }

        private void LoadCredentials()
        {
            try
            {
                if (!File.Exists(_fileName))
                {
                    Status = Status.Failure;
                    Logger.Failure("Credentials: File not found!", MethodBase.GetCurrentMethod());
                    return;
                }

                string jsonString = File.ReadAllText(_fileName);
                credentialsEntries = JsonSerializer.Deserialize<List<CredentialsEntry>>(jsonString);

                if (credentialsEntries == null)
                {
                    Status = Status.Failure;
                    Logger.Failure("Credentials: Failed to deserialize credentials.", MethodBase.GetCurrentMethod());
                    return;
                }

                Status = Status.Success;
                Logger.Success("Credentials: File read successfully!", MethodBase.GetCurrentMethod());
            }
            catch (Exception ex)
            {
                Status = Status.Error;
                Logger.Error(ex.Message, MethodBase.GetCurrentMethod());
            }
        }
    }
}
