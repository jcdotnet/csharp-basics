using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using ServiceContracts;
using StocksApp.Models;

namespace StocksApp.Controllers
{
    [Route("[controller]")]
    public class StocksController : Controller
    {
        private readonly TradingOptions _tradingOptions;
        private readonly IFinnhubService _finnhubService;

        public StocksController(IOptions<TradingOptions> tradingOptions, IFinnhubService finnhubService)
        {
            _tradingOptions = tradingOptions.Value;
            _finnhubService = finnhubService;
        }

        [Route("/")]
        [Route("[action]/{stock?}")]
        [Route("~/[action]/{stock?}")]
        public async Task<IActionResult> Explore(string? stock, bool showAll = false)
        {
            List<Dictionary<string, string>>? stocksDictionary = await _finnhubService.GetStocks();

            List<Stock> stocks = [];

            if (stocksDictionary is not null)
            {
                //filter stocks
                if (!showAll && _tradingOptions.Top25PopularStocks != null)
                {
                    string[]? Top25PopularStocksList = _tradingOptions.Top25PopularStocks.Split(',');
                    if (Top25PopularStocksList is not null)
                    {
                        stocksDictionary = [.. stocksDictionary.Where(s => 
                            Top25PopularStocksList.Contains(Convert.ToString(s["symbol"])))];
                    }
                }

                stocks = [.. stocksDictionary
                 .Select(temp => new Stock() { 
                     StockName = Convert.ToString(temp["description"]), 
                     StockSymbol = Convert.ToString(temp["symbol"]) })];
            }

            ViewBag.stock = stock;
            return View(stocks);
        }
    }
}
