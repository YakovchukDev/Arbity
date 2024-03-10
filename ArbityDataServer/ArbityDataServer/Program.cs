using ArbityDataServer.Credentials;
using ArbityDataServer.Credentials.Entities;
using ArbityDataServer.Entities;
using ArbityDataServer.Entities.Enums;
using ArbityDataServer.ExchangerClients;


BourseCredentials credentials = new BourseCredentials();
List<Thread> BourseClients = new List<Thread>();


CredentialsEntry binanceCredentials = credentials.GetCredentials(Bourse.Binance);
if (binanceCredentials != null && binanceCredentials.IsNotNullOrEmpty())
{
    BinanceClient binanceClient = new BinanceClient(binanceCredentials);
    BourseClients.Add(new Thread(() => binanceClient.Initialize(new List<Pair>() { new Pair(CoinShortName.BTC, CoinShortName.USDT) })));
    binanceClient.Loaded += () => { binanceClient.Start(); };
}

foreach (Thread client in BourseClients)
{
    client.Start();
}

Console.ReadLine();




