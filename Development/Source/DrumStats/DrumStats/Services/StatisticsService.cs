using DrumStats.Models.Statistics;
using DrumStats.Helpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DrumStats.Services
{
    public class StatisticsService
    {
        private const string serverUriTemplate = "https://sergiikozak.ocpu.io/DrumStatsRServer/R/{0}/json";
        private const string winRateFunction = "statsbundle";

        private JsonSerializer serializer;

        public StatisticsService()
        {
            serializer = new JsonSerializer();
        }

        public async Task<StatsBundle> GetStatsBundle()
        {
            using (var httpClient = new HttpClient())
            {
                var content = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("ticker", DateTime.Now.Second.ToString()),
                    new KeyValuePair<string, string>("cutoff.days", Settings.CutOffDays.ToString()),
                    new KeyValuePair<string, string>("cutoff.games", Settings.CutOffGames.ToString())
                });

                var response = await httpClient.PostAsync(string.Format(serverUriTemplate, winRateFunction), content);

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();

                    using (var reader = new StringReader(result))
                    using (var jsonReader = new JsonTextReader(reader))
                    {
                        var itemsResult = (StatsBundle)serializer.Deserialize(jsonReader, typeof(StatsBundle));

                        return itemsResult;
                    }
                }
                return null;
            }
        }
    }
}
