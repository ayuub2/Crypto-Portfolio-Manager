using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using CryptoMonitor.Models.Bittrex;
using Newtonsoft.Json;
using Crypto_Portfolio_Manager.Models;
using System.Security.Cryptography;

namespace Crypto_Portfolio_Manager.Controllers
{
    public class BittrexManager
    {
        //Url constants (public requests)
        private const string BaseUrl = "https://bittrex.com/api/";
        private const string ApiVersion = "v1.1";
        private const string MarketSummariesUrl = "/public/getmarketsummaires";
        private const string CoinsUrl = "/public/getcurrencies";
        private const string MarketUrl = "/public/getmarketsummary?market=";
        private const string TickerUrl = "/public/getticker?market=";
        private const string MarketHistoryUrl = "/public/getmarkethistory?market=";
        //Url constants (account requests)
        private const string BalancesUrl = "/account/getbalances";
        private const string OrderHistoryUrl = "/account/getorderhistory";
        private const string WithdrawalHistoryUrl = "/account/getwithdrawalhistory";
        private const string DepositHistoryUrl = "/account/getdeposithistory";

        //Parameter constants
        private const string ApiKey = "?apikey=";
        private const string Nonce = "&nonce=";
        private const string ApiSecret = "&apisecret=";
        private const string SignHeader = "apisign";

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
        
        //Public requests
        public async Task<MarketSummaryRequest<MarketSummaryResult[]>> GetMarkets()
        {
            var uri = BaseUrl + ApiVersion + MarketSummariesUrl;

            var request = new HttpRequestMessage(HttpMethod.Get, new Uri(uri));

            var response = await _httpClient.SendAsync(request);

            var json = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<MarketSummaryRequest<MarketSummaryResult[]>>(json);
        }

        public async Task<CoinSummaryRequest<CoinSummaryResult[]>> GetCoins()
        {
            var uri = BaseUrl + ApiVersion + CoinsUrl;

            var request = new HttpRequestMessage(HttpMethod.Get, new Uri(uri));

            var response = await _httpClient.SendAsync(request);

            var json = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<CoinSummaryRequest<CoinSummaryResult[]>>(json);
        }

        public async Task<MarketSummaryRequest<MarketSummaryResult>> GetMarketFor(string market, string crypto)
        {
            var uri = BaseUrl + ApiVersion + MarketUrl + market.ToLower() + "-" + crypto.ToLower();

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

        public async Task<MarketHistoryRequest<MarketHistoryResult[]>> GetMarketHistory(string market, string crypto)
        {
            var uri = BaseUrl + ApiVersion + MarketHistoryUrl + market.ToUpper() + "-" + crypto.ToUpper();

            var request = new HttpRequestMessage(HttpMethod.Get, new Uri(uri));

            var response = await _httpClient.SendAsync(request);

            var json = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<MarketHistoryRequest<MarketHistoryResult[]>>(json);
        }

        //Account requests
        public async Task<BalancesRequest<BalancesResult[]>> GetBalances() 
        {
            var parameters = ApiKey + apiKey + Nonce + GenerateNonce() + ApiSecret + apiSecret;

            var uri = BaseUrl + ApiVersion + BalancesUrl + parameters;

            var request = new HttpRequestMessage(HttpMethod.Get, new Uri(uri));

            request.Headers.Add(SignHeader, GenerateHashText(uri));

            var response = await _httpClient.SendAsync(request);

            var json = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<BalancesRequest<BalancesResult[]>>(json);
        }

        public async Task<OrderHistoryRequest<OrderHistoryResult[]>> GetOrderHistory()
        {
            var parameters = ApiKey + apiKey + Nonce + GenerateNonce();// + ApiSecret + apiSecret;

            System.Diagnostics.Debug.WriteLine(parameters);

            var uri = BaseUrl + ApiVersion + OrderHistoryUrl + parameters;

            System.Diagnostics.Debug.WriteLine(uri);

            var request = new HttpRequestMessage(HttpMethod.Get, new Uri(uri));

            var hashResult = GenerateHashText(uri);

            request.Headers.Add(SignHeader, hashResult);

            var response = await _httpClient.SendAsync(request);

            System.Diagnostics.Debug.WriteLine("Status:" + response.StatusCode);
            System.Diagnostics.Debug.WriteLine("Content:" + response.Content);
            System.Diagnostics.Debug.WriteLine("Headers:" + response.Headers);

            var json = await response.Content.ReadAsStringAsync();

            var check = JsonConvert.DeserializeObject<OrderHistoryRequest<OrderHistoryResult[]>>(json);

            System.Diagnostics.Debug.WriteLine("Success: " + check.Success, "Message: " + check.Message);

            return JsonConvert.DeserializeObject<OrderHistoryRequest<OrderHistoryResult[]>>(json);
        }

        public async Task<WithdrawalHistoryRequest<WithdrawalHistoryResult[]>> GetWithdrawalHistory()
        {
            var parameters = ApiKey + apiKey + Nonce + GenerateNonce() + ApiSecret + apiSecret;

            var uri = BaseUrl + ApiVersion + WithdrawalHistoryUrl + parameters;

            var request = new HttpRequestMessage(HttpMethod.Get, new Uri(uri));

            var response = await _httpClient.SendAsync(request);

            var json = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<WithdrawalHistoryRequest<WithdrawalHistoryResult[]>>(json);
        }

        public async Task<DepositHistoryRequest<DepositHistoryResult[]>> GetDepositHistory()
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

        private string GenerateHashText(string uri)
        {
            var uriBytes = _encoding.GetBytes(uri);

            using (var hmac = new HMACSHA512(apiSecretBytes))
            {
                var hash = hmac.ComputeHash(uriBytes);
                var hashText = byteToString(hash);
                System.Diagnostics.Debug.WriteLine(hashText);
                return hashText;
            }
        }

        private string byteToString(byte[] buff)
        {
            string sbinary = "";
            for (int i = 0; i < buff.Length; i++)
                sbinary += buff[i].ToString("X2"); /* hex format */
            return sbinary;
        }
    }
}
