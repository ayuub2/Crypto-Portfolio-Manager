using System;
namespace Crypto_Portfolio_Manager.Models
{
    public class BalancesRequest<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public T Result { get; set; }
    }

    public class BalancesResult
    {
        public string Currency { get; set; }
        public double Balance { get; set; }
        public double Available { get; set; }
        public double Pending { get; set; }
        public string CryptoAddress { get; set; }
        public bool Requested { get; set; }
        public string Uuid { get; set; }
    }
}
