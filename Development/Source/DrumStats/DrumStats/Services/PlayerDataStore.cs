using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using DrumStats.Models;
using Xamarin.Forms;
using Newtonsoft.Json;
using System.IO;

[assembly: Dependency(typeof(DrumStats.Services.PlayerDataStore))]
namespace DrumStats.Services
{
    public class PlayerDataStore : IDataStore<Player>
    {
        bool isInitialized;
        List<Player> players;

        public async Task<bool> AddItemAsync(Player item)
        {
            await InitializeAsync();

            players.Add(item);

            return await Task.FromResult(true);
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

        public async Task InitializeAsync()
        {
            if (isInitialized)
                return;

            players = new List<Player>();

            var client = new HttpClient();
            var playerStream = await client.GetStreamAsync("http://foosball-results.herokuapp.com/api/players");

            var serializer = new JsonSerializer();

            using (var reader = new StreamReader(playerStream))
            using(var jsonReader = new JsonTextReader(reader))
            {
                var playersResult = (IEnumerable<Player>)serializer.Deserialize(jsonReader, typeof(IEnumerable<Player>));
                foreach (Player player in playersResult)
                {
                    players.Add(player);
                }
            }

            isInitialized = true;
        }
    }
}
