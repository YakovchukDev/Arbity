using ArbityDataServer.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArbityDataServer.Credentials.Entities
{
    class CredentialsEntry
    {
        public Exchanger Exchanger { get; set; }
        public string APIKey { get; set; }
        public string SecretKey { get; set; }
    }
}
