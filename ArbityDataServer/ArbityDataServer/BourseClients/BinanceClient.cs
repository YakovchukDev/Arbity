using ArbityDataServer.Entities.Enums;
using ArbityDataServer.Entities;
using ArbityDataServer.Credentials.Entities;
using ArbityDataServer.LoggingService;
using ArbityDataServer.Entities.Analysis;
using Binance.Net.SymbolOrderBooks;
using Binance.Net.Clients;
using Binance.Net.Enums;
using CryptoExchange.Net.Interfaces;
using Kline = ArbityDataServer.Entities.Kline;

namespace ArbityDataServer.ExchangerClients
{
    public class BinanceClient
    {
        public event Action Loaded;

        public Bourse Info => Bourse.Binance;

        private BinanceSocketClient _client = new BinanceSocketClient();
        //private InternalArbitrageFinder _internalArbitrageFinder = new InternalArbitrageFinder();
        private Dictionary<Pair, BinanceSpotSymbolOrderBook> _orderBooks = new Dictionary<Pair, BinanceSpotSymbolOrderBook>();
        private Dictionary<Pair, Kline> _pairKlines = new Dictionary<Pair, Kline>();
        private CredentialsEntry Credentials { get; set; }
        private List<TradingPair> TradingPairs = new List<TradingPair>();
        private object lockObject = new object();

        public BinanceClient(CredentialsEntry credentials) 
        {

            Credentials = credentials;
        }

        ~BinanceClient()
        {
            Dispose();
            Logger.Info($"{Info}: Closed");
        }

        public async void Initialize(List<Pair> pairlist)
        {
            int totalCount = pairlist.Count;
            int count = 0;
            foreach(Pair pair in pairlist) 
            {
                if(await SubscribeToPair(pair))
                {
                    count++;
                }
            }
            InitializeTradingPairList();

            Logger.Success($"{Info}: Loaded");
            Loaded?.Invoke();
        }

        public async void Start()
        {
            Logger.Success($"{Info}: server strart!");
            do
            {
                DateTime currentTime = DateTime.Now;
                DateTime targetTime = new DateTime(currentTime.Year, currentTime.Month, currentTime.Day, currentTime.Hour, currentTime.AddMinutes(1).Minute, 0, 0);
                TimeSpan delay = targetTime - currentTime;
                if (delay.TotalMilliseconds > 0)
                {
                    await Task.Delay(delay);
                }
                UpdateTradingData();

                //вивід на консоль
                foreach (TradingPair pair in TradingPairs)
                {
                    pair.CalculateAll();
                    Console.WriteLine(pair.Pair.GetAttribute());
                    Console.WriteLine($"{pair.Bids.DataType.ToString()}:\n\tAVG price: {Math.Round(pair.Bids.AVGPrice, 2)}\n\tVolume: {Math.Round(pair.Bids.QuoteVolume, 2)}$/{pair.Bids.Volume}\n\tVolume1m:{pair.Bids.GetKline1m().Bids}");
                    Console.WriteLine($"{pair.Asks.DataType.ToString()}:\n\tAVG price: {Math.Round(pair.Asks.AVGPrice, 2)}\n\tVolume: {Math.Round(pair.Asks.QuoteVolume, 2)}$/{pair.Asks.Volume}\n\tVolume1m:{pair.Asks.GetKline1m().Asks}");
                }
                //аналіз даних

            } while (true);
        }

        public List<TradingPair> GetActualTradingPairs()
        {
            lock(lockObject)
            {
                return TradingPairs;
            }
        }

        private async Task<bool> SubscribeToPair(Pair pair)
        {
            var book = new BinanceSpotSymbolOrderBook(pair.GetAttribute());
            var startResult = book.StartAsync().Result;
            if (startResult.Success)
            {
                if (await SubscribeToKline(pair))
                {
                    _orderBooks.Add(pair, book);
                    Logger.Success($"{Info}: {pair.GetAttribute()}, could connect");
                    return true;
                }
                else
                {
                    Logger.Error($"{Info}: {pair.GetAttribute()}, kline could not connect");
                    return false;
                }
            }
            else
            {
                Logger.Error($"{Info}: {pair.GetAttribute()}, сould not connect");
                return false;
            }
        }

        private async Task<bool> SubscribeToKline(Pair pair, KlineInterval klineInterval = KlineInterval.OneMinute)
        {
            bool isSuccess = false;
            TaskCompletionSource<bool> loaded = new TaskCompletionSource<bool>();

            decimal asks = 0;
            decimal bids = 0;
            DateTime lastUpdate = DateTime.Now;
            await _client.SpotApi.ExchangeData.SubscribeToTradeUpdatesAsync("BTCUSDT", data =>
            {
                TimeSpan delay = DateTime.Now - lastUpdate;
                if (delay.TotalSeconds >= 60)
                {
                    if (_pairKlines.ContainsKey(pair))
                    {
                        _pairKlines[pair] = new Kline(asks, bids);
                    }
                    else
                    {
                        _pairKlines.Add(pair, new Kline(asks, bids));
                    }
                    bids = 0;
                    asks = 0;
                    lastUpdate = DateTime.Now;
                    isSuccess = true;
                    loaded.TrySetResult(true);
                }
                if (data.Data.BuyerIsMaker)
                {
                    bids += data.Data.Quantity;
                }
                else
                {
                    asks += data.Data.Quantity;
                }
            });

            await loaded.Task;
            return isSuccess;
        }

        private void UpdateTradingData()
        {
            foreach (TradingPair tradingPair in TradingPairs)
            {
                if (_orderBooks.ContainsKey(tradingPair.Pair))
                {
                    List<Book> bids = ToBooks(_orderBooks[tradingPair.Pair].Bids);
                    List<Book> asks = ToBooks(_orderBooks[tradingPair.Pair].Asks);
                    tradingPair.Asks.ChangeOrderBook(_pairKlines[tradingPair.Pair], asks);
                    tradingPair.Bids.ChangeOrderBook(_pairKlines[tradingPair.Pair], bids);
                }
                else
                {
                    Logger.Error($"{Info}: {tradingPair.Pair.GetAttribute()} pair not found");
                }
            }
            Logger.Success($"{Info}: pairs data updated!");
        }

        private void InitializeTradingPairList()
        {
            foreach (var orderBook in _orderBooks)
            {
                List<Book> bids = ToBooks(orderBook.Value.Bids);
                List<Book> asks = ToBooks(orderBook.Value.Asks);
                TradingPairs.Add(new TradingPair(orderBook.Key, new VolumeData(_pairKlines[orderBook.Key], bids, VolumeDataType.Bids), new VolumeData(_pairKlines[orderBook.Key], asks, VolumeDataType.Asks)));
            }
        }

        private List<Book> ToBooks(IEnumerable<ISymbolOrderBookEntry> list)
        {
            List<Book> books = new List<Book>();
            foreach (ISymbolOrderBookEntry entry in list)
            {
                books.Add(new Book(entry.Price, entry.Quantity));
            }
            return books;
        }

        private async void Dispose()
        {
            await _client.UnsubscribeAllAsync();
            GC.SuppressFinalize(this);
            Logger.Success($"{Info} stopped its work");
        }
    }
}
