using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CryptoMonitor.Models.Bittrex
{
    public class OrderHistoryRequest<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public T Result { get; set; }
    }

    public class OrderHistoryResult
    {
        public string OrderUuid { get; set; }
        public string Exchange { get; set; }
        public string TimeStamp { get; set; }
        public string OrderType { get; set; }
        public double Limit { get; set; }
        public double Quantity { get; set; }
        public double QuantityRemaining { get; set; }
        public double Commission { get; set; }
        public double Price { get; set; }
        public double PricePerUnit { get; set; }
        public bool IsConditional { get; set; }
        public string Condition { get; set; }
        public string ConditionTarget { get; set; }
        public bool ImmediateOrCancel { get; set; }
    }
}