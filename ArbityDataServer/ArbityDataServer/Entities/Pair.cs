using ArbityDataServer.Entities.Enums;

namespace ArbityDataServer.Entities
{
    class Pair
    {
        public CoinName FirstCoin { get; set; }
        public CoinName SecondCoin { get; set; }

        public Pair(CoinName firstCoin, CoinName secondCoin)
        {
            FirstCoin = firstCoin;
            SecondCoin = secondCoin;
        }

        public string GetAttribute => $"{FirstCoin}{SecondCoin}";
    }
}
