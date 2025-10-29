using Entities;
using Microsoft.EntityFrameworkCore;
using ServiceContracts;
using ServiceContracts.DTO;
using Services.Helpers;
using StocksApp.Entities;

namespace Services
{
    public class StocksService : IStocksService
    {
        //private readonly List<BuyOrder> _buyOrders;
        //private readonly List<SellOrder> _sellOrders;
        private readonly StockMarketDbContext _db;

        public StocksService(StockMarketDbContext db)
        {
            _db = db;
            //_buyOrders = [];
            //_sellOrders = [];
        }

        public async Task<BuyOrderResponse> CreateBuyOrder(BuyOrderRequest? buyOrderRequest)
        {
            ArgumentNullException.ThrowIfNull(buyOrderRequest);

            ValidationHelper.ModelValidation(buyOrderRequest);

            BuyOrder buyOrder = buyOrderRequest.ToBuyOrder();

            buyOrder.Id = Guid.NewGuid();

            //_buyOrders.Add(buyOrder);
            
            _db.BuyOrders.Add(buyOrder);
            await _db.SaveChangesAsync();
            return buyOrder.ToBuyOrderResponse();
        }

        public async Task<SellOrderResponse> CreateSellOrder(SellOrderRequest? sellOrderRequest)
        {
            ArgumentNullException.ThrowIfNull(sellOrderRequest);

            ValidationHelper.ModelValidation(sellOrderRequest);

            SellOrder sellOrder = sellOrderRequest.ToSellOrder();

            //generate SellOrderID
            sellOrder.SellOrderID = Guid.NewGuid();

            //_sellOrders.Add(sellOrder);
            _db.SellOrders.Add(sellOrder);
            await _db.SaveChangesAsync();

            return sellOrder.ToSellOrderResponse();
        }

        public async Task<List<BuyOrderResponse>> GetBuyOrders()
        {
            List<BuyOrder> buyOrders = await _db.BuyOrders
                .OrderByDescending(o => o.OrderDate).ToListAsync();
            return buyOrders.Select(o => o.ToBuyOrderResponse()).ToList();
        }

        public async Task<List<SellOrderResponse>> GetSellOrders()
        {
            List<SellOrder> sellOrders = await _db.SellOrders
                .OrderByDescending(o => o.OrderDate).ToListAsync();

            return sellOrders.Select(o => o.ToSellOrderResponse()).ToList();
        }
    }
}
