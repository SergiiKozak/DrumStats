using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Collections.Generic;
using DrumStats.Helpers;
using DrumStats.Models;
using DrumStats.Views;
using Xamarin.Forms;
using DrumStats.Services;

namespace DrumStats.ViewModels
{
    class PlayersViewModel : BaseViewModel
    {
        public IDataStore<Player> PlayerDataStore => DependencyService.Get<IDataStore<Player>>();

        public ObservableRangeCollection<Player> Players { get; set; }

        public Command LoadPlayersCommand { get; set; }

        public PlayersViewModel()
        {
            Players = new ObservableRangeCollection<Player>();
            LoadPlayersCommand = new Command(async () => await ExecuteLoadPlayersCommand());

            MessagingCenter.Subscribe<NewPlayerPage, Player>(this, "AddPlayer", async (obj, item) =>
            {
                var player = item as Player;
                var result = await PlayerDataStore.AddItemAsync(player);

                if(result)
                    Players.Add(player);
            });
        }

        async Task ExecuteLoadPlayersCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Players.Clear();
                var items = await PlayerDataStore.GetItemsAsync(true);
                Players.ReplaceRange(items);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                MessagingCenter.Send(new MessagingCenterAlert
                {
                    Title = "Error",
                    Message = "Unable to load players list.",
                    Cancel = "OK"
                }, "message");
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
