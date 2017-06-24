using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
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
        private Game previousGame;

        public IDataStore<Player> PlayerDataStore => DependencyService.Get<IDataStore<Player>>();
        public IDataStore<Game> GameDataStore => DependencyService.Get<IDataStore<Game>>();

        public ObservableRangeCollection<Player> Players { get; set; }

        public Command LoadPlayersCommand { get; set; }

        public Command PlayerSelectedCommand { get; set; }

        public List<int> AvailableScores { get; } = new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

        public Game Game { get; set; }

        public int ConsequentWins { get; set; }

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

        public void Initialize(Game previousGame, int consequentWins)
        {
            this.previousGame = previousGame;
            ConsequentWins = consequentWins;

            if (Settings.IsPlayerSubstitutionEnabled)
            { 
                var loserSuccessor = Game.GetTeam(previousGame.Loser.TeamColor);
                var winnerSuccessor = Game.GetTeam(previousGame.Winner.TeamColor);

                bool attackersChanging = false;

                if (ConsequentWins >= 3)
                {
                    attackersChanging = true;
                    ConsequentWins = 0;
                }

                selectionIndex = loserSuccessor.TeamColor == TeamColor.Blue ^ attackersChanging ? 0 : 2;

                loserSuccessor.Defence = Players.First(p => p.Id == previousGame.Loser.Attack.Id);
                loserSuccessor.Attack = attackersChanging ? Players.First(p => p.Id == previousGame.Winner.Attack.Id) : null;
                winnerSuccessor.Defence = Players.First(p => p.Id == previousGame.Winner.Defence.Id);
                winnerSuccessor.Attack = attackersChanging ? null : Players.First(p => p.Id == previousGame.Winner.Attack.Id);
            }

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
            ConsequentWins = 0;
            previousGame = null;
            Game.Reset();
        }

        public async Task<bool> SaveAndNext()
        {
            if (Game.Winner == null)
                return false;

            Game.EndDate = DateTime.Now;

            var result = await GameDataStore.AddItemAsync(Game);

            if (result && Settings.IsPlayerSubstitutionEnabled && (ConsequentWins == 0 || previousGame != null))
            {
                if (ConsequentWins == 0 || (previousGame.Winner.Attack.Id == Game.Winner.Attack.Id && previousGame.Winner.Defence.Id == Game.Winner.Defence.Id))
                {
                    ConsequentWins++;
                }
            }

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
