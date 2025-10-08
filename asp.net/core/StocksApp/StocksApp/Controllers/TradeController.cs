using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using ServiceContracts;
using StocksApp.Models;

namespace StocksApp.Controllers
{
    public class TradeController : Controller
    {
        // private readonly IConfiguration _configuration;
        private readonly IOptions<TradingOptions> _tradingOptions;
        private readonly IFinnhubService _finnhubService;
        
        public TradeController(IFinnhubService finnhubService, IOptions<TradingOptions> options)
        {
            _tradingOptions = options;
            _finnhubService = finnhubService;
        }

        [Route("/")]
        public IActionResult Index()
        {
            string? stockSymbol = _tradingOptions.Value.DefaultStockSymbol;
            if (string.IsNullOrEmpty(stockSymbol))
            {
                stockSymbol = "MSFT";
            }

            var companyProfielDictionary = _finnhubService.GetCompanyProfile(stockSymbol);           
            var stockQuoteDictionary = _finnhubService.GetStockPriceQuote(stockSymbol);

            StockTrade stockTrade = new StockTrade()
            {
                StockSymbol = stockSymbol,
                StockName = companyProfielDictionary?["name"].ToString(),
                Price = Convert.ToDouble(stockQuoteDictionary?["c"].ToString())
            };

            return View(stockTrade);
        }
    }
}
