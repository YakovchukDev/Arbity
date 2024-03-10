using ArbityDataServer.Entities.Enums;

namespace ArbityDataServer.Entities
{
    public class Kline
    {
        public decimal Asks { get; set; }
        public decimal Bids { get; set; }

        public Kline() { }

        public Kline(decimal asks, decimal bids)
        {
            Asks = asks;
            Bids = bids;
        }

        public decimal GetVolume1m(VolumeDataType DataType) 
        {
            if (DataType == VolumeDataType.Asks)
            {
                return Asks;
            }
            else if (DataType == VolumeDataType.Bids)
            {
                return Bids;
            }
            else
            {
                return 0;
            }
        }
    }
}
