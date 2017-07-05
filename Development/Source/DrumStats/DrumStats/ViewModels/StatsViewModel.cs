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
                                  from wra in statsBundle.WinRatesAbsolute.Where(x => p.Id == x.PlayerId).DefaultIfEmpty()
                                  from wrr in statsBundle.WinRatesRelative.Where(x => p.Id == x.PlayerId).DefaultIfEmpty()
                                  from gr in statsBundle.GoalRate.Where(x => p.Id == x.PlayerId).DefaultIfEmpty()
                                  from pc in statsBundle.PlayCounts.Where(x => p.Id == x.PlayerId).DefaultIfEmpty()
                                  from bp in statsBundle.BestPartners.Where(x => p.Id == x.PlayerId).DefaultIfEmpty()
                                  from wp in statsBundle.WorstPartners.Where(x => p.Id == x.PlayerId).DefaultIfEmpty()
                                  from vi in statsBundle.Victims.Where(x => p.Id == x.PlayerId).DefaultIfEmpty()
                                  from ne in statsBundle.Nemeses.Where(x => p.Id == x.PlayerId).DefaultIfEmpty()
                                  select new PlayerStats()
                                  {
                                      Player = p,
                                      WinRateAbsolute = wra ?? new Rate(),
                                      WinRateRelative = wrr ?? new Rate(),
                                      GoalRate = gr ?? new Rate(),
                                      PlayCount = pc ?? new PlayCount(),
                                      BestPartner = bp ?? new Person(),
                                      WorstPartner = wp ?? new Person(),
                                      Victim = vi ?? new Person(),
                                      Nemesis = ne ?? new Person()
                                  };
                IEnumerable<PlayerStats> playerStatsDeltas;

                if (PlayerStats.Count > 0)
                {
                    playerStatsDeltas = from t0 in PlayerStats
                                        join t1 in playerStats on t0.Player.Id equals t1.Player.Id
                                        select new PlayerStats()
                                        {
                                            Player = t1.Player,
                                            WinRateAbsolute = new Rate()
                                            {
                                                AttackRate = t1.WinRateAbsolute.AttackRate - t0.WinRateAbsolute.AttackRate,
                                                DefenceRate = t1.WinRateAbsolute.DefenceRate - t0.WinRateAbsolute.DefenceRate,
                                                TotalRate = t1.WinRateAbsolute.TotalRate - t0.WinRateAbsolute.TotalRate
                                            },
                                            WinRateRelative = new Rate()
                                            {
                                                AttackRate = t1.WinRateRelative.AttackRate - t0.WinRateRelative.AttackRate,
                                                DefenceRate = t1.WinRateRelative.DefenceRate - t0.WinRateRelative.DefenceRate,
                                                TotalRate = t1.WinRateRelative.TotalRate - t0.WinRateRelative.TotalRate
                                            },
                                            GoalRate = new Rate()
                                            {
                                                AttackRate = t1.GoalRate.AttackRate - t0.GoalRate.AttackRate,
                                                DefenceRate = t1.GoalRate.DefenceRate - t0.GoalRate.DefenceRate,
                                                TotalRate = t1.GoalRate.TotalRate - t0.GoalRate.TotalRate
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
                        WinRateAbsolute = new Rate(),
                        WinRateRelative = new Rate(),
                        GoalRate = new Rate(),
                        PlayCount = new PlayCount()
                    });
                }

                PlayerStatsDeltas.ReplaceRange(playerStatsDeltas);
                PlayerStats.ReplaceRange(playerStats);

                var playerStatsWithDeltas = (from s in PlayerStats
                                            join d in PlayerStatsDeltas on s.Player.Id equals d.Player.Id
                                            select new Pair<PlayerStats, PlayerStats>(s, d));


                PlayerStatsWithDeltas.ReplaceRange(playerStatsWithDeltas.OrderByDescending(psd => psd.First.WinRateRelative.TotalRate));
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
