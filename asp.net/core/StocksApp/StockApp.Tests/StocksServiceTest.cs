using AutoFixture;
using Entities;
using Microsoft.EntityFrameworkCore;
using Moq;
using RepositoryContracts;
using ServiceContracts;
using ServiceContracts.DTO;
using Services;
using StocksApp.Entities;

namespace StockApp.Tests
{
    public class StocksServiceTest
    {
        private readonly IStocksService _service;

        private readonly Mock<IStocksRepository> _stocksRepositoryMock;
        private readonly IStocksRepository _stocksRepository;

        private readonly IFixture _fixture;

        #region constructor
        public StocksServiceTest()
        {
            _fixture = new Fixture();

            _stocksRepositoryMock = new Mock<IStocksRepository>();
            _stocksRepository = _stocksRepositoryMock.Object;

            _service = new StocksService(_stocksRepository);
        }
        #endregion

        #region CreateBuyOrder

        // requirement: When you supply BuyOrderRequest as null, it should throw ArgumentNullException
        [Fact]
        public async Task CreateBuyOrder_Null_ToThrowArgumentNullException()
        {
            // Arrange
            BuyOrderRequest? buyOrderRequest = null;

            // Assert
            await Assert.ThrowsAsync<ArgumentNullException>(async () =>
            {
                // Act
                await _service.CreateBuyOrder(buyOrderRequest);
            });
        }

        // requirement: When you supply buyOrderQuantity as 0 (as per the specification, minimum is 1),
        // it should throw ArgumentException
        [Fact]
        public async Task CreateBuyOrder_QuantityIsZero_ToThrowArgumentException()
        {
            // Arrange
            BuyOrderRequest? buyOrderRequest = _fixture.Build<BuyOrderRequest>()
               .With(temp => temp.Quantity, (uint)0)
               .Create();

            // Mock
            BuyOrder buyOrder = buyOrderRequest.ToBuyOrder();
            _stocksRepositoryMock.Setup(temp => temp.CreateBuyOrder(It.IsAny<BuyOrder>())).ReturnsAsync(buyOrder);

            // Assert
            await Assert.ThrowsAsync<ArgumentException>(async() =>
            {
                // Act
                await _service.CreateBuyOrder(buyOrderRequest);
            });
        }

        // requirement: When you supply buyOrderQuantity as 100001 (as per the specification,
        // maximum is 100000), it should throw ArgumentException.
        [Fact]
        public async Task CreateBuyOrder_QuantityGreaterThanMaximum_ToThrowArgumentException()
        {
            // Arrange
            BuyOrderRequest? buyOrderRequest = _fixture.Build<BuyOrderRequest>()
              .With(temp => temp.Quantity, (uint)100001)
              .Create();

            // Mock
            BuyOrder buyOrder = buyOrderRequest.ToBuyOrder();
            _stocksRepositoryMock.Setup(temp => temp.CreateBuyOrder(It.IsAny<BuyOrder>())).ReturnsAsync(buyOrder);

            // Assert
            await Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                // Act
                await _service.CreateBuyOrder(buyOrderRequest);
            });
        }

        // requirement: When you supply buyOrderPrice as 0 (as per the specification, minimum is 1),
        // it should throw ArgumentException.
        [Fact]
        public async Task CreateBuyOrder_PriceIsZero_ToThrowArgumentException()
        {
            // Arrange
            BuyOrderRequest? buyOrderRequest = _fixture.Build<BuyOrderRequest>()
              .With(temp => temp.Price, 0)
              .Create();

            // Mock
            BuyOrder buyOrder = buyOrderRequest.ToBuyOrder();
            _stocksRepositoryMock.Setup(temp => temp.CreateBuyOrder(It.IsAny<BuyOrder>())).ReturnsAsync(buyOrder);

            // Assert
            await Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                // Act
                await _service.CreateBuyOrder(buyOrderRequest);
            });
        }

        // requirement: When you supply buyOrderPrice as 60001 (as per the specification,
        // maximum is 60000), it should throw ArgumentException.
        [Fact]
        public async Task CreateBuyOrder_PriceGreaterThanMaximum_ToThrowArgumentException()
        {
            // Arrange
            BuyOrderRequest? buyOrderRequest = _fixture.Build<BuyOrderRequest>()
              .With(temp => temp.Price, 60001)
              .Create();

            // Mock
            BuyOrder buyOrder = buyOrderRequest.ToBuyOrder();
            _stocksRepositoryMock.Setup(temp => temp.CreateBuyOrder(It.IsAny<BuyOrder>())).ReturnsAsync(buyOrder);

            // Assert
            await Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                // Act
                await _service.CreateBuyOrder(buyOrderRequest);
            });
        }

        // requirement: When you supply stock symbol=null (as per the specification,
        // stock symbol can't be null), it should throw ArgumentException.
        [Fact]
        public async Task CreateBuyOrder_StockSymbolNull_ToThrowArgumentException()
        {
            // Arrange
            BuyOrderRequest? buyOrderRequest = _fixture.Build<BuyOrderRequest>()
              .With(temp => temp.StockSymbol, null as string)
              .Create();

            // Mock
            BuyOrder buyOrder = buyOrderRequest.ToBuyOrder();
            _stocksRepositoryMock.Setup(temp => temp.CreateBuyOrder(It.IsAny<BuyOrder>())).ReturnsAsync(buyOrder);

            // Assert
            await Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                // Act
                await _service.CreateBuyOrder(buyOrderRequest);
            });
        }

        // requirement: When you supply orderDate as "1999-12-31" (YYYY-MM-DD) - (as per the specification,
        // it should be equal or newer date than 2000-01-01), it should throw ArgumentException.
        [Fact]
        public async Task CreateBuyOrder_DateOlderThanMinimum_ToThrowArgumentException()
        {
            // Arrange
            BuyOrderRequest? buyOrderRequest = _fixture.Build<BuyOrderRequest>()
              .With(temp => temp.OrderDate, new DateTime(1999, 12, 31)) // Convert.ToDateTime("1999-12-31")
              .Create();

            // Mock
            BuyOrder buyOrder = buyOrderRequest.ToBuyOrder();
            _stocksRepositoryMock.Setup(temp => temp.CreateBuyOrder(It.IsAny<BuyOrder>())).ReturnsAsync(buyOrder);
          
            // Assert
            await Assert.ThrowsAsync<ArgumentException>( async () =>
            {
                // Act
                await _service.CreateBuyOrder(buyOrderRequest);
            });
        }

        // requirement: If you supply all valid values, it should be successful and return
        // an object of BuyOrderResponse type with auto-generated BuyOrderID(guid)
        [Fact]
        public async Task CreateBuyOrder_Valid_ToBeSuccessful()
        {
            // Arrange
            BuyOrderRequest? buyOrderRequest = _fixture.Build<BuyOrderRequest>().Create();

            //Mock
            BuyOrder buyOrder = buyOrderRequest.ToBuyOrder();
            _stocksRepositoryMock.Setup(temp => temp.CreateBuyOrder(It.IsAny<BuyOrder>())).ReturnsAsync(buyOrder);

            // Act
            BuyOrderResponse response = await _service.CreateBuyOrder(buyOrderRequest);
            buyOrder.Id = response.Id;
            BuyOrderResponse expected = buyOrder.ToBuyOrderResponse();

            // Assert
            Assert.Equal(expected, response);
        }

        #endregion

        #region CreateSellOrder

        // same requirements for CreateSellOrder // to mock repository
        [Fact]
        public async Task CreatSellyOrder_Null_ToThrowArgumentNullException()
        {
            // Arrange
            SellOrderRequest? sellOrderRequest = null;

            // Assert
            await Assert.ThrowsAsync<ArgumentNullException>(async () =>
            {
                // Act
                await _service.CreateSellOrder(sellOrderRequest);
            });
        }

        [Fact]
        public async Task CreatSellOrder_QuantityIsZero_ToThrowArgumentException()
        {
            // Arrange
            SellOrderRequest? sellOrderRequest = new SellOrderRequest()
            {
                StockName = "Microsoft",
                StockSymbol = "MSFT",
                Quantity = 0,
                OrderDate = DateTime.Now,
                Price = 1000,
            };

            // Assert
            await Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                // Act
                await _service.CreateSellOrder(sellOrderRequest);
            });
        }

        [Fact]
        public async Task CreateSellOrder_QuantityGreaterThanMaximum_ToThrowArgumentException()
        {
            // Arrange
            SellOrderRequest? sellOrderRequest = new SellOrderRequest()
            {
                StockName = "Microsoft",
                StockSymbol = "MSFT",
                Quantity = 100001,
                OrderDate = DateTime.Now,
                Price = 1000,
            };

            // Assert
            await Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                // Act
                await _service.CreateSellOrder(sellOrderRequest);
            });
        }

        [Fact]
        public async Task CreateSellOrder_PriceIsZero_ToThrowArgumentException()
        {
            // Arrange
            SellOrderRequest? sellOrderRequest = new SellOrderRequest()
            {
                StockName = "Microsoft",
                StockSymbol = "MSFT",
                Quantity = 1000,
                OrderDate = DateTime.Now,
                Price = 0,
            };

            // Assert
            await Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                // Act
                await _service.CreateSellOrder(sellOrderRequest);
            });
        }

        [Fact]
        public async Task CreateSellOrder_PriceGreaterThanMaximum_ToThrowArgumentException()
        {
            // Arrange
            SellOrderRequest? sellOrderRequest = new SellOrderRequest()
            {
                StockName = "Microsoft",
                StockSymbol = "MSFT",
                Quantity = 1000,
                OrderDate = DateTime.Now,
                Price = 60001,
            };

            // Assert
            await Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                // Act
                await _service.CreateSellOrder(sellOrderRequest);
            });
        }

        [Fact]
        public async Task CreateSellOrder_StockSymbolNull_ToThrowArgumentException()
        {
            // Arrange
            SellOrderRequest? sellOrderRequest = new SellOrderRequest()
            {
                StockName = "Microsoft",
                StockSymbol = null!,
                Quantity = 1000,
                OrderDate = DateTime.Now,
                Price = 1000,
            };

            // Assert
            await Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                // Act
                await _service.CreateSellOrder(sellOrderRequest);
            });
        }

        [Fact]
        public async Task CreateSellOrder_DateOlderThanMinimum_ToThrowArgumentException()
        {
            // Arrange
            SellOrderRequest? sellOrderRequest = new SellOrderRequest()
            {
                StockName = "Microsoft",
                StockSymbol = "MSFT",
                Quantity = 1000,
                OrderDate = new DateTime(1999, 12, 31), // Convert.ToDateTime("1999-12-31")
                Price = 1000,
            };

            // Assert
            await Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                // Act
                await _service.CreateSellOrder(sellOrderRequest);
            });
        }

        [Fact]
        public async Task CreateSellOrder_Valid_ToBeSuccessful()
        {
            // Arrange
            SellOrderRequest? sellOrderRequest = _fixture.Build<SellOrderRequest>().Create();

            // Mock
            SellOrder sellOrder = sellOrderRequest.ToSellOrder();
            _stocksRepositoryMock.Setup(temp => temp.CreateSellOrder(It.IsAny<SellOrder>())).ReturnsAsync(sellOrder);

            // Act
            SellOrderResponse response = await _service.CreateSellOrder(sellOrderRequest);

            // Assert
            sellOrder.Id = response.Id;
            var expectedResponse = sellOrder.ToSellOrderResponse();
            Assert.Equal(expectedResponse, response);        
        }

        #endregion

        #region GetBuyOrders

        // requirement: When you invoke this method, by default, the returned list should be empty.
        [Fact]
        public async Task GetBuyOrders_Default_ToBeEmpty()
        {
            // Arrange
            List<BuyOrder> buyOrders = [];

            //Mock
            _stocksRepositoryMock.Setup(temp => temp.GetBuyOrders()).ReturnsAsync(buyOrders);

            // Act
            var response = await _service.GetBuyOrders();

            // Assert
            Assert.Empty(response);
        }

        // requirement: When you first add few buy orders using CreateBuyOrder() method;
        // and then invoke GetBuyOrders() method; the returned list should contain all the same buy orders
        [Fact]
        public async Task GetBuyOrders_AfterCreateBuyOrderCall_ToBeSuccessful()
        {
            // Arrange
            List<BuyOrder> list = [
                _fixture.Build<BuyOrder>().Create(),
                _fixture.Build<BuyOrder>().Create()
            ];

            List<BuyOrderResponse> expectedList = list.Select(temp => temp.ToBuyOrderResponse()).ToList();

            //Mock
            _stocksRepositoryMock.Setup(temp => temp.GetBuyOrders()).ReturnsAsync(list);

            // Act
            var responseList = await _service.GetBuyOrders();

            // Assert
            Assert.Equivalent(expectedList, responseList);
        }

        #endregion

        #region GetSellOrders

        // same requirements 
        [Fact]
        public async Task GetSellOrders_Default_ToBeEmpty()
        {
            // Arrange
            List<SellOrder> sellOrders = [];

            //Mock
            _stocksRepositoryMock.Setup(temp => temp.GetSellOrders()).ReturnsAsync(sellOrders);

            // Act
            var response = await _service.GetSellOrders();

            // Assert
            Assert.Empty(response);
        }

        // requirement: When you first add few buy orders using CreateBuyOrder() method;
        // and then invoke GetAllBuyOrders() method; the returned list should contain all the same buy orders
        [Fact]
        public async Task GetSellOrders_AfterCreateSellOrderCall_ToBeSuccessful()
        {
            // Arrange
            List<SellOrder> list = [
                _fixture.Build<SellOrder>().Create(),
                _fixture.Build<SellOrder>().Create()
            ];

            List<SellOrderResponse> expectedList = list.Select(temp => temp.ToSellOrderResponse()).ToList();

            // Mock
            _stocksRepositoryMock.Setup(temp => temp.GetSellOrders()).ReturnsAsync(list);

            // Act
            var responseList = await _service.GetSellOrders();

            // Assert
            Assert.Equivalent(expectedList, responseList);
        }

        #endregion
    }
}
