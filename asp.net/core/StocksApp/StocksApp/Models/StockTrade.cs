namespace StocksApp.Models
{
    /// <summary>
    /// del class to supply trade details (stock id, stock name, price and quantity etc.) to the view
    /// </summary>
    public class StockTrade
    {
        public string? StockSymbol { get; set; }
        public string? StockName { get; set; }
        public double Price { get; set; } = 0;
        public uint Quantity { get; set; } = 0;

        public override string ToString()
        {
            return $"{StockName} ({StockSymbol}) {Price}";
        }
    }
}
