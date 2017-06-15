using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Collections.Generic;
using DrumStats.Helpers;
using DrumStats.Models;
using DrumStats.Views;
using Xamarin.Forms;

namespace DrumStats.ViewModels
{
    class PlayersViewModel : BaseViewModel
    {
        public ObservableRangeCollection<Player> Players { get; set; }

        public Command LoadPlayersCommand { get; set; }

        public PlayersViewModel()
        {
            Players = new ObservableRangeCollection<Player>();
            LoadPlayersCommand = new Command(async () => await ExecuteLoadPlayersCommand());
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
