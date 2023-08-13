namespace ArbityDataServer.Entities
{
    class Volume
    {
        public decimal Price { get; private set; }
        public decimal AvailableVolume { get; private set; }
        public decimal TotalVolume24h { get; private set; }

        private decimal _modifier = 24 * 6; //10m

        public Volume(decimal totalVolume24h, List<Book> userOrders) 
        {
            TotalVolume24h = totalVolume24h;
            Calculate(userOrders);
        }

        private void Calculate(List<Book> userOrders)
        {
            List<decimal> quantites = new List<decimal>();
            decimal currentVolume = TotalVolume24h / _modifier;
            
            foreach (var order in userOrders)
            {
                if (AvailableVolume > currentVolume)
                {
                    break;
                }
                quantites.Add(order.Quantity);
                AvailableVolume += order.Quantity * order.Price;
            }

            Price = AvailableVolume / quantites.Sum();
        }

        public void Change(Volume volume)
        {
            Price = volume.Price;
            AvailableVolume = volume.AvailableVolume;
        }
    }
}
