using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CryptoMonitor.Models.Bittrex
{
    public class DepositHistoryRequest<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public T Result { get; set; }
    }

    public class DepositHistoryResult
    {
        public string PaymentUuid { get; set; }
        public string Currency { get; set; }
        public double Amount { get; set; }
        public string Address { get; set; }
        public string Opened { get; set; }
        public bool Authorized { get; set; }
        public bool PendingPayment { get; set; }
        public double TxCost { get; set; }
        public string TxId { get; set; }
        public bool Canceled { get; set; }
        public bool InvalidAddress { get; set; }
    }
}