using ArbityDataServer.Entities.Enums;

namespace ArbityDataServer.Entities
{
    class TradingPair
    {
        public readonly Pair Pair;
        public readonly Exchanger Exchanger;
        public Volume Bids { get; private set; }
        public Volume Asks { get; private set; }

        public TradingPair(Pair pair, Exchanger exchanger, Volume bids, Volume asks)
        {
            Pair = pair;
            Exchanger = exchanger;
            Bids = bids;
            Asks = asks;
        }

        public void SetVolume(Volume bids, Volume asks)
        {
            Bids = bids;
            Asks = asks;
        }
    }
}
