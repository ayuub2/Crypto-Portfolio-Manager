using System;
using System.Net.Http;
using System.Threading.Tasks;
using CryptoMonitor.Models.Bittrex;
using Newtonsoft.Json;
using Crypto_Portfolio_Manager.Models;

namespace Crypto_Portfolio_Manager.Controllers
{
    public class BittrexManager
    {
        private const string BaseUrl = "https://bittrex.com/api/";
        private const string ApiVersion = "v1.1";
        private const string MarketSummariesUrl = "/public/getmarketsummaires";
        private const string MarketUrl = "/public/getmarketsummary?market=";
        private const string TickerUrl = "/public/getticker?market=";
        private const string CoinsUrl = "/public/getcurrencies";
        private const string BalancesUrl = "/public/getbalances?apikey=";
        private const string MarketHistoryUrl = "/public/getmarkethistory?market=";
        private const string OrderHistoryUrl = "/account/getorderhistory";
        private const string ApiKeyUrl = "?apikey=";

        private readonly HttpClient _httpClient = new HttpClient();

        public async Task<MarketSummaryRequest<MarketSummaryResult[]>> GetMarkets()
        {
            var uri = BaseUrl + ApiVersion + MarketSummariesUrl;

            var request = new HttpRequestMessage(HttpMethod.Get, new Uri(uri));

            var response = await _httpClient.SendAsync(request);

            var json = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<MarketSummaryRequest<MarketSummaryResult[]>>(json);
        }

        public async Task<MarketSummaryRequest<MarketSummaryResult>> GetMarketFor(string market)
        {
            var uri = BaseUrl + ApiVersion + MarketUrl + "btc-" + market.ToLower();

            var request = new HttpRequestMessage(HttpMethod.Get, new Uri(uri));

            var response = await _httpClient.SendAsync(request);

            var json = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<MarketSummaryRequest<MarketSummaryResult>>(json);
        }

        public async Task<TickerRequest<TickerResult>> GetTicker(string market) 
        {
            var uri = BaseUrl + ApiVersion + TickerUrl + market;

            var request = new HttpRequestMessage(HttpMethod.Get, new Uri(uri));

            var response = await _httpClient.SendAsync(request);

            var json = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<TickerRequest<TickerResult>>(json);
        }

        public async Task<CoinSummaryRequest<CoinSummaryResult[]>> GetCoins() 
        {
            var uri = BaseUrl + ApiVersion + CoinsUrl;

            var request = new HttpRequestMessage(HttpMethod.Get, new Uri(uri));


            var response = await _httpClient.SendAsync(request);

            var json = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<CoinSummaryRequest<CoinSummaryResult[]>>(json);
        }

        //Currently private whilst looking into security risks.
        private async Task<BalancesRequest<BalancesResult[]>> GetBalances() 
        {
            var uri = BaseUrl + ApiVersion + BalancesUrl + generateApiKey();

            var request = new HttpRequestMessage(HttpMethod.Get, new Uri(uri));

            var response = await _httpClient.SendAsync(request);

            var json = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<BalancesRequest<BalancesResult[]>>(json);
        }

        private async Task<OrderHistoryRequest<OrderHistoryResult[]>> getOrderHistory(string apiKey)
        {
            var uri = BaseUrl + ApiVersion + BalancesUrl + OrderHistoryUrl + ApiKeyUrl + apiKey;

            var request = new HttpRequestMessage(HttpMethod.Get, new Uri(uri));

            var response = await _httpClient.SendAsync(request);

            var json = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<OrderHistoryRequest<OrderHistoryResult[]>>(json);
        }

        //TODO: generate api key
        private string generateApiKey() {
            return "";
        }
    }
}
