using System;
namespace Crypto_Portfolio_Manager.Models
{
    public class TickerRequest<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public T Result { get; set; }
    }

    public struct TickerResult 
    {
        public decimal Bid { get; set; }
        public decimal Ask { get; set; }
        public decimal Last { get; set; }
    }
}
