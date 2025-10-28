// assignment from the ASP.NET core course by Harsha Vardhan
using ServiceContracts;
using Services;
using StocksApp;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.Configure<TradingOptions>(builder.Configuration.GetSection("TradingOptions"));
builder.Services.AddTransient<IFinnhubService, FinnhubService>();
// since wew are using a in-memory collection (list) to save the orders
// we need a singleon service so the list values persist after requests
builder.Services.AddSingleton<IStocksService, StocksService>();
builder.Services.AddHttpClient();

var app = builder.Build();

app.UseStaticFiles();
app.MapControllers();

app.Run();
