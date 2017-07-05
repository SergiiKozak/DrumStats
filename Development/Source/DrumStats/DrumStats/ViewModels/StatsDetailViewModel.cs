using DrumStats.Helpers;
using DrumStats.Models;
using DrumStats.Models.Statistics;
using DrumStats.Services;
using DrumStats.Views;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Xamarin.Forms;

namespace DrumStats.ViewModels
{
    public class StatsDetailViewModel : BaseViewModel
    {
        public IDataStore<Player> PlayerDataStore => DependencyService.Get<IDataStore<Player>>();

        public PlayerStats PlayerStats { get; set; }

        public ObservableRangeCollection<Player> Players { get; set; }

        public Command LoadPlayersCommand { get; set; }

        public StatsDetailViewModel(PlayerStats playerStats)
        {
            PlayerStats = playerStats;
            Players = new ObservableRangeCollection<Player>();
            LoadPlayersCommand = new Command(async () => await ExecuteLoadPlayersCommand());
            MessagingCenter.Subscribe<NewPlayerPage, Player>(this, "AddPlayer", (obj, item) =>
            {
                LoadPlayersCommand.Execute(null);
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
                OnPropertyChanged(string.Empty);
            }
        }
    }
}
