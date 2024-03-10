using ArbityDataServer.Entities.Enums;

namespace ArbityDataServer.Credentials.Entities
{
    public class CredentialsEntry
    {
        public Bourse Exchanger { get; set; }
        public string APIKey { get; set; }
        public string SecretKey { get; set; }

        public bool IsNotNullOrEmpty()
        {
            return APIKey != null && APIKey != string.Empty && SecretKey != null && SecretKey != null;
        }
    }
}
