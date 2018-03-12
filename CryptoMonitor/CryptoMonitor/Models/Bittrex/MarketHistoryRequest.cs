using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CryptoMonitor.Models.Bittrex
{
    public class MarketHistoryRequest<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public T Result { get; set; }
    }

    public class MarketHistoryResult
    {
        public Int64 Id { get; set; }
        public string TimeStamp { get; set; }
        public double Quantity { get; set; }
        public double Price { get; set; }
        public double Total { get; set; }
        public string FillType { get; set; }
        public string OrderType { get; set; }
    }
}