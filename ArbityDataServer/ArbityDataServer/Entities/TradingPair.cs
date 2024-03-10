namespace ArbityDataServer.Entities
{
    public class TradingPair
    {
        public readonly Pair Pair;
        public VolumeData Bids { get; private set; }
        public VolumeData Asks { get; private set; }

        public TradingPair(Pair pair, VolumeData bids, VolumeData asks)
        {
            Pair = pair;
            Bids = bids;
            Asks = asks;
        }

        public void CalculateAll()
        {
            Asks.Calculate();
            Bids.Calculate();
        }
    }
}
