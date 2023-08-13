namespace ArbityDataServer.Entities
{
    class Book
    {
        public decimal Price { get; set; }
        public decimal Quantity { get; set; }

        public Book(decimal price, decimal quantity)
        {
            Price = price;
            Quantity = quantity;
        }
    }
}
