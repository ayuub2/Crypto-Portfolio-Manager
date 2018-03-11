using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Http;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace CryptoMonitor.Models.CryptoCompare
{
    public class CryptoCompareManager
    {
        private const string BaseUrl = "https://min-api.cryptocompare.com/data/price?fsym=BTC&tsyms=USD,EUR";

        private readonly HttpClient _httpClient = new HttpClient();

        public async Task GetPriceFor(string coin) 
        {
            var uri = BaseUrl;

            var request = new HttpRequestMessage(HttpMethod.Get, new Uri(uri));

            var response = await _httpClient.SendAsync(request);

            var json = await response.Content.ReadAsStringAsync();

            //return JsonConvert.DeserializeObject<MarketSummaryRequest<MarketSummaryResult[]>>(json);
        }
    }
}