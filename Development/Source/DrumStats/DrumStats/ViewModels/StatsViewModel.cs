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
using Xamarin.Forms;

namespace DrumStats.ViewModels
{
    public class StatsViewModel : BaseViewModel
    {
        public IDataStore<Player> PlayerDataStore => DependencyService.Get<IDataStore<Player>>();

        public StatisticsService StatsService => new StatisticsService();

        public ObservableRangeCollection<Player> Players { get; set; }

        public ObservableRangeCollection<WinRate> WinRates { get; set; }

        public Command LoadPlayersCommand { get; set; }

        public Command LoadStatsCommand { get; set; }

        public StatsViewModel()
        {
            Players = new ObservableRangeCollection<Player>();
            LoadPlayersCommand = new Command(async () => await ExecuteLoadPlayersCommand());

            WinRates = new ObservableRangeCollection<WinRate>();
            LoadStatsCommand = new Command(async () => await ExecuteLoadStatsCommand());

            MessagingCenter.Subscribe<NewPlayerPage, Player>(this, "AddPlayer", async (obj, item) =>
            {
                var player = item as Player;
                var result = await PlayerDataStore.AddItemAsync(player);

                if (result)
                    Players.Add(player);
            });
        }

        async Task ExecuteLoadStatsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                WinRates.Clear();
                var items = await StatsService.GetWinRates();
                WinRates.ReplaceRange(items);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                MessagingCenter.Send(new MessagingCenterAlert
                {
                    Title = "Error",
                    Message = "Unable to load stats.",
                    Cancel = "OK"
                }, "message");
            }
            finally
            {
                IsBusy = false;
            }
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
