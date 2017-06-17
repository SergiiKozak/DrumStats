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

namespace DrumStats.Services
{
    public abstract class RestDataStore<TItem> : IDataStore<TItem>
        where TItem : BaseDataObject
    {
        public abstract string ServerUri { get; }

        private JsonSerializer serializer;

        bool isInitialized;
        List<TItem> items;

        public RestDataStore()
        {
            serializer = new JsonSerializer();
        }

        public async Task<bool> AddItemAsync(TItem item)
        {
            await InitializeAsync();

            using (var writer = new StringWriter())
            using (var jsonWriter = new JsonTextWriter(writer))
            {
                using (var httpClient = new HttpClient())
                {
                    serializer.Serialize(jsonWriter, item);
                    var content = new StringContent(writer.ToString(), Encoding.UTF8, "application/json");

                    var response = await httpClient.PostAsync(ServerUri, content);

                    if (response.IsSuccessStatusCode)
                    {
                        var result = await response.Content.ReadAsStringAsync();

                        using (var reader = new StringReader(result))
                        using (var jsonReader = new JsonTextReader(reader))
                        {
                            var itemsResult = (TItem)serializer.Deserialize(jsonReader, typeof(TItem));
                            item.Id = itemsResult.Id;
                            items.Add(item);
                        }

                    }

                    return response.IsSuccessStatusCode;
                }
            }
        }

        public async Task<bool> UpdateItemAsync(TItem item)
        {
            await InitializeAsync();

            var _item = items.Where((TItem arg) => arg.Id == item.Id).FirstOrDefault();
            items.Remove(_item);
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(TItem item)
        {
            await InitializeAsync();

            var _item = items.Where((TItem arg) => arg.Id == item.Id).FirstOrDefault();
            items.Remove(_item);

            return await Task.FromResult(true);
        }

        public async Task<TItem> GetItemAsync(string id)
        {
            await InitializeAsync();

            return await Task.FromResult(items.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<TItem>> GetItemsAsync(bool forceRefresh = false)
        {
            await InitializeAsync();

            return await Task.FromResult(items);
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

            items = new List<TItem>();
            using (var httpClient = new HttpClient())
            {
                var itemStream = await httpClient.GetStreamAsync(ServerUri);

                using (var reader = new StreamReader(itemStream))
                using (var jsonReader = new JsonTextReader(reader))
                {
                    var itemsResult = (IEnumerable<TItem>)serializer.Deserialize(jsonReader, typeof(IEnumerable<TItem>));
                    foreach (TItem item in itemsResult)
                    {
                        items.Add(item);
                    }
                }
            }

            isInitialized = true;
        }
    }
}
