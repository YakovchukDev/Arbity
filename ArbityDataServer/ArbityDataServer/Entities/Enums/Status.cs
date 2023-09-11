using System.ComponentModel;

namespace ArbityDataServer.Entities.Enums
{
    enum Status
    {
        [DefaultValue(Unknown)]
        Unknown = 0,
        Success = 1,
        Failure = 2,
        Error = 3,
    }
}
