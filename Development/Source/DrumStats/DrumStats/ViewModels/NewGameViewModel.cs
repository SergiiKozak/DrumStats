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
    public class NewGameViewModel : BaseViewModel
    {
        private int selectionIndex;

        public IDataStore<Player> PlayerDataStore => DependencyService.Get<IDataStore<Player>>();
        public IDataStore<Game> GameDataStore => DependencyService.Get<IDataStore<Game>>();

        public ObservableRangeCollection<Player> Players { get; set; }

        public ObservableRangeCollection<Player> PlayersList1 { get; private set; }
        public ObservableRangeCollection<Player> PlayersList2 { get; private set; }

        public Command LoadPlayersCommand { get; set; }

        public Command PlayerSelectedCommand { get; set; }

        public List<int> AvailableScores { get; } = new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

        public Game Game { get; set; }

        public NewGameViewModel()
        {
            Title = "New Game";

            Game = new Game();

            Players = new ObservableRangeCollection<Player>();
            LoadPlayersCommand = new Command(async () => await ExecuteLoadPlayersCommand());

            PlayerSelectedCommand = new Command((p) => SelectPlayer(p as Player));

            MessagingCenter.Subscribe<NewPlayerPage, Player>(this, "AddPlayer", (obj, item) =>
            {
                var player = item as Player;
                Players.Add(player);
            });
        }

        public void SelectPlayer(Player player)
        {
            switch (selectionIndex)
            {
                case 0:
                    Game.BlueTeam.Attack = player;
                    break;
                case 1:
                    Game.BlueTeam.Defence = player;
                    break;
                case 2:
                    Game.RedTeam.Attack = player;
                    break;
                case 3:
                    Game.RedTeam.Defence = player;
                    break;
                default:
                    selectionIndex = 0;
                    SelectPlayer(player);
                    return;
            }

            Game.StartDate = DateTime.Now;

            selectionIndex++;
        }

        public void SelectScore(int score, TeamColor teamColor)
        {
            switch (teamColor)
            {
                case TeamColor.Blue:
                    Game.BlueTeam.Score = score;
                    break;
                case TeamColor.Red:
                    Game.RedTeam.Score = score;
                    break;
                default:
                    break;
            }
        }

        public void ResetGame()
        {
            selectionIndex = 0;
            Game.Reset();
        }

        public async Task<bool> SaveAndNext()
        {
            Game.EndDate = DateTime.Now;
            var result = await GameDataStore.AddItemAsync(Game);

            return result;
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
