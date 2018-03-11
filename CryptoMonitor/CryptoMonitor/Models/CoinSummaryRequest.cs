using System;
namespace Crypto_Portfolio_Manager.Models
{
    public class CoinSummaryRequest<T> 
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public T Result { get; set; }
    }

    public class CoinSummaryResult
    {
        public string Currency { get; set; }
        public string CurrencyLong { get; set; }
        public int MinConfirmation { get; set; }
        public double TxFee { get; set; }
        public bool IsActive { get; set; }
        public string CoinType { get; set; }
        public string BaseAddress { get; set; }
    }
}
