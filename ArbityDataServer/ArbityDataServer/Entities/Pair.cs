using ArbityDataServer.Entities.Enums;

namespace ArbityDataServer.Entities
{
    class Pair
    {
        public readonly CoinShortName FirstCoin;
        public readonly CoinShortName SecondCoin;

        public Pair(CoinShortName firstCoin, CoinShortName secondCoin)
        {
            FirstCoin = firstCoin;
            SecondCoin = secondCoin;
        }

        public string GetAttribute => $"{FirstCoin}{SecondCoin}";
    }
}
