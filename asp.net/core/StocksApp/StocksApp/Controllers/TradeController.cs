using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Rotativa.AspNetCore;
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

        [Route("[action]/{stockSymbol}")]
        [Route("~/[controller]/{stockSymbol}")]
        public async Task<IActionResult> Index(string stockSymbol)
        {
            if (string.IsNullOrEmpty(stockSymbol))
            {
                stockSymbol = "MSFT";
            }

            var companyProfielDictionary = await _finnhubService.GetCompanyProfile(stockSymbol);           
            var stockQuoteDictionary = await _finnhubService.GetStockPriceQuote(stockSymbol);

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
        public async Task<IActionResult> Orders()
        {
            var buyOrderResponses = await _stocksService.GetBuyOrders();
            var sellOrderResponses = await _stocksService.GetSellOrders();

            Orders orders = new() { 
                BuyOrders = buyOrderResponses, 
                SellOrders = sellOrderResponses
            };
            return View(orders);
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<IActionResult> BuyOrder(BuyOrderRequest buyOrderRequest)
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

            await _stocksService.CreateBuyOrder(buyOrderRequest);

            return RedirectToAction(nameof(Orders));
        }


        [Route("[action]")]
        [HttpPost]
        public async Task<IActionResult> SellOrder(SellOrderRequest sellOrderRequest)
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

            await _stocksService.CreateSellOrder(sellOrderRequest);

            return RedirectToAction(nameof(Orders));
        }


        [Route("[action]")]
        public async Task<IActionResult> OrdersPDF()
        {
            List<IOrderResponse> orders = [];
            orders.AddRange(await _stocksService.GetBuyOrders());
            orders.AddRange(await _stocksService.GetSellOrders());
            orders = orders.OrderByDescending(temp => temp.OrderDate).ToList();

            ViewBag.TradingOptions = _tradingOptions;

            return new ViewAsPdf("OrdersPDF", orders, ViewData)
            {
                PageMargins = new Rotativa.AspNetCore.Options.Margins() { 
                    Top = 20, Right = 20, Bottom = 20, Left = 20 
                },
                PageOrientation = Rotativa.AspNetCore.Options.Orientation.Landscape
            };
        }

    }
}
