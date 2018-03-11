using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Http;
using Newtonsoft.Json;

namespace CryptoMonitor.Models.CryptoCompare
{
    public class CryptoCompareManager
    {
        private const string BaseUrl = "https://min-api.cryptocompare.com/data/price?fsym=BTC&tsyms=USD,EUR";

        private readonly HttpClient _httpClient = new HttpClient();
    }
}