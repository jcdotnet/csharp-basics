using ServiceContracts;
using ServiceContracts.DTO;
using Services.Helpers;
using StocksApp.Entities;

namespace Services
{
    public class StocksService : IStocksService
    {
        private readonly List<BuyOrder> _buyOrders;
        private readonly List<SellOrder> _sellOrders;

        public StocksService()
        {
            _buyOrders = [];
            _sellOrders = [];
        }

        public BuyOrderResponse CreateBuyOrder(BuyOrderRequest? buyOrderRequest)
        {
            ArgumentNullException.ThrowIfNull(buyOrderRequest);

            ValidationHelper.ModelValidation(buyOrderRequest);

            BuyOrder buyOrder = buyOrderRequest.ToBuyOrder();

            buyOrder.Id = Guid.NewGuid();

            _buyOrders.Add(buyOrder);

            return buyOrder.ToBuyOrderResponse();
        }

        public SellOrderResponse CreateSellOrder(SellOrderRequest? sellOrderRequest)
        {
            ArgumentNullException.ThrowIfNull(sellOrderRequest);

            ValidationHelper.ModelValidation(sellOrderRequest);

            SellOrder sellOrder = sellOrderRequest.ToSellOrder();

            //generate SellOrderID
            sellOrder.SellOrderID = Guid.NewGuid();

            //add sell order object to sell orders list
            _sellOrders.Add(sellOrder);

            //convert the SellOrder object into SellOrderResponse type
            return sellOrder.ToSellOrderResponse();
        }

        public List<BuyOrderResponse> GetBuyOrders()
        {
            // return _buyOrders.Order().Select().ToList();
            return [.. _buyOrders
                .OrderByDescending(temp => temp.OrderDate)
                .Select(temp => temp.ToBuyOrderResponse())]; 
        }

        public List<SellOrderResponse> GetSellOrders()
        {
            return [.. _sellOrders
                .OrderByDescending(temp => temp.OrderDate)
                .Select(temp => temp.ToSellOrderResponse())];
        }
    }
}
