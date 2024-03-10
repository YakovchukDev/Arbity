using ArbityDataServer.Entities.Enums;

namespace ArbityDataServer.Entities
{
    public class Pair
    {
        public CoinShortName FirstCoin { get; set; }
        public CoinShortName SecondCoin { get; set; }

        public Pair(CoinShortName firstCoin, CoinShortName secondCoin)
        {
            FirstCoin = firstCoin;
            SecondCoin = secondCoin;
        }

        public string GetAttribute(string separator = "", bool IsLowerSymbols = false)
        {
            if (IsLowerSymbols)
            {
                return $"{FirstCoin}{separator}{SecondCoin}".ToLower();
            }
            else
            {
                return $"{FirstCoin}{separator}{SecondCoin}".ToUpper();
            }
        }
    }
}
