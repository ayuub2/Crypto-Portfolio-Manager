using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Crypto_Portfolio_Manager.Models;

namespace Crypto_Portfolio_Manager.Controllers
{
    public class BittrexManager
    {
        private string BaseURL = "https://bittrex.com/api/";
        private string ApiVersion = "v1.1";

        private HttpClient HttpClient;

        public async Task<MarketSummaryRequest<MarketSummaryResult[]>> getMarkets()
        {
            var uri = BaseURL + ApiVersion + "/getmarketsummaries";

            var request = new HttpRequestMessage(HttpMethod.Get, new Uri(uri));

            var response = await HttpClient.SendAsync(request);

            var json = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<MarketSummaryRequest<MarketSummaryResult[]>>(json);
        }

        public async Task<TickerRequest<TickerResult>> getTicker(string Market) 
        {
            var uri = BaseURL + ApiVersion + "/getticker?market=" + Market;

            var request = new HttpRequestMessage(HttpMethod.Get, new Uri(uri));

            var response = await HttpClient.SendAsync(request);

            var json = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<TickerRequest<TickerResult>>(json);
        }

        public async Task<CoinSummaryRequest<CoinSummaryResult[]>> getCoins() 
        {
            var uri = BaseURL + ApiVersion + "/getcurrencies";

            var request = new HttpRequestMessage(HttpMethod.Get, new Uri(uri));

            var response = await HttpClient.SendAsync(request);

            var json = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<CoinSummaryRequest<CoinSummaryResult[]>>(json);
        }

        //Currently privat whilst looking into security risks.
        private async Task<BalancesRequest<BalancesResult[]>> getBalance() 
        {
            var uri = BaseURL + ApiVersion + "getbalances?apikey=" + generateApiKey();

            var request = new HttpRequestMessage(HttpMethod.Get, new Uri(uri));

            var response = await HttpClient.SendAsync(request);

            var json = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<BalancesRequest<BalancesResult[]>>(json);
        }

        private string generateApiKey() {
            return "";
        }
    }
}
