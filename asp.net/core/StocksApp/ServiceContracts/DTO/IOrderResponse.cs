﻿namespace ServiceContracts.DTO
{
    public interface IOrderResponse
    {
        string StockSymbol { get; set; }
        string StockName { get; set; }

        DateTime OrderDate { get; set; }

        uint Quantity { get; set; }

        double Price { get; set; }

        OrderType TypeOfOrder { get; }

        double TradeAmount { get; set; }
    }

    public enum OrderType
    {
        BuyOrder, SellOrder
    }
}
