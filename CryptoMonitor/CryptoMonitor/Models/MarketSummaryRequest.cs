using System;
namespace Crypto_Portfolio_Manager.Models
{
    public struct MarketSummaryRequest<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public T Result { get; set; }
    }

    public class MarketSummaryResult
    {
        public string MarketName { get; set; }
        public double High { get; set; } 
        public double Low { get; set; }
        public double Volume { get; set; }
        public double Last { get; set; }
        public double BaseVolume { get; set; }
        public string TimeStamp { get; set; }
        public double Bid { get; set; }
        public double Ask { get; set; }
        public int OpenBuyOrders { get; set; }
        public int OpenSellOrders { get; set; }
        public double PrevDay { get; set; }
        public string Created { get; set; }
    }
}
