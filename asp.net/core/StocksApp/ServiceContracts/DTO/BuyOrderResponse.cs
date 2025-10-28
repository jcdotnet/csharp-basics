using ServiceContracts.DTO;
using StocksApp.Entities;

namespace ServiceContracts.DTO
{
    public class BuyOrderResponse
    {
        public Guid Id { get; set; }
        public string StockSymbol { get; set; } = string.Empty;

        public string StockName { get; set; } = string.Empty;

        public DateTime OrderDate { get; set; }

        public uint Quantity { get; set; }

        public double Price { get; set; }

        public double TradeAmount { get; set; }

        public override bool Equals(object? obj)
        {
            if (obj == null) return false;
            if (obj is not BuyOrderResponse) return false;

            BuyOrderResponse other = (BuyOrderResponse)obj;
            return Id == other.Id && StockSymbol == other.StockSymbol && StockName == other.StockName;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
public static class BuyOrderExtensions
{
    public static BuyOrderResponse ToBuyOrderResponse(this BuyOrder buyOrder)
    {
        return new BuyOrderResponse() { 
            Id = buyOrder.Id, 
            StockSymbol = buyOrder.StockSymbol, 
            StockName = buyOrder.StockName, 
            Price = buyOrder.Price,
            OrderDate = buyOrder.OrderDate, 
            Quantity = buyOrder.Quantity, 
            TradeAmount = buyOrder.Price * buyOrder.Quantity 
        };
    }
}
