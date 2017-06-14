using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DrumStats.Models;
using Xamarin.Forms;

//[assembly: Dependency(typeof(DrumStats.Services.MockPlayerDataStore))]
namespace DrumStats.Services
{
    public class MockPlayerDataStore : IDataStore<Player>
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
            var _players = new List<Player>
            {
                new Player { Id = Guid.NewGuid().ToString(), Name = "Sergii", Surname = "Kozak"},
                new Player { Id = Guid.NewGuid().ToString(), Name = "Vova", Surname = "Troskot"},
                new Player { Id = Guid.NewGuid().ToString(), Name = "Vova", Surname = "Gavrysh"},
                new Player { Id = Guid.NewGuid().ToString(), Name = "Andriy", Surname = "Vynogradov"}
            };

            foreach (Player player in _players)
            {
                players.Add(player);
            }

            isInitialized = true;
        }
    }
}
