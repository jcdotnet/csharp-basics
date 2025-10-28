using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using ServiceContracts;
using ServiceContracts.DTO;
using Services;
using StocksApp.Models;

namespace StocksApp.Controllers
{
    [Route("[controller]")]
    public class TradeController : Controller
    {
        private readonly IConfiguration _configuration; // in order to get the finnhub token
        // IOptions: structured, strongly typed configuration binding for predictable values
        // Iconfiguration: direct & dynamic access to settings at runtime (we use both here)
        private readonly IOptions<TradingOptions> _tradingOptions;
        private readonly IFinnhubService _finnhubService;
        private readonly IStocksService _stocksService;

        public TradeController(IFinnhubService finnhubService, IStocksService stocksService,
            IConfiguration configuration, IOptions<TradingOptions> options)
        {
            _finnhubService = finnhubService;
            _stocksService = stocksService;
            _configuration = configuration;
            _tradingOptions = options;
        }

        [Route("/")]
        [Route("[action]")]
        [Route("~/[controller]")]
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
                Price = Convert.ToDouble(stockQuoteDictionary?["c"].ToString()),
                Quantity = _tradingOptions.Value.DefaultOrderQuantity ?? 0
            };
            ViewBag.FinnhubToken = _configuration["FinnhubToken"];

            return View(stockTrade);
        }

        [Route("[action]")]
        public IActionResult Orders()
        {
            List<BuyOrderResponse> buyOrderResponses = _stocksService.GetBuyOrders();
            List<SellOrderResponse> sellOrderResponses = _stocksService.GetSellOrders();

            Orders orders = new() { 
                BuyOrders = buyOrderResponses, 
                SellOrders = sellOrderResponses
            };
            return View(orders);
        }

        [Route("[action]")]
        [HttpPost]
        public IActionResult BuyOrder(BuyOrderRequest buyOrderRequest)
        {
            buyOrderRequest.OrderDate = DateTime.Now;
            ModelState.Clear();
            TryValidateModel(buyOrderRequest);

            if (!ModelState.IsValid)
            {
                ViewBag.Errors = ModelState.Values.SelectMany(v => 
                    v.Errors).Select(e => e.ErrorMessage).ToList();
                StockTrade stockTrade = new() { 
                    StockName = buyOrderRequest.StockName,
                    Price = buyOrderRequest.Price,
                    Quantity = buyOrderRequest.Quantity, 
                    StockSymbol = buyOrderRequest.StockSymbol 
                };
                return View("Index", stockTrade);
            }

            BuyOrderResponse buyOrderResponse = _stocksService.CreateBuyOrder(buyOrderRequest);

            return RedirectToAction(nameof(Orders));
        }


        [Route("[action]")]
        [HttpPost]
        public IActionResult SellOrder(SellOrderRequest sellOrderRequest)
        {
            sellOrderRequest.OrderDate = DateTime.Now;
            ModelState.Clear();
            TryValidateModel(sellOrderRequest);

            if (!ModelState.IsValid)
            {
                ViewBag.Errors = ModelState.Values.SelectMany(v =>
                    v.Errors).Select(e => e.ErrorMessage).ToList();
                StockTrade stockTrade = new() { 
                    StockName = sellOrderRequest.StockName,
                    Price = sellOrderRequest.Price,
                    Quantity = sellOrderRequest.Quantity, 
                    StockSymbol = sellOrderRequest.StockSymbol 
                };
                return View("Index", stockTrade);
            }

            SellOrderResponse sellOrderResponse = _stocksService.CreateSellOrder(sellOrderRequest);

            return RedirectToAction(nameof(Orders));
        }
    }
}
