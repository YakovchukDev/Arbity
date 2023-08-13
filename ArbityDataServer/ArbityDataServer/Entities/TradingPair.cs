using ArbityDataServer.Entities.Enums;

namespace ArbityDataServer.Entities
{
    class TradingPair
    {
        public Pair Pair { get; set; }
        public Exchanger Exchanger { get; set; }
        public Volume Bids { get; set; }
        public Volume Asks { get; set; }

        public TradingPair(Pair pair, Exchanger exchanger, Volume bids, Volume asks)
        {
            Pair = pair;
            Exchanger = exchanger;
            Bids = bids;
            Asks = asks;
        }
    }
}
