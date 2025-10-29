using RepositoryContracts;
using ServiceContracts;
using ServiceContracts.DTO;
using Services.Helpers;
using StocksApp.Entities;

namespace Services
{
    public class StocksService : IStocksService
    {
        private readonly IStocksRepository _stocksRepository;

        public StocksService(IStocksRepository stocksRepository)
        {
            _stocksRepository = stocksRepository;
        }

        public async Task<BuyOrderResponse> CreateBuyOrder(BuyOrderRequest? buyOrderRequest)
        {
            ArgumentNullException.ThrowIfNull(buyOrderRequest);

            ValidationHelper.ModelValidation(buyOrderRequest);

            BuyOrder buyOrder = buyOrderRequest.ToBuyOrder();

            buyOrder.Id = Guid.NewGuid();

            return (await _stocksRepository.CreateBuyOrder(buyOrder)).ToBuyOrderResponse();
        }

        public async Task<SellOrderResponse> CreateSellOrder(SellOrderRequest? sellOrderRequest)
        {
            ArgumentNullException.ThrowIfNull(sellOrderRequest);

            ValidationHelper.ModelValidation(sellOrderRequest);

            SellOrder sellOrder = sellOrderRequest.ToSellOrder();

            sellOrder.Id = Guid.NewGuid();

            return (await _stocksRepository.CreateSellOrder(sellOrder)).ToSellOrderResponse();
        }

        public async Task<List<BuyOrderResponse>> GetBuyOrders()
        {
            var buyOrders = await _stocksRepository.GetBuyOrders();

            return buyOrders.Select(o => o.ToBuyOrderResponse()).ToList();
        }

        public async Task<List<SellOrderResponse>> GetSellOrders()
        {
            var sellOrders = await _stocksRepository.GetSellOrders();

            return sellOrders.Select(o => o.ToSellOrderResponse()).ToList();
        }
    }
}
