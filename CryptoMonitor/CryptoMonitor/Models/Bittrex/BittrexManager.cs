using System;
using System.Net.Http;
using System.Text;
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
        private const string ApiKey = "?apikey=";
        private const string Nonce = "&nonce=";
        private const string ApiSecret = "&apisecret=";
        private const string MarketSummariesUrl = "/public/getmarketsummaires";
        private const string MarketUrl = "/public/getmarketsummary?market=";
        private const string TickerUrl = "/public/getticker?market=";
        private const string CoinsUrl = "/public/getcurrencies";
        private const string BalancesUrl = "/public/getbalances?apikey=";
        private const string MarketHistoryUrl = "/public/getmarkethistory?market=";
        private const string OrderHistoryUrl = "/account/getorderhistory";
        private const string WithdrawalHistoryUrl = "/account/getwithdrawalhistory";
        private const string DepositHistoryUrl = "/account/getdeposithistory";

        private readonly HttpClient _httpClient = new HttpClient();
        private readonly Encoding _encoding = Encoding.UTF8;

        private string apiKey;
        private string apiSecret;
        private byte[] apiSecretBytes;

        public BittrexManager()
        {
            this.apiKey = null;
            this.apiSecret = null;
            this.apiSecretBytes = null;
        }

        public BittrexManager(string apiKey, string apiSecret)
        {
            this.apiKey = apiKey;
            this.apiSecret = apiSecret;
            this.apiSecretBytes = _encoding.GetBytes(apiSecret);
        }

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
            var parameters = ApiKey + apiKey + Nonce + GenerateNonce() + ApiSecret + apiSecret;

            var uri = BaseUrl + ApiVersion + BalancesUrl + parameters;

            var request = new HttpRequestMessage(HttpMethod.Get, new Uri(uri));

            var response = await _httpClient.SendAsync(request);

            var json = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<BalancesRequest<BalancesResult[]>>(json);
        }

        private async Task<OrderHistoryRequest<OrderHistoryResult[]>> GetOrderHistory()
        {
            var parameters = ApiKey + apiKey + Nonce + GenerateNonce() + ApiSecret + apiSecret;

            var uri = BaseUrl + ApiVersion + OrderHistoryUrl + parameters;

            var request = new HttpRequestMessage(HttpMethod.Get, new Uri(uri));

            var response = await _httpClient.SendAsync(request);

            var json = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<OrderHistoryRequest<OrderHistoryResult[]>>(json);
        }

        private async Task<WithdrawalHistoryRequest<WithdrawalHistoryResult[]>> GetWithdrawalHistory()
        {
            var parameters = ApiKey + apiKey + Nonce + GenerateNonce() + ApiSecret + apiSecret;

            var uri = BaseUrl + ApiVersion + WithdrawalHistoryUrl + parameters;

            var request = new HttpRequestMessage(HttpMethod.Get, new Uri(uri));

            var response = await _httpClient.SendAsync(request);

            var json = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<WithdrawalHistoryRequest<WithdrawalHistoryResult[]>>(json);
        }

        private async Task<DepositHistoryRequest<DepositHistoryResult[]>> GetDepositHistory()
        {
            var parameters = ApiKey + apiKey + Nonce + GenerateNonce() + ApiSecret + apiSecret;

            var uri = BaseUrl + ApiVersion + DepositHistoryUrl + parameters;

            var request = new HttpRequestMessage(HttpMethod.Get, new Uri(uri));

            var response = await _httpClient.SendAsync(request);

            var json = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<DepositHistoryRequest<DepositHistoryResult[]>>(json);
        }

        //return date and time nonce to ensure uniqueness
        private string GenerateNonce()
        {
            var nonce = DateTime.Now.Ticks;
            return nonce.ToString();
        }
    }
}
