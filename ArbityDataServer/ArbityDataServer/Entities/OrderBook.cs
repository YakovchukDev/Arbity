namespace ArbityDataServer.Entities
{
    class OrderBook
    {
        public List<Book> Bids { get; private set; }
        public List<Book> Asks { get; private set; }

        //custom constructors for exchanges
    }
}
