using DrumStats.Helpers;
using DrumStats.Models;
using DrumStats.Models.Statistics;
using DrumStats.Services;
using DrumStats.Views;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace DrumStats.ViewModels
{
    public class StatsViewModel : BaseViewModel
    {
        public IDataStore<Player> PlayerDataStore => DependencyService.Get<IDataStore<Player>>();

        public StatisticsService StatsService => new StatisticsService();

        public ObservableRangeCollection<PlayerStats> PlayerStats { get; set; }

        public Command LoadDataCommand { get; set; }

        public StatsViewModel()
        {
            PlayerStats = new ObservableRangeCollection<PlayerStats>();
            LoadDataCommand = new Command(async () => await ExecuteLoadDataCommand());

            MessagingCenter.Subscribe<NewPlayerPage, Player>(this, "AddPlayer", (obj, item) =>
            {
                LoadDataCommand.Execute(null);
            });
        }

        async Task ExecuteLoadDataCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                var players = await PlayerDataStore.GetItemsAsync();
                var statsBundle = await StatsService.GetStatsBundle();

                var playerStats = from p in players
                                  join wra in statsBundle.WinRatesAbsolute on p.Id equals wra.PlayerId
                                  join wrr in statsBundle.WinRatesRelative on p.Id equals wrr.PlayerId
                                  join pc in statsBundle.PlayCounts on p.Id equals pc.PlayerId
                                  select new PlayerStats()
                                  {
                                      Player = p,
                                      WinRateAbsolute = wra,
                                      WinRateRelative = wrr,
                                      PlayCount = pc
                                  };

                PlayerStats.ReplaceRange(playerStats.OrderByDescending(ps => ps.WinRateRelative.AttackWinRate + ps.WinRateRelative.DefenceWinRate));
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                MessagingCenter.Send(new MessagingCenterAlert
                {
                    Title = "Error",
                    Message = "Unable to load stats data.",
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
