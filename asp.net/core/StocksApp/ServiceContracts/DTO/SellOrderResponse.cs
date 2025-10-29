using StocksApp.Entities;

namespace ServiceContracts.DTO
{
    public class SellOrderResponse : IOrderResponse
    {
        public Guid Id { get; set; }
        public string StockSymbol { get; set; } = string.Empty;

        public string StockName { get; set; } = string.Empty;

        public DateTime OrderDate { get; set; }

        public uint Quantity { get; set; }

        public double Price { get; set; }

        public double TradeAmount { get; set; }

        public OrderType TypeOfOrder => OrderType.SellOrder;

        public override bool Equals(object? obj)
        {
            if (obj == null) return false;
            if (obj is not SellOrderResponse) return false;

            SellOrderResponse other = (SellOrderResponse)obj;
            return Id == other.Id && StockSymbol == other.StockSymbol && StockName == other.StockName;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
    public static class SellOrderExtensions
    {
        /// <summary>
        /// An extension method to convert an object of SellOrder class into SellOrderResponse class
        /// </summary>
        /// <param name="sellOrder">The SellOrder object to convert</param>
        /// <returns>Returns the converted SellOrderResponse object</returns>
        public static SellOrderResponse ToSellOrderResponse(this SellOrder sellOrder)
        {
            return new SellOrderResponse() { 
                Id = sellOrder.SellOrderID, 
                StockSymbol = sellOrder.StockSymbol, 
                StockName = sellOrder.StockName, 
                Price = sellOrder.Price, 
                OrderDate = sellOrder.OrderDate, 
                Quantity = sellOrder.Quantity, 
                TradeAmount = sellOrder.Price * sellOrder.Quantity 
            };
        }
    }
}
