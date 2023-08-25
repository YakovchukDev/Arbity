namespace ArbityDataServer.Entities
{
    class Book
    {
        public decimal Price { get; private set; }
        public decimal Quantity { get; private set; }

        public Book(decimal price, decimal quantity)
        {
            Price = price;
            Quantity = quantity;
        }
    }
}
