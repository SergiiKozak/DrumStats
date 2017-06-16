using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using DrumStats.Models;
using Xamarin.Forms;
using Newtonsoft.Json;
using System.IO;
using System.Text;
using System.Net.Http.Headers;

[assembly: Dependency(typeof(DrumStats.Services.PlayerDataStore))]
namespace DrumStats.Services
{
    public class PlayerDataStore : IDataStore<Player>
    {
        private const string serverUri = "http://foosball-results.herokuapp.com/api/players";

        private JsonSerializer serializer;

        bool isInitialized;
        List<Player> players;

        public PlayerDataStore()
        {
            serializer = new JsonSerializer();
        }

        public async Task<bool> AddItemAsync(Player item)
        {
            await InitializeAsync();

            using (var writer = new StringWriter())
            using (var jsonWriter = new JsonTextWriter(writer))
            {
                using (var httpClient = new HttpClient())
                {
                    serializer.Serialize(jsonWriter, item);
                    var content = new StringContent(writer.ToString(), Encoding.UTF8, "application/json");

                    var response = await httpClient.PostAsync(serverUri, content);

                    if (response.IsSuccessStatusCode)
                    {
                        var result = await response.Content.ReadAsStringAsync();

                        using (var reader = new StringReader(result))
                        using (var jsonReader = new JsonTextReader(reader))
                        {
                            var playerResult = (Player)serializer.Deserialize(jsonReader, typeof(Player));
                            item.Id = playerResult.Id;
                            players.Add(item);
                        }

                    }

                    return response.IsSuccessStatusCode;
                }
            }
        }

        public async Task<bool> UpdateItemAsync(Player item)
        {
            await InitializeAsync();

            var _item = players.Where((Player arg) => arg.Id == item.Id).FirstOrDefault();
            players.Remove(_item);
            players.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(Player item)
        {
            await InitializeAsync();

            var _item = players.Where((Player arg) => arg.Id == item.Id).FirstOrDefault();
            players.Remove(_item);

            return await Task.FromResult(true);
        }

        public async Task<Player> GetItemAsync(string id)
        {
            await InitializeAsync();

            return await Task.FromResult(players.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<Player>> GetItemsAsync(bool forceRefresh = false)
        {
            await InitializeAsync();

            return await Task.FromResult(players);
        }

        public Task<bool> PullLatestAsync()
        {
            return Task.FromResult(true);
        }


        public Task<bool> SyncAsync()
        {
            return Task.FromResult(true);
        }

        public async Task InitializeAsync(bool forceRefresh = false)
        {
            if (isInitialized && !forceRefresh)
                return;

            players = new List<Player>();
            using (var httpClient = new HttpClient())
            {
                var playerStream = await httpClient.GetStreamAsync(serverUri);

                using (var reader = new StreamReader(playerStream))
                using (var jsonReader = new JsonTextReader(reader))
                {
                    var playersResult = (IEnumerable<Player>)serializer.Deserialize(jsonReader, typeof(IEnumerable<Player>));
                    foreach (Player player in playersResult)
                    {
                        players.Add(player);
                    }
                }
            }

            isInitialized = true;
        }
    }
}
