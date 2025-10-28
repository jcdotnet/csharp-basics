using ServiceContracts;
using ServiceContracts.DTO;
using Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.ConstrainedExecution;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace StockApp.Tests
{
    public class StocksServiceTest
    {
        private readonly IStocksService _service;

        public StocksServiceTest()
        {
            _service = new StocksService();
        }

        #region CreateBuyOrder

        // requirement: When you supply BuyOrderRequest as null, it should throw ArgumentNullException
        [Fact]
        public void CreateBuyOrder_Null_ToThrowArgumentNullException()
        {
            // Arrange
            BuyOrderRequest? buyOrderRequest = null;

            // Assert
            Assert.Throws<ArgumentNullException>(() =>
            {
                // Act
                _service.CreateBuyOrder(buyOrderRequest);
            });
        }

        // requirement: When you supply buyOrderQuantity as 0 (as per the specification, minimum is 1),
        // it should throw ArgumentException
        [Fact]
        public void CreateBuyOrder_QuantityIsZero_ToThrowArgumentException()
        {
            // Arrange
            BuyOrderRequest? buyOrderRequest = new BuyOrderRequest()
            {
                StockName = "Microsoft",
                StockSymbol = "MSFT",
                Quantity = 0,
                OrderDate = DateTime.Now,
                Price = 1000,
            };

            // Assert
            Assert.Throws<ArgumentException>(() =>
            {
                // Act
                _service.CreateBuyOrder(buyOrderRequest);
            });
        }

        // requirement: When you supply buyOrderQuantity as 100001 (as per the specification,
        // maximum is 100000), it should throw ArgumentException.
        [Fact]
        public void CreateBuyOrder_QuantityGreaterThanMaximum_ToThrowArgumentException()
        {
            // Arrange
            BuyOrderRequest? buyOrderRequest = new BuyOrderRequest()
            {
                StockName = "Microsoft",
                StockSymbol = "MSFT",
                Quantity = 100001,
                OrderDate = DateTime.Now,
                Price = 1000,
            };

            // Assert
            Assert.Throws<ArgumentException>(() =>
            {
                // Act
                _service.CreateBuyOrder(buyOrderRequest);
            });
        }

        // requirement: When you supply buyOrderPrice as 0 (as per the specification, minimum is 1),
        // it should throw ArgumentException.
        [Fact]
        public void CreateBuyOrder_PriceIsZero_ToThrowArgumentException()
        {
            // Arrange
            BuyOrderRequest? buyOrderRequest = new BuyOrderRequest()
            {
                StockName = "Microsoft",
                StockSymbol = "MSFT",
                Quantity = 1000,
                OrderDate = DateTime.Now,
                Price = 0,
            };

            // Assert
            Assert.Throws<ArgumentException>(() =>
            {
                // Act
                _service.CreateBuyOrder(buyOrderRequest);
            });
        }

        // requirement: When you supply buyOrderPrice as 10001 (as per the specification,
        // maximum is 10000), it should throw ArgumentException.
        [Fact]
        public void CreateBuyOrder_PriceGreaterThanMaximum_ToThrowArgumentException()
        {
            // Arrange
            BuyOrderRequest? buyOrderRequest = new BuyOrderRequest()
            {
                StockName = "Microsoft",
                StockSymbol = "MSFT",
                Quantity = 1000,
                OrderDate = DateTime.Now,
                Price = 10001,
            };

            // Assert
            Assert.Throws<ArgumentException>(() =>
            {
                // Act
                _service.CreateBuyOrder(buyOrderRequest);
            });
        }

        // requirement: When you supply stock symbol=null (as per the specification,
        // stock symbol can't be null), it should throw ArgumentException.
        [Fact]
        public void CreateBuyOrder_StockSymbolNull_ToThrowArgumentException()
        {
            // Arrange
            BuyOrderRequest? buyOrderRequest = new BuyOrderRequest()
            {
                StockName = "Microsoft",
                StockSymbol = null!, 
                Quantity = 1000,
                OrderDate = DateTime.Now,
                Price = 1000,
            };

            // Assert
            Assert.Throws<ArgumentException>(() =>
            {
                // Act
                _service.CreateBuyOrder(buyOrderRequest);
            });
        }

        // requirement: When you supply orderDate as "1999-12-31" (YYYY-MM-DD) - (as per the specification,
        // it should be equal or newer date than 2000-01-01), it should throw ArgumentException.
        [Fact]
        public void CreateBuyOrder_DateOlderThanMinimum_ToThrowArgumentException()
        {
            // Arrange
            BuyOrderRequest? buyOrderRequest = new BuyOrderRequest()
            {
                StockName = "Microsoft",
                StockSymbol = "MSFT",
                Quantity = 1000,
                OrderDate = new DateTime(1999, 12, 31), // Convert.ToDateTime("1999-12-31")
                Price = 1000,
            };

            // Assert
            Assert.Throws<ArgumentException>(() =>
            {
                // Act
                _service.CreateBuyOrder(buyOrderRequest);
            });
        }

        // requirement: If you supply all valid values, it should be successful and return
        // an object of BuyOrderResponse type with auto-generated BuyOrderID(guid)
        [Fact]
        public void CreateBuyOrder_Valid_ToBeSuccessful()
        {
            // Arrange
            BuyOrderRequest? buyOrderRequest = new BuyOrderRequest()
            {
                StockName = "Microsoft",
                StockSymbol = "MSFT",
                Quantity = 1000,
                OrderDate = DateTime.Now,
                Price = 1000,
            };
            //BuyOrderResponse expected = buyOrderRequest.ToBuyOrder().ToBuyOrderResponse();

            // Act
            BuyOrderResponse response = _service.CreateBuyOrder(buyOrderRequest);
            //expected.Id = response.Id;

            // Assert
            Assert.NotEqual(Guid.Empty, response.Id);
        }

        #endregion

        #region CreateSellOrder

        // same requirements for CreateSellOrder
        [Fact]
        public void CreatSellyOrder_Null_ToThrowArgumentNullException()
        {
            // Arrange
            SellOrderRequest? sellOrderRequest = null;

            // Assert
            Assert.Throws<ArgumentNullException>(() =>
            {
                // Act
                _service.CreateSellOrder(sellOrderRequest);
            });
        }

        [Fact]
        public void CreatSellOrder_QuantityIsZero_ToThrowArgumentException()
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
            Assert.Throws<ArgumentException>(() =>
            {
                // Act
                _service.CreateSellOrder(sellOrderRequest);
            });
        }

        [Fact]
        public void CreateSellOrder_QuantityGreaterThanMaximum_ToThrowArgumentException()
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
            Assert.Throws<ArgumentException>(() =>
            {
                // Act
                _service.CreateSellOrder(sellOrderRequest);
            });
        }

        [Fact]
        public void CreateSellOrder_PriceIsZero_ToThrowArgumentException()
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
            Assert.Throws<ArgumentException>(() =>
            {
                // Act
                _service.CreateSellOrder(sellOrderRequest);
            });
        }

        [Fact]
        public void CreateSellOrder_PriceGreaterThanMaximum_ToThrowArgumentException()
        {
            // Arrange
            SellOrderRequest? sellOrderRequest = new SellOrderRequest()
            {
                StockName = "Microsoft",
                StockSymbol = "MSFT",
                Quantity = 1000,
                OrderDate = DateTime.Now,
                Price = 10001,
            };

            // Assert
            Assert.Throws<ArgumentException>(() =>
            {
                // Act
                _service.CreateSellOrder(sellOrderRequest);
            });
        }

        [Fact]
        public void CreateSellOrder_StockSymbolNull_ToThrowArgumentException()
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
            Assert.Throws<ArgumentException>(() =>
            {
                // Act
                _service.CreateSellOrder(sellOrderRequest);
            });
        }

        [Fact]
        public void CreateSellOrder_DateOlderThanMinimum_ToThrowArgumentException()
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
            Assert.Throws<ArgumentException>(() =>
            {
                // Act
                _service.CreateSellOrder(sellOrderRequest);
            });
        }

        [Fact]
        public void CreateSellOrder_Valid_ToBeSuccessful()
        {
            // Arrange
            SellOrderRequest? sellOrderRequest = new SellOrderRequest()
            {
                StockName = "Microsoft",
                StockSymbol = "MSFT",
                Quantity = 1000,
                OrderDate = DateTime.Now,
                Price = 1000,
            };

            // Act
            SellOrderResponse response = _service.CreateSellOrder(sellOrderRequest);

            // Assert
            Assert.NotEqual(Guid.Empty, response.Id);
        }

        #endregion

        #region GetBuyOrders

        // requirement: When you invoke this method, by default, the returned list should be empty.
        [Fact]
        public void GetBuyOrders_Default_ToBeEmpty()
        {
            // Arrange
            // Act
            var response = _service.GetBuyOrders();

            // Assert
            Assert.Empty(response);
        }

        // requirement: When you first add few buy orders using CreateBuyOrder() method;
        // and then invoke GetBuyOrders() method; the returned list should contain all the same buy orders
        [Fact]
        public void GetBuyOrders_AfterCreateBuyOrderCall_ToBeSuccessful()
        {
            // Arrange
            BuyOrderRequest? buyOrderRequest1 = new BuyOrderRequest()
            {
                StockName = "Microsoft",
                StockSymbol = "MSFT",
                Quantity = 1000,
                OrderDate = DateTime.Now,
                Price = 1000,
            };
            BuyOrderRequest? buyOrderRequest2 = new BuyOrderRequest()
            {
                StockName = "Google",
                StockSymbol = "GOOGL",
                Quantity = 1000,
                OrderDate = DateTime.Now,
                Price = 1000,
            };

            List<BuyOrderRequest> list = [buyOrderRequest1, buyOrderRequest2];

            List<BuyOrderResponse> expectedList = new List<BuyOrderResponse>();
            foreach (var order in list)
            {
                expectedList.Add(_service.CreateBuyOrder(order));
            }

            // Act
            var responseList = _service.GetBuyOrders();

            // Assert
            foreach (var expected in expectedList)
            {
                Assert.Contains(expected, responseList);
            }
        }

        #endregion

        #region GetSellOrders

        // same requirements 
        [Fact]
        public void GetSellOrders_Default_ToBeEmpty()
        {
            // Arrange
            // Act
            var response = _service.GetSellOrders();

            // Assert
            Assert.Empty(response);
        }

        // requirement: When you first add few buy orders using CreateBuyOrder() method;
        // and then invoke GetAllBuyOrders() method; the returned list should contain all the same buy orders
        [Fact]
        public void GetSellOrders_AfterCreateSellOrderCall_ToBeSuccessful()
        {
            // Arrange
            SellOrderRequest? sellOrderRequest1 = new SellOrderRequest()
            {
                StockName = "Microsoft",
                StockSymbol = "MSFT",
                Quantity = 1000,
                OrderDate = DateTime.Now,
                Price = 1000,
            };
            SellOrderRequest? sellOrderRequest2 = new SellOrderRequest()
            {
                StockName = "Google",
                StockSymbol = "GOOGL",
                Quantity = 1000,
                OrderDate = DateTime.Now,
                Price = 1000,
            };

            List<SellOrderRequest> list = [sellOrderRequest1, sellOrderRequest2];

            List<SellOrderResponse> expectedList = new List<SellOrderResponse>();
            foreach (var order in list)
            {
                expectedList.Add(_service.CreateSellOrder(order));
            }

            // Act
            var responseList = _service.GetSellOrders();

            // Assert
            foreach (var expected in expectedList)
            {
                Assert.Contains(expected, responseList);
            }
        }

        #endregion
    }
}
