using OrdersService.API.Middleware;
using OrdersService.BusinessLogicLayer;
using OrdersService.BusinessLogicLayer.HttpClients;
using OrdersService.DataAccessLayer;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddBusinessLogicLayer();
builder.Services.AddDataAccessLayer(builder.Configuration);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.WithOrigins("http://localhost:4200") // Angular app
        .AllowAnyMethod().AllowAnyHeader();
    });
});

builder.Services.AddHttpClient<UsersMicroserviceClient>(client =>
{  
    client.BaseAddress = new Uri($"http://{builder.Configuration["UsersMicroserviceName"]}:" +
        $"{builder.Configuration["UsersMicroservicePort"]}");
        // we can also read env variables with Environment.GetEnvironmentVariable
});
builder.Services.AddHttpClient<ProductsMicroserviceClient>(client =>
{
    client.BaseAddress = new Uri($"http://{builder.Configuration["ProductsMicroserviceName"]}:" +
        $"{builder.Configuration["ProductsMicroservicePort"]}");
});

var app = builder.Build();
app.UseExceptionHandlingMiddleware();

app.UseCors();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
