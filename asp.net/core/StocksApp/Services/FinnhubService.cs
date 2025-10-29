using RepositoryContracts;
using ServiceContracts;

namespace Services
{
    public class FinnhubService : IFinnhubService
    {
        // dependencies
        private readonly IFinnhubRepository _finnhubRepository;

        // Dependency Injection (constructor injection)
        public FinnhubService(IFinnhubRepository finnhubRepository)
        {
            _finnhubRepository = finnhubRepository;
        }

        public async Task<Dictionary<string, object>>? GetCompanyProfile(string stockSymbol)
        {
            return await _finnhubRepository.GetCompanyProfile(stockSymbol);
        }

        public async Task<Dictionary<string, object>>? GetStockPriceQuote(string stockSymbol)
        {
            return await _finnhubRepository.GetStockPriceQuote(stockSymbol);  
        }

        public async Task<List<Dictionary<string, string>>?> GetStocks()
        {
            return await _finnhubRepository.GetStocks();
        }

        public async Task<Dictionary<string, object>?> SearchStocks(string stockSymbolToSearch)
        {
            return await _finnhubRepository.SearchStocks(stockSymbolToSearch);
        }
    }
}
