using ArbityDataServer.Entities.Enums;

namespace ArbityDataServer.Entities
{
    public class VolumeData
    {
        public readonly VolumeDataType DataType;
        public decimal AVGPrice { get; private set; }
        public decimal Volume { get; private set; }
        public decimal QuoteVolume { get; private set; }
        private Kline _kline1m;
        private List<Book> _books;

        public VolumeData(Kline kline1m, List<Book> books, VolumeDataType volumeDataType) 
        {
            _kline1m = kline1m;
            _books = books;
            DataType = volumeDataType;
        }

        public Kline GetKline1m() => _kline1m;

        public void ChangeOrderBook(Kline kline1m, List<Book> books)
        {
            _kline1m = kline1m;
            _books = books;
        }

        public void Calculate()
        {
            List<decimal> quantites = new List<decimal>();
            decimal volume1m = _kline1m.GetVolume1m(DataType);
            QuoteVolume = 0;
            Volume = 0;
            AVGPrice = 0;
            foreach (var order in _books)
            {
                if (Volume > volume1m)
                {
                    break;
                }
                quantites.Add(order.Quantity);
                QuoteVolume += order.Quantity * order.Price;
                Volume += order.Quantity;
            }
            AVGPrice = QuoteVolume / quantites.Sum();
        }
    }
}
