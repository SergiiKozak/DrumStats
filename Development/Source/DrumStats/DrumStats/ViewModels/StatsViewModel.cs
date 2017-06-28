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
        public ObservableRangeCollection<PlayerStats> PlayerStatsDeltas { get; set; }

        public ObservableRangeCollection<Pair<PlayerStats, PlayerStats>> PlayerStatsWithDeltas { get; set; }

        public Command LoadDataCommand { get; set; }

        public StatsViewModel()
        {
            PlayerStats = new ObservableRangeCollection<PlayerStats>();
            PlayerStatsDeltas = new ObservableRangeCollection<PlayerStats>();
            PlayerStatsWithDeltas = new ObservableRangeCollection<Pair<PlayerStats, PlayerStats>>();

            LoadDataCommand = new Command(async () => await ExecuteLoadDataCommand());

            MessagingCenter.Subscribe<NewPlayerPage, Player>(this, "AddPlayer", (obj, item) =>
            {
                LoadDataCommand.Execute(null);
            });
            MessagingCenter.Subscribe<NewGamePage, Game>(this, "GameSaved", (obj, item) =>
            {
                //LoadDataCommand.Execute(null);
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
                IEnumerable<PlayerStats> playerStatsDeltas;

                if (PlayerStats.Count > 0)
                {
                    playerStatsDeltas = from t0 in PlayerStats
                                        join t1 in playerStats on t0.Player.Id equals t1.Player.Id
                                        select new PlayerStats()
                                        {
                                            Player = t1.Player,
                                            WinRateAbsolute = new WinRate()
                                            {
                                                AttackWinRate = t1.WinRateAbsolute.AttackWinRate - t0.WinRateAbsolute.AttackWinRate,
                                                DefenceWinRate = t1.WinRateAbsolute.DefenceWinRate - t0.WinRateAbsolute.DefenceWinRate,
                                                TotalWinRate = t1.WinRateAbsolute.TotalWinRate - t0.WinRateAbsolute.TotalWinRate
                                            },
                                            WinRateRelative = new WinRate()
                                            {
                                                AttackWinRate = t1.WinRateRelative.AttackWinRate - t0.WinRateRelative.AttackWinRate,
                                                DefenceWinRate = t1.WinRateRelative.DefenceWinRate - t0.WinRateRelative.DefenceWinRate,
                                                TotalWinRate = t1.WinRateRelative.TotalWinRate - t0.WinRateRelative.TotalWinRate
                                            },
                                            PlayCount = new PlayCount()
                                            {
                                                AttackPlayCount = t1.PlayCount.AttackPlayCount - t0.PlayCount.AttackPlayCount,
                                                DefencePlayCount = t1.PlayCount.DefencePlayCount - t0.PlayCount.DefencePlayCount,
                                                TotalPlayCount = t1.PlayCount.TotalPlayCount - t0.PlayCount.TotalPlayCount
                                            }
                                        };
                }
                else
                {
                    playerStatsDeltas = playerStats.Select(ps => new PlayerStats()
                    {
                        Player = ps.Player,
                        WinRateAbsolute = new WinRate(),
                        WinRateRelative = new WinRate(),
                        PlayCount = new PlayCount()
                    });
                }

                PlayerStatsDeltas.ReplaceRange(playerStatsDeltas);
                PlayerStats.ReplaceRange(playerStats);

                var playerStatsWithDeltas = (from s in PlayerStats
                                            join d in PlayerStatsDeltas on s.Player.Id equals d.Player.Id
                                            select new Pair<PlayerStats, PlayerStats>(s, d));


                PlayerStatsWithDeltas.ReplaceRange(playerStatsWithDeltas.OrderByDescending(psd => psd.First.WinRateRelative.AttackWinRate + psd.First.WinRateRelative.DefenceWinRate));
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
